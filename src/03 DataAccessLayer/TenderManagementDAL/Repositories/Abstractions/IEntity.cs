namespace TenderManagementDAL.Repositories.Abstractions;

public interface IEntity : IEntity<int>
{
}

public interface IEntity<TId>
{
    TId Id { get; set; }
}