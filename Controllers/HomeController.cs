using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Interfaces;

namespace TextAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IEnumerable<ITextAnalyzer> _textAnalyzers;

        public HomeController(IEnumerable<ITextAnalyzer> textAnalyzers)
        {
            _textAnalyzers = textAnalyzers;
        }

        [HttpPost]
        public async Task<List<string>> Post([FromBody] string text)
        {
            var result = new List<string>();

            //Go through each implementation of ITextAnalyzer and call Analyze method
            foreach (var textAnalyzer in _textAnalyzers)
            {
                result.Add(await textAnalyzer.Analyze(text));
            }
            return result;
        }
    }
}