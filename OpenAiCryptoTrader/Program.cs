using OpenAI_API.FineTune;
using CryptoExchange.Net.CommonObjects;
using ACE.Trading.Data;
using ACE.Trading.Data.Collection;
using ACE.Trading.OpenAi.TrainingData;
using ACE.Trading;
using System.Transactions;
using ACE.Trading.OpenAi;
using System.Diagnostics;
using Newtonsoft.Json;
using ACE.Trading.Analytics.Slopes;
using System;
using OpenAI_API;

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
        "[F] - Files \n" +
        "[V] - View Collected Data\n" +
        "[U] - Upload a TrainingFile\n" +
        "[J] - Fine tune a new model ;)");

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
            fileHandling();
            break;
        case 'J':
        case 'j':
            startFineTune();
            break;
        default:
            break;
    }
    Console.WriteLine("Invalid reponse received, try again.");
}

async void startFineTune()
{
    string fileId, symbol;
    do {
        fileId = readString("What is the id of the Training File to use?");
        symbol = readString("What is the symbol for the Training File?");
        Console.WriteLine($"File Id: {fileId} | Symbol: {symbol}");
    } while(readYesNo("Are the details correct?"));

    HyperParams parameters = readHypers();

    FineTuneResult result = await pe.fineTune(fileId, symbol, parameters);

    //result.HyperParameters

}

HyperParams readHypers()
{
    Console.WriteLine("Batch Size\n The batch size to use for training.\n The batch size is the number of training examples used to train a single forward and backward pass.\n");
    Console.WriteLine("Number Of Epochs - The number of epochs to train the model for.\n An epoch refers to one full cycle through the training dataset.\n");
    Console.WriteLine("Learning Rate Multiplier - The learning rate multiplier to use for training.\n The fine-tuning learning rate is the original learning rate used for pretraining multiplied by this value.\n");
    Console.WriteLine(" Prompt Weight Loss - The weight to use for loss on the prompt tokens.\n This controls how much the model tries to learn to generate the prompt (as compared to the completion which always has a weight of 1.0),\n and can add a stabilizing effect to training when completions are short.\n");

    HyperParams hypers = new HyperParams()
    {
        BatchSize = readInt("Batch Size: "),
        NumberOfEpochs = readInt("Number of Epochs: "),
        LearningRateMultiplier = readDouble("Learning Rate Multiplier: "),
        PromptLossWeight = readDouble("Prompt Loss Weight: ")
    };

    return hypers;
}


#region Files
void fileHandling()
{
    char input; bool proceed = false;
    do
    {
        printHeading();
        input = readChar(
            "[V] - View Files\n" +
            "[D] - Delete File\n" +
            "[U] - Upload File"
            );
        
        switch (input)
        {
            case 'V':
            case 'v':
            case 'D':
            case 'd':
            case 'U':
            case 'u':
                proceed = true;
                break;
        }
    } while (!proceed);
    switch (input)
    {
        case 'V':
        case 'v':
            viewFiles();
            break;
        case 'D':
        case 'd':
            deleteFile();
            break;
        case 'U':
        case 'u':
            uploadFile();
            break;
    }
}
async void uploadFile()
{
    printHeading();

    bool input = readYesNo("Would you like to generate a new a training file?", false);
    if (input)
    {
        input = readYesNo("Are you using binance data?", false);
        if (input)
        {
            string inputFilename = readFilename("What is the filename of the binance data to use?", false);
            string saveName = readString("What name do you want the output file to have: ", false);
            string outputFilename = Path.GetTempPath() + saveName + ".jsonl";
            int promptsPer = readInt("How may prompts in the prompt/completion ratio: ", false);
            int completionsPer = readInt("How may completions in the prompt/completion ratio: ", false);

            bool useSlope = readYesNo("Would you like to use slopeData or standard?", false); 
            Console.WriteLine("Converting...");
            if (!useSlope)
            {
                
                Convertions.convertFromBinanceData(inputFilename, outputFilename, promptsPer, completionsPer);
            }
            else
            {
                TrainingData td = BinanceToTraining.convertSlopes(inputFilename, promptsPer, completionsPer);
                File.WriteAllText(outputFilename, td.ToString());
            
            }
            Console.WriteLine("Converting Complete.");
            Console.Write("Uploading");
            Task<OpenAI_API.Files.File> y = pe.uploadFile(outputFilename);
            while (!y.IsCompleted) { Thread.Sleep(1000); Console.Write('.'); }
            if (y.IsCanceled)
            {
                Console.WriteLine("Timeout occured.");
            }
            else if (y.IsCompletedSuccessfully)
            {
                Console.WriteLine("Uploading Complete.");
                Console.WriteLine("\nFile id | File name | File Size");
                Console.WriteLine($"{y.Result.Id} | {y.Result.Name} | {y.Result.Bytes}B");
            }
            Console.WriteLine("\n\n Press any key to continue.");
            FlushKeyboard();
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("NOT YET IMPLEMENTED.");
        }
    }
    else if (!input)
    {
        string filename = readFilename("What is the filename of the trainingfile: ");
        var y = pe.uploadFile(filename);
        Console.WriteLine("\nFile id | File name | File Size");
        Console.WriteLine($"{y.Result.Id} | {y.Result.Name} | {y.Result.Bytes}B");
    }


}
async void viewFiles()
{
    Task<List<OpenAI_API.Files.File>> response = pe.getFiles();
    while (!response.IsCompleted) { Thread.Sleep(1000); Console.Write('.'); }
    if (response.IsCanceled)
    {
        Console.WriteLine("Timeout occured.");
    }
    else if (response.IsCompletedSuccessfully)
    {
        printHeading();
        Console.WriteLine("\nFile id | File name | File Size\n");
        foreach (var file in response.Result)
        {
            Console.WriteLine($"{file.Id} | {file.Name} | {file.Bytes}B");
        }
    }
    Console.WriteLine("\n\n Press any key to continue.");
    FlushKeyboard();
    Console.ReadKey();

}
async void deleteFile()
{


    var response = await pe.getFiles();
    if (response == null || response.Count == 0)
    {
        Console.WriteLine("No files found");
        return;
    }
    printHeading();
    Console.WriteLine("\nFile Index | File id | File name | File Size\n");
    for (int i = 0; i < response.Count; i++)
    {
        Console.WriteLine($"{i} | {response[i].Id} | {response[i].Name} | {response[i].Bytes}B");
    }
    int input = -1;
    do {
        input = readInt("Which File would you like to delete?: ", false);
    } while (input < 0 || input >= response.Count);
    var deletedFile = await pe.deleteFile(response[input].Id);
    if (deleteFile == null)
    {
        Console.WriteLine("Error");
        return;
    }
    else
    {
        string deletedText = deletedFile.Deleted ? "Deleted" : "Not-Deleted";
        Console.WriteLine($"File: {deletedFile.Id} / {deletedFile.Name} | {deletedText}");
    }
}

