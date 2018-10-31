using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPIExample.Util;

namespace WebAPIExample.ActionFilters
{
    public class PaginationAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string[] paginationHeaders = new string[] { "x-sort-param", "x-sort-direction", "x-page-start", "x-page-size" };
            /* Headers Example
             * x-sort-param: <nome do atributo>
             * x-sort-direction: <desc> ou <asc> 
             * x-page-start:10
             * x-page-size: 25
             */

        private string SortParam { get; set; }
        private string SortDirection { get; set; }
        private int PageStart { get; set; }
        private int PageSize { get; set; }
        
        /// <summary>
        /// The object type of the list to be filtered
        /// </summary>
        private Type ObjectType { get; }

        public PaginationAttribute(Type objectType)
        {
            ObjectType = objectType;
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {

            #region Inital Validation Logic


            // Check if there are the pagination headers in the request, 
            bool doPagination = paginationHeaders.ToList().All(x => actionContext.Request.Headers.Contains(x));

            continuation().Wait();
            // If there isn't just return the response to the web api flow
            if (!doPagination) return continuation();


            #endregion

            #region Get HttpContent Data Logic

            // Get the values of the pagination headers request, and the HttpResponseMessage
            SortParam = actionContext.Request.Headers.GetValues(paginationHeaders[0]).First();
            SortDirection = actionContext.Request.Headers.GetValues(paginationHeaders[1]).First();
            PageStart = Convert.ToInt32(actionContext.Request.Headers.GetValues(paginationHeaders[2]).First());
            PageSize = Convert.ToInt32(actionContext.Request.Headers.GetValues(paginationHeaders[3]).First());

            if (!(SortDirection == "asc" || SortDirection == "dsc")) return continuation();
            
            HttpResponseMessage response = continuation().Result;
            HttpContent content = response.Content;

            // Get and deserialize the data response of the annotated action
            string jsonString = @content.GetValue();
            dynamic deserialized = JsonConvert.DeserializeObject(jsonString, ObjectType);


            // Hold the all data that has the SortParam property name in a dynamic list
            var objsFound = new List<dynamic>(deserialized)
                                    .Where(x => x.GetType().GetProperty(SortParam) != null).ToList();
            // If there isn't any data return the response to the flow
            if (objsFound.Count == 0) return continuation();


            #endregion

            #region Filter Data Logic


            // The pagination logic
            objsFound = SortDirection.Equals("asc") ?
                objsFound.OrderBy(x => x.GetType().GetProperty(SortParam).GetValue(x, null))
                         .Skip(PageSize <= 1 ? PageStart * PageSize - 1 : (PageStart * PageSize) - PageSize)
                         .Take(PageSize).ToList() :
                objsFound.OrderByDescending(x => x.GetType().GetProperty(SortParam).GetValue(x, null))
                        .Skip(PageSize <= 1 ? PageStart * PageSize - 1 : (PageStart * PageSize) - PageSize)
                        .Take(PageSize).ToList();

            #endregion

            #region Return Data Logic

            // Serialize the filtered data
            var json = JsonConvert.SerializeObject(objsFound);

            // Clean the response content
            response.Content.Dispose();

            // Generate a StringContent with the json
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Set the response content with the generated string content
            response.Content = stringContent;

            // Return with the filtered data
            return Task.Run(() => response);

            #endregion
        }
    }
    
}