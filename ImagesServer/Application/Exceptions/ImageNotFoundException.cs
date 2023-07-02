namespace Application.Exceptions;

public class ImageNotFoundException : Exception
{
    public ImageNotFoundException(string imageName) : base($"Image {imageName} not found.")
    {
        
    }
}