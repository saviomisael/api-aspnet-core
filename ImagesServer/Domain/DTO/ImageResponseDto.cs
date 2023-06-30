namespace Domain.DTO;

public class ImageResponseDto
{
    public string Name { get; }
    public string Url { get; }

    public ImageResponseDto(string name, string url)
    {
        Name = name;
        Url = url;
    }
}