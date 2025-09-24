namespace EducaDev.API.Infrastructure.Integrations;

public class LeonardoModels
{
    
}
public class LeonardoGenerationResponse
{
    public SdGenerationJob sdGenerationJob { get; set; }
}

public class SdGenerationJob
{
    public string generationId { get; set; }
    public int apiCreditCost { get; set; }
}

public class LeonardoGetGenerationResponse
{
    public Generations_by_pk generations_by_pk { get; set; }
}

public class Generations_by_pk
{
    public Generated_images[] generated_images { get; set; }
    public string modelId { get; set; }
    public object motion { get; set; }
    public object motionModel { get; set; }
    public object motionStrength { get; set; }
    public string prompt { get; set; }
    public string negativePrompt { get; set; }
    public int imageHeight { get; set; }
    public object imageToVideo { get; set; }
    public int imageWidth { get; set; }
    public int inferenceSteps { get; set; }
    public long seed { get; set; }
    public object ultra { get; set; }
    public bool @public { get; set; }
    public string scheduler { get; set; }
    public string sdVersion { get; set; }
    public string status { get; set; }
    public object presetStyle { get; set; }
    public object initStrength { get; set; }
    public double guidanceScale { get; set; }
    public string id { get; set; }
    public string createdAt { get; set; }
    public bool promptMagic { get; set; }
    public object promptMagicVersion { get; set; }
    public object promptMagicStrength { get; set; }
    public bool photoReal { get; set; }
    public object photoRealStrength { get; set; }
    public object fantasyAvatar { get; set; }
    public Prompt_moderations[] prompt_moderations { get; set; }
    public object[] generation_elements { get; set; }
}

public class Generated_images
{
    public string url { get; set; }
    public bool nsfw { get; set; }
    public string id { get; set; }
    public int likeCount { get; set; }
    public object motionMP4URL { get; set; }
    public object[] generated_image_variation_generics { get; set; }
}

public class Prompt_moderations
{
    public object[] moderationClassification { get; set; }
}

