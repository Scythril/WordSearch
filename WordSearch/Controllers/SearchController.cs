using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WordSearch.Models;
using WordSearch.Services.Interfaces;

namespace WordSearch.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SearchController : BaseController
    {
        private readonly IThesaurusService thesaurusService;
        private readonly IDictionaryService dictionaryService;

        public SearchController(IOptions<AppConfiguration> options, IThesaurusService thesaurusService, IDictionaryService dictionaryService) : base(options)
        {
            this.thesaurusService = thesaurusService;
            this.dictionaryService = dictionaryService;
        }

        [HttpGet("{query}")]
        public async Task<SearchResult> Get(String query)
        {
            var thesaurus = await thesaurusService.GetSynonymsAsync(config, query);
            var dictionary = await dictionaryService.GetDefinitionAsync(config, query);
            return new SearchResult
            {
                ThesaurusEntries = thesaurus,
                DictionaryEntries = dictionary
            };
        }
    }
}