#endregion
void viewData()
{
    Console.Clear();
    Console.WriteLine($"Symbol: NumOfLoggedPriceChanges | Latest Price");
    string[] symbols = DataCache.getAllSymbols();
    foreach (string symbol in symbols)
    {
        var sd = DataCache.GetSymbolData(symbol);
        Console.WriteLine($"{symbol}: {sd.getPriceHistory.Count} | ${sd.getLatestPrice}");
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

    List<PricePoint> prices = await new Predictions().predict(symbol, modelID);
    Console.Clear();
    Console.WriteLine($"---  ACE Predictions  ---\nSymbol: {symbol}\nModel: {modelID}");

    foreach (var price in prices)
    {
        Console.WriteLine(string.Format("Time UTC: {0} | Price: ${1}", price.timeUtc, price.deltaPrice));
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
        if (!SimpleEncoding.ExtractPromptFromDataCache(symbol, (SimpleEncoding.priceInterval)(chosenInterval - 48), (SimpleEncoding.priceType)(chosenPriceType - 48), duration, out output))
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

#region reading data from user
int readValue()
{
    FlushKeyboard();
    int? val;
    do { val = Console.Read(); } while (val == null || val == -1);
    return val.Value;
}
bool readYesNo(string prompt, bool clear = true)
{
    FlushKeyboard();
    bool output = false, proceed = false;
    do
    {
        var input = readChar(prompt + " [Y]es | [N]o", clear);
        switch (input)
        {
            case 'Y':
            case 'y':
                output = proceed = true;
                break;
            case 'N':
            case 'n':
                output = false;
                proceed = !output;
                break;
        }
    } while (!proceed);
    return output;
}
char readChar(string messagePrompt, bool clear = true)
{
    FlushKeyboard();
    if (clear) Console.Clear();
    Console.WriteLine(messagePrompt);
    int? val;
    do { val = Console.Read(); } while (val == null || val == -1);
    return (char)val.Value;
}
string readString(string messagePrompt, bool clear = true)
{
    FlushKeyboard();
    if (clear) Console.Clear();
    Console.WriteLine(messagePrompt);
    string output;
    do
    {
        output = Console.ReadLine();
    } while (output == null || output == "");
    return output;
}
string readFilename(string messagePrompt, bool clear = true)
{
    string output;
    do
    {
        output = readString(messagePrompt, clear);
    } while (!File.Exists(output));
    return output;
}
int readInt(string messagePrompt, bool clear = true)
{
    FlushKeyboard();
    string output; int number;
    do
    {
        output = readString(messagePrompt, clear);
    } while (output == null || output == "" || !int.TryParse(output, out number));

    return number;
}

double readDouble(string messagePrompt, bool clear = true)
{
    FlushKeyboard();
    string output; double number;
    do
    {
        output = readString(messagePrompt, clear);
    } while (output == null || output == "" || !double.TryParse(output, out number));

    return number;
}
void FlushKeyboard()
{
    while (Console.In.Peek() != -1)
        Console.In.Read();
}
#endregion