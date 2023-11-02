namespace DataAccess.IRepositories
{
    public interface IEntity
    {
        int Id { get; set; }
        string UserId { get; set; }
    }
}