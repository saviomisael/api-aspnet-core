using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace GamesWebApi.Tests;

public static class ConvertRequestHelper
{
    public static StringContent ToJson<T>(T requestBody)
    {
        return new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
            MediaTypeNames.Application.Json);
    }
}