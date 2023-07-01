using System.Net;
using System.Net.Http.Headers;
using Domain.DTO;
using Infrastructure.ImagesServerApi.Contracts;
using Newtonsoft.Json;

namespace Infrastructure.ImagesServerApi;

public class ImagesServerApiClient : IImagesServerApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ImagesServerApiClient(IHttpClientFactory factory)
    {
        _httpClientFactory = factory;
    }

    public async Task<ImageResponseDto?> PostImageAsync(Stream imageStream, string contentType, string imageName)
    {
        var httpClient = _httpClientFactory.CreateClient("ImagesServer");

        var imageContent = new StreamContent(imageStream);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        using MultipartFormDataContent multipartContent = new();
        multipartContent.Add(imageContent, "image", imageName);

        using var response = await httpClient.PostAsync("images", multipartContent);

        if (response.StatusCode != HttpStatusCode.Created) return null;

        var body = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<ImageResponseDto>(body);
    }
}