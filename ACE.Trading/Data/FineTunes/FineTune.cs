using ACE.Trading.Optimization;
using Newtonsoft.Json;
using OpenAI_API.FineTune;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ACE.Trading.Data.FineTunes
{
    public class FineTunes
    {
        // Non-Static Funcs\Vars
        public List<FineTune> fineTuneList;
        public List<FineTuneFile> fineTuneFilesList;

        public FineTunes()
        {
            fineTuneList = new List<FineTune>(); 
            fineTuneFilesList = new List<FineTuneFile>();
        }
        // Static Funcs
        private static FineTunes data;
        private const string cacheLocation = "FineTunes.x3";
        public static FineTune FindFineTuneById(string id)
        {
            if (data == null)
            {
                Load();
            }

            return data.fineTuneList.Find(x => x.id == id);

        }
        public static FineTuneFile FindFileById(string id)
        {
            if (data == null)
            {
                Load();
            }

            return data.fineTuneFilesList.Find(x => x.id == id);

        }
        public static void AddFile(FineTuneFile x)
        {
            if (data == null)
            {
                Load();
            }
            data.fineTuneFilesList.Add(x);
            Save();
        }
        public static void Add(FineTune x)
        {
            if (data == null)
            {
                Load();
            }
            data.fineTuneList.Add(x);
            Save();
        }
        public static void Load()
        {
            if (File.Exists(cacheLocation))
            {
                string json = File.ReadAllText(cacheLocation);
                data = JsonConvert.DeserializeObject<FineTunes>(json);
            }
            else
            {
                if (data == null)
                {
                    data = new FineTunes();
                }
            }
        }
        public static void Save()
        {
            string jsonString = JsonConvert.SerializeObject(data);
             File.WriteAllText(cacheLocation, jsonString);
        }
    }
    public class FineTune
    {

        public string id;
        public int slopesPerPrompt;
        public List<string> files;

        public FineTune(string id, FineTuneFile data)
        {
            this.id = id;
            slopesPerPrompt = data.slopesPerPrompt;
            if (files == null)
            {
                files = new List<string>();
            }
            files.Add(data.id);

        }
        public FineTune(FineTuneResult result)
        {
            this.id = result.Id;

            if (result.TrainingFiles.Count > 0)
            {
                FineTuneFile x = FineTunes.FindFileById(result.TrainingFiles[0].Id);
                this.slopesPerPrompt = x.slopesPerPrompt;
                if (files == null)
                {
                    files = new List<string>();
                }
                files.Add(x.id);
            }

        }
    }
    public class FineTuneFile
    {
        public string id;
        public int slopesPerPrompt;
        /// <summary>
        /// Creates an instance of a local reference to a fine tune file configuration
        /// </summary>
        /// <param name="id">Id of the uploaded file</param>
        /// <param name="slopesPerPrompt">Number of slopes per prompt within the training file</param>
        public FineTuneFile(string id, int slopesPerPrompt)
        {
            this.id = id;
            this.slopesPerPrompt = slopesPerPrompt;
        }
    }
}
