using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using MonitorDataAccess.EF;

namespace MonitorDataAccess.DataAccess;

[AutoConstructor]
public partial class DBLogDataAccess : IDataAccess<LogEntry>
{
    private readonly PubContext _dbContext;

    public async Task<LogEntry> Add(LogEntry entry)
    {
        // Transformer le texte en clair en chriffé
        entry.UpdateEncryptedText();

        var addedEntity = await _dbContext.Logs.AddAsync(entry);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public async Task AddRange(IEnumerable<LogEntry> entries)
    {
        await _dbContext.Logs.AddRangeAsync(entries);
        _dbContext.SaveChanges();
    }

    public async Task<List<LogEntry>> GetAll()
    {
        return await _dbContext.Logs
                                  .Select(e => e.UpdateDecryptedText())
                                  .ToListAsync();
    }

}
