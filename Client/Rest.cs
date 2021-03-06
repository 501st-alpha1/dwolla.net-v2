﻿using System;
using System.Web;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using DwollaV2.SerializableTypes;
using System.Net.Http.Headers;

namespace DwollaV2
{
  /// <summary>
  ///   Handles POST and GET requests, as well as serialization
  /// </summary>
  public class Rest
  {
    /// <summary>
    ///   An instance of the configuration class, which wraps around
    ///   ConfigurationManager
    /// </summary>
    public Config C = new Config();

    /// <summary>
    ///   WCF serializer
    /// </summary>
    public JavaScriptSerializer Jss = new JavaScriptSerializer();

    /// <summary>
    ///   Fully parses result out of Dwolla envelope into easily
    ///   usable serializable type. Verifies response
    ///   and raises error if API exception encountered.
    /// </summary>
    /// <typeparam name="T">Type of serializable data</typeparam>
    /// <param name="response">JSON response string</param>
    /// <returns>
    ///   Can either be a single object or a serializable
    ///   type as a part of a collection
    /// </returns>
    protected DwollaResponse<T> DwollaParse<T>(HttpResponseMessage response)
    {
      string content = response.Content.ReadAsStringAsync().Result;

      if (response.IsSuccessStatusCode)
      {
        DwollaResponse<T> result = new DwollaResponse<T>();
        result.Success = response.IsSuccessStatusCode;
        result.Location = response.Headers.Location;
        result.Response = Jss.Deserialize<T>(content);

        return result;
      }
      else
      {
        ErrorResponse errorResponse = Jss.Deserialize<ErrorResponse>(content);
        string error = errorResponse.Code + ": " + errorResponse.Message;

        throw new ApiException(error, errorResponse);
      }
    }

    /// <summary>
    ///   Synchronous POST request wrapper around HttpClient
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage Post(string endpoint, Dictionary<string, string> parameters)
    {
      using (var client = new HttpClient())
      {
        try
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameters["oauth_token"]);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

          HttpResponseMessage response = client.PostAsync(
            (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
            + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;

          return response;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a POST request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }

    /// <summary>
    ///   A variation of the POST method wherein the only difference
    ///   is that we post a Dictionary<string,object>. Used for MassPay
    ///   and off-site gateway checkouts.
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage PostSpecial(string endpoint, Dictionary<string, object> parameters)
    {
      using (var client = new HttpClient())
      {
        try
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)parameters["oauth_token"]);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

          HttpResponseMessage response = client.PostAsync(
            (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
            + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;

          return response;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a POST request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }

    /// <summary>
    ///   Synchronous GET request wrapper around HttpClient
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage Get(string endpoint, Dictionary<string, string> parameters)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameters["oauth_token"]);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

        var builder = new UriBuilder(
          (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
          + endpoint);

        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

        foreach (string k in parameters.Keys)
          query[k] = parameters[k];

        builder.Query = query.ToString();

        try
        {
          return client.GetAsync(builder.Uri).Result;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a POST request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }

    /// <summary>
    ///   Synchronous PUT request wrapper around HttpClient
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage Put(string endpoint, Dictionary<string, string> parameters)
    {
      using (var client = new HttpClient())
      {
        try
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameters["oauth_token"]);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

          HttpResponseMessage response = client.PutAsync(
            (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
            + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;

          return response;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a PUT request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }

    /// <summary>
    ///   A variation of the PUT method wherein the only difference
    ///   is that we post a Dictionary<string,object>. Used for MassPay
    ///   and off-site gateway checkouts.
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage PutSpecial(string endpoint, Dictionary<string, object> parameters)
    {
      using (var client = new HttpClient())
      {
        try
        {
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)parameters["oauth_token"]);
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

          HttpResponseMessage response = client.PutAsync(
            (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
            + endpoint, new StringContent(Jss.Serialize(parameters), Encoding.UTF8, "application/json")).Result;

          return response;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a PUT request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }

    /// <summary>
    ///   Synchronous DELETE request wrapper around HttpClient
    /// </summary>
    /// <param name="endpoint">Dwolla API endpoint</param>
    /// <param name="parameters">A Dictionary with the parameters</param>
    /// <returns>JSON-encoded string with API response</returns>
    protected HttpResponseMessage Delete(string endpoint, Dictionary<string, string> parameters)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameters["oauth_token"]);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(C.dwolla_accept));

        var builder = new UriBuilder(
          (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
          + endpoint);

        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

        foreach (string k in parameters.Keys)
          query[k] = parameters[k];

        builder.Query = query.ToString();

        try
        {
          return client.GetAsync(builder.Uri).Result;
        }
        catch (Exception wtf)
        {
          Console.WriteLine("dwolla.net: An exception has occurred while making a DELETE request.");
          Console.WriteLine(wtf.ToString());
          return null;
        }
      }
    }
  }
}
