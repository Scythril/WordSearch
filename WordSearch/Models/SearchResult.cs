using System.Collections.Generic;
using WordSearch.Models.Dictionary;
using WordSearch.Models.Thesaurus;

namespace WordSearch.Models
{
    public class SearchResult
    {
        public IList<DictionaryEntry> DictionaryEntries { get; set; }
        public IList<ThesaurusItem> ThesaurusEntries { get; set; }
    }
}
