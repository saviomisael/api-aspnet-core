namespace Domain.Entity;

public class Game : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string UrlImage { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public AgeRating AgeRating { get; set; } = null!;
    public string? AgeRatingId { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Platform> Platforms { get; set; } = new List<Platform>();

    public Game()
    { }
    
    public Game(string name, string urlImage, string description, decimal price, DateTime releaseDate, AgeRating ageRating)
    {
        Name = name;
        UrlImage = urlImage;
        Description = description;
        Price = price;
        ReleaseDate = releaseDate;
        AgeRating = ageRating;
        Genres = new List<Genre>();
        Platforms = new List<Platform>();
    }

    public void AddGenre(Genre genre)
    {
        Genres.Add(genre);
    }

    public void AddPlatform(Platform platform)
    {
        Platforms.Add(platform);
    }
}