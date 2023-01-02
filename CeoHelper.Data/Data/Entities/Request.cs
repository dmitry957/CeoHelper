using CeoHelper.Data.Entities;

namespace CeoHelper.Data.Data.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public string Body { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }

        public int TokensUsed { get; set; }
    }
}
