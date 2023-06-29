using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.OpenAi.TrainingData
{
    public class DataFile
    {
        private List<DataLine> Lines = new List<DataLine>();

        public void Add(DataLine line)
        {
            Lines.Add(line);
        }
        public void Add(string prompt, string completion)
        {
            Add(new DataLine(prompt, completion));
        }

        override public string ToString()
        {
            string output = "";
            foreach (DataLine line in Lines.ToArray())
            {
                output += line.ToString();
            }
            return output.Trim();
        }
    }
}
