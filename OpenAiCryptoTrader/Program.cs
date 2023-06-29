using OpenAI_API.FineTune;
using CryptoExchange.Net.CommonObjects;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using ACE.Trading.OpenAi.TrainingData;
using ACE.Trading;
using System.Transactions;
using ACE.Trading.OpenAi;
using System.Diagnostics;

//Console.WriteLine("Welcome to the ACE-4096 predictive trading.");

//BinanceHandler b = new BinanceHandler();

OpenAiIntegration pe = new OpenAiIntegration();

//b.ViewData(new string[] { "BTCUSDT" });

DataCache.Load();
DataCache.AutoSaveDelay = 5 * 60; // in seconds

PriceHistoryLogging priceHistoryLogging = new PriceHistoryLogging();

priceHistoryLogging.startLogging();
Console.Read();
while (true)
{
    Console.WriteLine("Which task Would you like to undertake: ");
    int val = readChar(
        "[T] - Train a new model.\n" +
        "[P] - Attempt to Predict next Price.\n" +
        "[S] - Start the autoTrader\n" +
        "[F] - View uploaded files \n" +
        "[V] - View Collected Data ;)");

    switch (val)
    {
        case 'T':
        case 't':
            trainModel();
            break;
        case 'C':
        case 'c':
            collectData();
            break;
        case 'P':
        case 'p':
            predict();
            break;
        case 'S':
        case 's':
            startAutoTrader();
            break;
        case 'V':
        case 'v':
            viewData();
            break;
        case 'F':
        case 'f':
            viewFiles();
            break;
        default:
            break;
    }
    Console.WriteLine("Invalid reponse received, try again.");
}

async void viewFiles()
{
    printHeading();
    Console.WriteLine("\nFile id | File name | File Size\n");

    var response = await pe.getFiles();

    if (response == null || response.Count == 0)
    {
        Console.WriteLine("No files found");
        return;
    }
    foreach (var file in response)
    {
        Console.WriteLine($"{file.Id} | {file.Name} | {file.Bytes}B");
    }

}

void viewData()
{
    Console.Clear();
    Console.WriteLine($"Symbol: NumOfLoggedPriceChanges | Latest Price");
    string[] symbols = DataCache.getAllSymbols();
    foreach (string symbol in symbols)
    {
        var sd = DataCache.GetSymbolData(symbol);
        Console.WriteLine($"{symbol}: {sd.priceHistory.Count} | ${sd.getLatestPrice}");
    }
    readValue();
}

void collectData()
{
    Console.Write("Enter the symbol to collect data for: ");
    string symbol = null;
    do
    {
        symbol = Console.ReadLine();
    } while (symbol == null || symbol == "");
    DataCache.add(symbol);
    priceHistoryLogging.startLogging(symbol);


    var x = 0.0m;
    while (true)
    {
        Thread.Sleep(1000);
        var sd = DataCache.GetSymbolData(symbol);
        var price = sd.getLatestPrice;

        // Set Colour
        if (price > x)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (price < x)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        Console.WriteLine(symbol + ": $" + price);
        x = price;
    }
}

void startAutoTrader()
{

    Console.WriteLine("Not Yet Implemented");
}

async void predict()
{
    Console.WriteLine("Retrieving Current proccessing models...");
    
    var list = pe.getFineTuneList();

    Console.WriteLine("What symbol would you like to train for?: ");
    Console.WriteLine("Selection Id | Model Name | Base Model Used | Symbol");
    int tmpNum = 0;
    if (list.Result == null)
    {
        Console.WriteLine("No fine tunes to use. please train a model first.");
        return;
    }
    foreach (FineTuneResult res in list.Result.data)
    {
        string resSymbol = "Undefined";
        if (res.FineTunedModel == null)
            continue;
        if (res.FineTunedModel.Contains('-'))
        {
            resSymbol = res.FineTunedModel.Substring(res.FineTunedModel.LastIndexOf('-') + 1);
        }
        Console.WriteLine(tmpNum++ + " | " + res.FineTunedModel + " | " + res.Model + " | " + resSymbol);
    }
    // reading input 
    int selection = -1;
    string tmpRead = "";
    do
    {
        Console.WriteLine("Model to use (By Id): ");
        do
        {
            tmpRead = Console.ReadLine();
        } while (tmpRead == null || tmpRead?.Length == 0);
    } while (!Int32.TryParse(tmpRead, out selection) && (selection < 0 && selection >= list.Result.data.Count));

    // create model
    var modelID = list.Result.data[selection].Id;

    // fetches Symbol data
    var symbol = list.Result.data[selection].FineTunedModel?.Substring(list.Result.data[selection].FineTunedModel.LastIndexOf('-') + 1);
    if (symbol?.Length == 0)
        Console.WriteLine("Invalid Symbol");

    List<Price> prices = await new Predictions().predict(symbol, modelID);
    Console.Clear();
    Console.WriteLine($"---  ACE Predictions  ---\nSymbol: {symbol}\nModel: {modelID}");

    foreach (var price in prices)
    {
        Console.WriteLine(string.Format("Time UTC: {0} | Price: ${1}", price.timeUtc, price.getDeltaPrice));
    }
}

