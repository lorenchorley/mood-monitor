namespace MonitorDataAccess.DataAccess;

public interface IDataAccess<Entity>
{
    Task<List<Entity>> GetAll();
    Task<Entity> Add(Entity entry);
}
