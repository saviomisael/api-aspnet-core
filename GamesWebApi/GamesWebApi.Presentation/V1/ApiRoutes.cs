namespace GamesWebApi.V1;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = Root + "/" + Version;
    public static class GenreRoutes
    {
        public const string Create = Base + "/genres";
        public const string GetAll = Base + "/genres";
        public const string DeleteByName = Base + "/genres/{name}";
    }

    public static class PlatformRoutes
    {
        public const string Create = Base + "/platforms";
        public const string GetAll = Base + "/platforms";
        public const string DeleteByName = Base + "/platforms/{name}";
    }

    public static class AgeRatingRoutes
    {
        public const string GetAll = Base + "/age-ratings";
    }

    public static class GameRoutes
    {
        public const string CreateGame = Base + "/games";
    }
}