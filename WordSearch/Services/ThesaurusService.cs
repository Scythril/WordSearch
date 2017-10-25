using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WordSearch.Models;
using WordSearch.Models.Thesaurus;
using WordSearch.Services.Interfaces;

namespace WordSearch.Services
{
    public class ThesaurusService : IThesaurusService
    {
        private Uri endpoint = new Uri("http://thesaurus.altervista.org/thesaurus/v1");

        public async Task<IList<ThesaurusItem>> GetSynonymsAsync(AppConfiguration config, String search)
        {
            String language = "en_US";
            using (var client = new HttpClient())
            {
                client.BaseAddress = endpoint;
                var response = await client.GetAsync($"?key={HttpUtility.UrlEncode(config.ThesaurusApiKey)}&word={HttpUtility.UrlEncode(search.Trim())}&language={HttpUtility.UrlEncode(language)}&output=json");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ThesaurusResponse>(result);
                return list.Response.Select(x => x.List).ToList();
            }
        }
    }
}
