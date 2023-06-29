using System.Net.Http;
using GamesWebApi.DTO;
using Newtonsoft.Json;

namespace GamesWebApi.Tests;

public static class ConvertResponseHelper
{
    public static T? ToObject<T>(HttpResponseMessage response)
    {
        var body = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<T>(body);
    }
}