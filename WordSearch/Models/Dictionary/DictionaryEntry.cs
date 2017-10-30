using System;
using System.Collections.Generic;

namespace WordSearch.Models.Dictionary
{
    public class DictionaryEntry
    {
        public String Headword { get; set; }
        public String Wav { get; set; }
        public String FunctionalLabel { get; set; }
        public String Date { get; set; }
        public IList<Definition> Definitions { get; set; }

        public String WavUrl {
            get
            {
                if (String.IsNullOrWhiteSpace(Wav))
                    return null;
                String subdirectory;
                if (Wav.StartsWith("bix"))
                    subdirectory = "bix";
                else if (Wav.StartsWith("gg"))
                    subdirectory = "gg";
                else if (Wav.StartsWith("number"))
                    subdirectory = "number";
                else
                    subdirectory = Wav.Substring(0, 1);
                return $"http://media.merriam-webster.com/soundc11/{subdirectory}/{Wav}";
            }
        }

        public DictionaryEntry()
        {
            Definitions = new List<Definition>();
        }
    }
}
