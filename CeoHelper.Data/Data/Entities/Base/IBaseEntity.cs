namespace CeoHelper.Data.Data.Entities.Base
{
	public interface IBaseEntity
	{
		long Id { get; set; }
		DateTime CreationDate { get; set; }
		DateTime? ModifyDate { get; set; }
	}
}
