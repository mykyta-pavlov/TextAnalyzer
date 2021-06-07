using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TextAnalyzer.ApiResponses;
using TextAnalyzer.Interfaces;

namespace TextAnalyzer.Services
{
    public class PronounsPercentage : ITextAnalyzer
    {
        private readonly IConfiguration _config;

        public PronounsPercentage(IConfiguration config)
        {
            _config = config;
        }
        
        public async Task<string> Analyze(string textToAnalyze)
        {
            //Split user's text by specified characters
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] splitText = textToAnalyze.Split(delimiterChars);
            
            int numberOfWords = splitText.Length;
            int numberOfPronouns = 0;

            //Go through each user's word and send it to external API to evaluate
            foreach (var word in splitText)
            {
                //Connection string with current word
                string url = _config["DictionaryAPI:ConnectionString"] + word;
            
                //Send GET request to Dictionary API
                using (HttpResponseMessage response = await ApiHelper.GetResponseFromApiAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //To JSON
                        string res = await response.Content.ReadAsStringAsync();

                        //JSON to objects
                        var dictionaryApiResponse = JsonSerializer.Deserialize<List<DictionaryApiResponse>>(res);
                        
                        //Go through the object and add 1 if the word is a pronoun
                        foreach (var apiResponse in dictionaryApiResponse)
                        {
                            foreach (var meaning in apiResponse.meanings)
                            {
                                if (meaning.partOfSpeech.Contains("pronoun"))
                                {
                                    numberOfPronouns++;
                                }
                            }
                        }
                    }
                }
            }
            return "The text contains " + 100 * numberOfPronouns / numberOfWords + " percent of pronouns";
        }
    }
}