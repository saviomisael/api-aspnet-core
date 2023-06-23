namespace GamesWebApi;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = Root + "/" + Version;
    public static class GenreRoutes
    {
        public const string Create = Base + "/genres";
    }
}