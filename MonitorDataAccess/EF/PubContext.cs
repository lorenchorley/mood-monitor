using Microsoft.EntityFrameworkCore;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MonitorDataAccess.EF;

public class PubContext : DbContext
{
    public PubContext(DbContextOptions<PubContext> options) : base(options)
    {
            
    }

    public DbSet<StatsEntry> Statistics { get; set; }
    public DbSet<LogEntry> Logs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=localhost\\IP16;Database=MoodMonitor;Integrated Security=True;TrustServerCertificate=true");
    //}
}