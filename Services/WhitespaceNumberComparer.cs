using System;
using System.IO;
using System.Threading.Tasks;
using TextAnalyzer.Interfaces;

namespace TextAnalyzer.Services
{
    public class WhitespaceNumberComparer : ITextAnalyzer
    {
        public async Task<string> Analyze(string textToAnalyze)
        {
            //Read data from sample text file
            string textFromFile = await File.ReadAllTextAsync("sample.txt");
            
            //Compare the number of whitespaces in user's and sample texts
            string comparisonWord = WhitespaceCounter(textToAnalyze) > WhitespaceCounter(textFromFile) ? " more" : " less";

            //Return the difference of words numbers
            return "The text has " 
                   + Math.Abs(WhitespaceCounter(textToAnalyze) - WhitespaceCounter(textFromFile)) + comparisonWord 
                   + " words than sample text";
        }

        private int WhitespaceCounter(string text)
        {
            int whitespaceNumber = 0;
            
            //Break the text by each character and count the number of whitespaces
            foreach (var c in text.ToCharArray())
            {
                if (char.IsWhiteSpace(c))
                {
                    whitespaceNumber++;
                }
            }

            return whitespaceNumber;
        }
    }
}