using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MonitorDataAccess.EF;

namespace MonitorDataAccess.DataAccess;

[AutoConstructor]
public partial class DBStatsDataAccess : IDataAccess<StatsEntry>
{
    private readonly PubContext _dbContext;

    public async Task<StatsEntry> Add(StatsEntry entry)
    {
        var addedEntity = await _dbContext.Statistics.AddAsync(entry);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }
    
    public async Task AddRange(IEnumerable<StatsEntry> entries)
    {
        await _dbContext.Statistics.AddRangeAsync(entries);
        _dbContext.SaveChanges();
    }

    public async Task<List<StatsEntry>> GetAll()
    {
        return await _dbContext.Statistics
                               .ToListAsync();
    }

}
