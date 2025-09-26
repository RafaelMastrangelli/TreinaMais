namespace EducaDev.API.Infrastructure.Integrations
{
    public class Output
    {
        public string type { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string role { get; set; }
        public Content[] content { get; set; }
    }
}
