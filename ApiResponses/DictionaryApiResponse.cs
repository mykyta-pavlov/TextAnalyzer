namespace TextAnalyzer.ApiResponses
{
    public class DictionaryApiResponse
    {
        public string word { get; set; }
        public Meaning[] meanings { get; set; }

        public class Meaning
        {
            public string partOfSpeech { get; set; }
        }
    }
}