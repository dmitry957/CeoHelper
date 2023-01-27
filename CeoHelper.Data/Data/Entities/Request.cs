using CeoHelper.Data.Data.Entities.Base;
using CeoHelper.Data.Entities;

namespace CeoHelper.Data.Data.Entities
{
    public class Request : BaseEntity
    {
        public long Id { get; set; }

        public string Body { get; set; } = string.Empty;

        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }

        public int TokensUsed { get; set; }

        public bool IsLiked { get; set; }
    }
}
