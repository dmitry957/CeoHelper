namespace CeoHelper.Data.Data.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        protected BaseEntity()
        {
            DateTime now = DateTime.UtcNow;
            CreationDate = new DateTime(now.Ticks / 100000 * 100000, now.Kind);
        }
    }
}
