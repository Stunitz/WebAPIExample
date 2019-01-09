using System.Net.Http;

namespace WebAPIExample.Util
{
    static class HttpContentExtensions
    {
        public static string GetValue(this HttpContent self)
        {
            string value = self.ReadAsStringAsync().Result;
            return value;
            
        }
        
    }
}