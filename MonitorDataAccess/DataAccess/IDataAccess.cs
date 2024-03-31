namespace MonitorDataAccess.DataAccess;

public interface IDataAccess<TDto>
{
    Task<List<TDto>> GetAll();
    Task Add(TDto entry);
}
