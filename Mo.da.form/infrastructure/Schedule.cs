using Azure;
using Microsoft.Build.Framework.Profiler;
using System;
using System.Net.Http;

namespace MO.DA.FORM.infrastructure
{
    public class Schedule
    {
        public async Task GetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                object? data = await httpClient.GetAsync("https://e.mospolytech.ru/#/schedule/current");
            }
        }
    }
}