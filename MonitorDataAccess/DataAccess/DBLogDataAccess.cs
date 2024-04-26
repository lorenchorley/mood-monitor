using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MonitorDataAccess.EF;

namespace MonitorDataAccess.DataAccess;

[AutoConstructor]
public partial class DBLogDataAccess : IDataAccess<LogEntry>
{
    private readonly PubContext _appDbContext;

    public async Task<LogEntry> Add(LogEntry entry)
    {
        var addedEntity = await _appDbContext.Logs.AddAsync(entry);
        _appDbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public async Task<List<LogEntry>> GetAll()
    {
        return await _appDbContext.Logs.ToListAsync();
    }

}
