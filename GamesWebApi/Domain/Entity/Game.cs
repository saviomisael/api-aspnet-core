namespace Domain.Entity;

public class Game : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string UrlImage { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public virtual AgeRating? AgeRating { get; set; }
    public string? AgeRatingId { get; set; }
    public virtual ICollection<Genre>? Genres { get; set; }
    public virtual ICollection<Platform>? Platforms { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }

    public Game()
    { }
    
    public void AddGenre(Genre genre)
    {
        Genres ??= new List<Genre>();
        Genres.Add(genre);
    }

    public void AddPlatform(Platform platform)
    {
        Platforms ??= new List<Platform>();
        Platforms.Add(platform);
    }
}