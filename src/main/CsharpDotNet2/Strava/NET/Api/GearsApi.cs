using System;
using System.Collections.Generic;
using RestSharp;
using Strava.NET.Client;
using Strava.NET.Model;

namespace Strava.NET.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IGearsApi
    {
        /// <summary>
        /// Get Equipment Returns an equipment using its identifier.
        /// </summary>
        /// <param name="id">The identifier of the gear.</param>
        /// <returns>DetailedGear</returns>
        DetailedGear GetGearById (int? id);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class GearsApi : IGearsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GearsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public GearsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="GearsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public GearsApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public string GetBasePath(String basePath)
        {
            return ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get Equipment Returns an equipment using its identifier.
        /// </summary>
        /// <param name="id">The identifier of the gear.</param> 
        /// <returns>DetailedGear</returns>            
        public DetailedGear GetGearById (int? id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetGearById");
            
    
            var path = "/gear/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
                                                    
            // authentication setting, if any
            var authSettings = new string[] { "strava_oauth" };
    
            // make the HTTP request
            var response = (RestResponse) ApiClient.CallApi(path, Method.Get, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetGearById: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetGearById: " + response.ErrorMessage, response.ErrorMessage);
    
            return (DetailedGear) ApiClient.Deserialize(response.Content, typeof(DetailedGear), response.Headers);
        }
    
    }
}
