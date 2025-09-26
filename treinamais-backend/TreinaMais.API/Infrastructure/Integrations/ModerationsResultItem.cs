namespace EducaDev.API.Infrastructure.Integrations
{
    public class ModerationsResultItem
    {
        public bool flagged { get; set; }
        public Dictionary<string, bool> categories { get; set; }
        public Dictionary<string, double> category_scores { get; set; }
    }
}
