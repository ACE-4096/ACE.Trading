using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi.TrainingData
{
    public class DataLine
    {
        private string _prompt;
        private string _completion;

        public string getPrompt { get { return _prompt; } }
        public string getCompletion { get { return _completion; } }

        public DataLine(string prompt, string completion)
        {
            _prompt = prompt;
            _completion = completion;
        }

        override public string ToString()
        {
            return $"{{\"prompt\": \"{_prompt}\", \"completion\": \"{_completion}\"}}\n";
        }
    }
}
