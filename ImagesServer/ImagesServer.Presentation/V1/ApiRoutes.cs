namespace ImagesServer.V1;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = Root + "/" + Version;
    
    public static class Images
    {
        public const string CreateImage = Base + "/images";
        public const string GetImage = Base + "/images/{name}";
    }
}