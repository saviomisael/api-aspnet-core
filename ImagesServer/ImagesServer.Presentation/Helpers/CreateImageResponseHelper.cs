using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace ImagesServer.Helpers;

public static class CreateImageResponseHelper
{
    public static HttpResponseMessage ReturnImage(byte[] imageBytes)
    {
        var result = new HttpResponseMessage(HttpStatusCode.OK);
        result.Content = new ByteArrayContent(imageBytes);
        result.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

        return result;
    }
}