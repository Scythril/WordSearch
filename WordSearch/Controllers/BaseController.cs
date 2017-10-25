using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WordSearch.Models;

namespace WordSearch.Controllers
{
    public abstract class BaseController : Controller
    {
        internal readonly AppConfiguration config;

        public BaseController(IOptions<AppConfiguration> options)
        {
            config = options.Value;
        }
    }
}
