using System.Threading.Tasks;

namespace TextAnalyzer.Interfaces
{
    public interface ITextAnalyzer
    {
        public Task<string> Analyze(string textToAnalyze);
    }
}