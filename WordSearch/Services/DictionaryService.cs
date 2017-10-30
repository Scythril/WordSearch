using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using WordSearch.Models;
using WordSearch.Models.Dictionary;
using WordSearch.Services.Interfaces;

namespace WordSearch.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly Uri endpoint = new Uri("http://www.dictionaryapi.com/api/v1/references/collegiate/xml/");

        public async Task<IList<DictionaryEntry>> GetDefinitionAsync(AppConfiguration config, string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = endpoint;
                var response = await client.GetAsync($"{HttpUtility.UrlEncode(query.Trim())}?key={HttpUtility.UrlEncode(config.DictionaryApiKey)}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var doc = XDocument.Parse(result);
                var results = new List<DictionaryEntry>();
                foreach (var el in doc.Root.Elements("entry"))
                {
                    var de = new DictionaryEntry
                    {
                        Headword = el.Descendants("hw").FirstOrDefault()?.Value,
                        Wav = el.Descendants("sound").FirstOrDefault()?.Descendants("wav").FirstOrDefault()?.Value,
                        FunctionalLabel = el.Descendants("fl").FirstOrDefault()?.Value
                    };
                    var def = el.Descendants("def").FirstOrDefault();
                    if (def != null)
                    {
                        de.Date = def.Descendants("date").FirstOrDefault()?.Value;
                        foreach (var desc in def.Descendants())
                        {
                            if (desc.Name == "dt")
                            {
                                var definition = new Definition
                                {
                                    SenseNumber = desc.ElementsBeforeSelf("sn").LastOrDefault()?.Value,
                                    DefiningText = desc.Value
                                };
                                de.Definitions.Add(definition);
                            }
                        }
                    }
                    results.Add(de);
                }
                return results;
            }
        }
    }
}
