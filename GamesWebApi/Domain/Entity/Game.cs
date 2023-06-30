namespace Domain.Entity;

public class Game : AggregateRoot
{
    public string Name { get; }
    public string UrlImage { get; }
    public string Description { get; }
    public decimal Price { get; }
    public DateTime ReleaseDate { get; }
    public AgeRating AgeRating { get; }
    public ICollection<Genre> Genres { get; }
    public ICollection<Platform> Platforms { get; }

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