namespace EducaDev.API.Infrastructure.Integrations;

public class ModerationImageRequest
{
    public string model { get; set; }
    public Input[] input { get; set; }
}

public class Input
{
    public string type { get; set; }
    public string text { get; set; }
    public Image_url image_url { get; set; }
}

public class Image_url
{
    public string url { get; set; }
}

