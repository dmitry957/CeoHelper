namespace CeoHelper.Shared.Models.Request
{
    public class SearchResultModel
    {
        public string GeneratedContent { get; set; } = string.Empty;
        public int AvailableTokens { get; set; }
        public long RequestId { get; set; }
    }
}
