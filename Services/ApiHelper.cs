using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer.Services
{
    public class ApiHelper
    {
        private static HttpClient ApiClient { get; set; }

        private static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static async Task<HttpResponseMessage> GetResponseFromApiAsync(string url)
        {
            InitializeClient();
            return await ApiClient.GetAsync(url);
        }
    }
}