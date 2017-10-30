using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordSearch.Models;
using WordSearch.Models.Dictionary;

namespace WordSearch.Services.Interfaces
{
    public interface IDictionaryService
    {
        Task<IList<DictionaryEntry>> GetDefinitionAsync(AppConfiguration config, String query);
    }
}
