namespace EducaDev.API.Infrastructure.Integrations
{
    public class ModerationsApiResponse
    {
        public string id { get; set; }
        public string model { get; set; }
        public List<ModerationsResultItem> results { get; set; }
    }
}
