using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MonitorDataAccess.EF;

namespace MonitorDataAccess.DataAccess;

[AutoConstructor]
public partial class DBStatsDataAccess : IDataAccess<StatsEntry>
{
    private readonly PubContext _appDbContext;

    public async Task<StatsEntry> Add(StatsEntry entry)
    {
        var addedEntity = await _appDbContext.Statistics.AddAsync(entry);
        _appDbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public async Task<List<StatsEntry>> GetAll()
    {
        return await _appDbContext.Statistics.ToListAsync();
    }

}
