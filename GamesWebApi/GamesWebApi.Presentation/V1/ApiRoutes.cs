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
        public const string GetGameById = Base + "/games/{id}";
        public const string UpdateGameById = Base + "/games/{id}";
        public const string UpdateImage = Base + "/games/{id}";
        public const string DeleteGameById = Base + "/games/{id}";
        public const string AddReview = Base + "/games/{id}/reviews";
        public const string UpdateReview = Base + "/games/reviews/{id}";
    }
    
    public static class ReviewersRoutes
    {
        public const string CreateAccount = Base + "/reviewers";
        public const string Login = Base + "/reviewers/tokens";
        public const string DeleteAccount = Base + "/reviewers";
        public const string RefreshToken = Base + "/reviewers/tokens/refreshToken";
        public const string ReviewerInfo = Base + "/reviewers/{username}";
        public const string GamesByUsername = Base + "/reviewers/games";
    }
}