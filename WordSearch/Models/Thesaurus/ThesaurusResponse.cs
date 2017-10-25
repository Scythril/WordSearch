using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordSearch.Models.Thesaurus
{
    [JsonObject]
    public class ThesaurusResponse
    {
        public IList<ThesaurusListItem> Response { get; set; }
    }
}
