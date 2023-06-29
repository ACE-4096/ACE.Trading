using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Analytics.Slopes
{
    internal class TrainingData
    {
        private List<TrainingDataLine> Lines = new List<TrainingDataLine>();

        public void Add(TrainingDataLine line)
        {
            Lines.Add(line);
        }
        public void Add(string prompt, string completion)
        {
            Add(new TrainingDataLine(prompt, completion));
        }

        public string ToString()
        {
            string output = "";
            foreach (TrainingDataLine line in Lines.ToArray())
            {
                output += line.ToString();
            }
            return output.Trim();
        }

    }

    internal class TrainingDataLine
    {
        private string _prompt;
        private string _completion;

        public string getPrompt { get { return _prompt; } }
        public string getCompletion { get { return _completion; } }

        public TrainingDataLine(string prompt, string completion)
        {
            _prompt = prompt;
            _completion = completion;
        }

        public string ToString()
        {
            return string.Format("{\"prompt\": \"{0}\", \"completion\": \"{1}\"}\n", _prompt, _completion);
        }
    }
}
