﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lessons._02
{
    /// <summary>
    /// Declare an interface ISiteReader with a method ReadAsync(string requestUrl) that returns string in asynchronous way.
    /// Implement the interface and keep the asynchronous approach.
    /// Use await to call the method with "http://www.google.com" parameter.
    /// Print out the content of the page.
    /// </summary>
    public class TaskE
    {
        public static void Run()
        {
            SiteReader reader = new SiteReader();
            Task<string> result = reader.ReadAsync("http://www.google.com");

            Console.WriteLine(result.Result);
        }
    }

    interface ISiteReader
    {
        Task<string>  ReadAsync(string requestUrl);
    }

     class SiteReader : ISiteReader
    {
        public async Task<string> ReadAsync(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(request);
            }
        }
    }
}