void trainModel()
{
    

    string symbol = readString("What symbol would you like to train for?");
    if (symbol.Length < 6)
    {
        Console.WriteLine("Invalid Symbol");
        return;
    }

    bool useCache = true;
    bool loop = true;
    while(loop)
    {
        Console.Write("Would you like to compile a training set based on the DataCache? [Y]es | [N]o :");
        int input = Console.Read();
        switch (input)
        {
            case 'Y':
            case 'y':
                loop = false;
                break;
            case 'N':
            case 'n':
                loop = useCache = false;
                break;
        }
    
    }

    if (!useCache)
    {
        //
        Console.WriteLine("Trining with Binance data. ");

        Console.WriteLine("What is the location of the candlestick data to use? ");
        string? fileName;
        do
        {
            fileName = Console.ReadLine();
        } while (fileName == null || !File.Exists(fileName) || fileName == "");

        int promptNum = readInt("How many price points per prompt? : ");

        int completionNum = readInt("How many price points per completion? : ");
        Console.WriteLine("Converting...");
        DataFile df = Convertions.convertFromBinanceDataFile(fileName, promptNum, completionNum);//(fileName, outputFileName);
        string tmpFilename = Path.GetTempFileName()+ ".JSONL";
        File.WriteAllText(tmpFilename, df.ToString());
        Console.WriteLine("Convertions Complete");

        Console.WriteLine("Creating Fine-Fune on OpenAi network...");
        var result = pe.fineTune(tmpFilename, symbol);
        Console.WriteLine("Creation complete.");

        foreach (var _event in result.Result.Events)
        {
            Console.WriteLine(_event.CreatedAt + ": " + _event.Message);
        }
    }
    else
    {
        printHeading();
        Console.WriteLine("\nSymbol: " + symbol);
        Console.WriteLine("");
        
        // Get price intervals
        var input = -1;

        Console.WriteLine("What interval apart sholuld the prices be?: [0] 1 Minute, [1] 5 Minute, [2] 15 Minute, [3] 1 Hour");
        do
        {
            input = Console.Read();
        } while (input < '0' || input > '3');

        int chosenInterval = input;
        input = -1;

        // get price type - avgPrice / deltaPrice
        Console.WriteLine("Would you like the training data to be prepared using the [0] Delta price (Change in price since last interval)\nOr the [1] Average price (Average price during the interval): ");
        do
        {
            input = Console.Read();
        } while (input < '0' || input > '1');
        int chosenPriceType = input;

        input = -1;

        // get duration
        Console.WriteLine("How much data would you like to use? What duration of time should be used for the data set? ");
        do
        {
            input = Console.Read();
        } while (input < '0');
        int duration = input-48;

        string output;
        if (!Encoding.ExtractPromptFromDataCache(symbol, (Encoding.priceInterval)(chosenInterval-48), (Encoding.priceType)(chosenPriceType-48), duration, out output))
        {
            Console.WriteLine("Compilation failed.");
            return;
        }

        string tmp = Path.Combine(Path.GetTempPath(), "tmpTrainingDataSet.txt");
        File.WriteAllText(tmp, output);
        Console.WriteLine(string.Format("The Training data has been compiled to : \"{0}\"", tmp));

        OpenAiIntegration openAi = new OpenAiIntegration();
        
        var result = openAi.fineTune(tmp, symbol);

        Console.WriteLine($"Fine tune status: {result.Result.Status}, Model Name: {result.Result.FineTunedModel}");
    }
}
void printHeading()
{
    Console.Clear();
    Console.WriteLine("----------------------------------------------------------");
    Console.WriteLine("-----ACE-4096 Predictive Trading - DataCache Training-----");
}

int readValue()
{
    //Console.In.ReadLine();
    int? val;
    do { val = Console.Read(); } while (val == null || val == -1);
    return val.Value;
}
int readChar(string messagePrompt)
{
    Console.Clear();
    Console.WriteLine(messagePrompt);
    int? val;
    do { val = Console.Read(); } while (val == null || val == -1);
    return val.Value;
}

string readString(string messagePrompt)
{
    Console.Clear();
    Console.WriteLine(messagePrompt);
    string output;
    do
    {
        output = Console.ReadLine();
    } while (output == null || output == "");
    return output;
}
int readInt(string messagePrompt)
{
    Console.Clear();
    Console.WriteLine(messagePrompt);
    string output; int number;
    do
    {
        output = Console.ReadLine();
    } while (output == null || output == "" || !int.TryParse(output, out number));

    return number;
}