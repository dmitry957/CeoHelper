namespace CeoHelper.Shared.Models.Request
{
    public class SearchRequestModel
    {
        public string Language { get; set; } = string.Empty;
        public int TextSize { get; set; }
        public string Keywords { get; set; } = string.Empty;
        public int KeywordDensity { get; set; }
        public string Personalization { get; set; } = string.Empty;
        public int Tokens { get; set; }
    }
}
