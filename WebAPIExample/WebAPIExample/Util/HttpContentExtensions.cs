using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

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