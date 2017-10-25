using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordSearch.Models;
using WordSearch.Models.Thesaurus;

namespace WordSearch.Services.Interfaces
{
    public interface IThesaurusService
    {
        Task<IList<ThesaurusItem>> GetSynonymsAsync(AppConfiguration config, String search);
    }
}
