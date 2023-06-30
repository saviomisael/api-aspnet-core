namespace Domain.Entity;

public class Image
{
    public string Id { get; }
    public string Name { get; private set; }
    public string Extension { get; }
    public byte[] Content { get; }

    public Image(string extension, byte[] content)
    {
        Extension = extension;
        Content = content;
        Id = Guid.NewGuid().ToString();
        Name = Guid.NewGuid().ToString();
    }

    public void GenerateNewName()
    {
        Name = Guid.NewGuid().ToString();
    }
}