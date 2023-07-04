using System.Net;
using System.Net.Http.Headers;
using Domain.DTO;
using Infrastructure.ImagesServerApi.Contracts;
using Infrastructure.ImagesServerApi.Options;
using Newtonsoft.Json;

namespace Infrastructure.ImagesServerApi;

public class ImagesServerApiClient : IImagesServerApiClient
{
    private readonly HttpClient _client;

    public ImagesServerApiClient(HttpClient client, ImagesServerOptions options)
    {
        _client = client;
        _client.BaseAddress = new Uri(options.BaseUrl);
    }

    public async Task<ImageResponseDto?> PostImageAsync(byte[] imageBytes, string contentType, string imageName)
    {
        var imageContent = new ByteArrayContent(imageBytes);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        using MultipartFormDataContent multipartContent = new();
        multipartContent.Add(imageContent, "image", imageName);

        try
        {
            using var response = await _client.PostAsync("images", multipartContent);

            if (response.StatusCode != HttpStatusCode.Created) return null;

            var body = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ImageResponseDto>(body);
        }
        catch (HttpRequestException e)
        {
            return null;
        }
    }

    public async Task DeleteImageAsync(string imageName)
    {
        try
        {
            await _client.DeleteAsync($"images/{imageName}");
        }
        catch (HttpRequestException)
        {
        }
    }
}