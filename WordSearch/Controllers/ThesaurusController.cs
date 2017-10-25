using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WordSearch.Models;
using WordSearch.Services.Interfaces;
using WordSearch.Models.Thesaurus;

namespace WordSearch.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ThesaurusController : BaseController
    {
        private readonly IThesaurusService thesaurusService;

        public ThesaurusController(IOptions<AppConfiguration> options, IThesaurusService thesaurusService) : base(options)
        {
            this.thesaurusService = thesaurusService;
        }

        [HttpGet("{search}")]
        public async Task<IList<ThesaurusItem>> Get(String search)
        {
            return await thesaurusService.GetSynonymsAsync(config, search);
        }
    }
}