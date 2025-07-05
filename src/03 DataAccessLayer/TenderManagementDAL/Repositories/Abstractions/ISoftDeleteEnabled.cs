namespace TenderManagementDAL.Repositories.Abstractions;

public interface ISoftDeleteEnabled : ISoftDeleteEnabled<int>, ISoftDeleteEnabledBase
{
}

public interface ISoftDeleteEnabled<T> : ISoftDeleteEnabledBase
{
    T Id { get; set; }
}