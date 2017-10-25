using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordSearch.Models.Thesaurus
{
    [JsonObject]
    public class ThesaurusItem
    {
        public String Category { get; set; }

        public String Synonyms { get; set; }

        [JsonIgnore]
        public IList<String> GetSynonyms {
            get
            {
                return Synonyms.Split('|');
            }
        }
    }
}
