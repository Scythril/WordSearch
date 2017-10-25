using Newtonsoft.Json;

namespace WordSearch.Models.Thesaurus
{
    [JsonObject]
    public class ThesaurusListItem
    {
        public ThesaurusItem List { get; set; }
    }
}
