﻿using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.ExampleData;
using MonitorDataAccess.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MonitorDataAccess.DataAccess;

public partial class ImportFromGoogleNotesDataAccess([FromKeyedServices("GoogleNotesData")] IEnumerable<ITextDataSource> dataSources) : IDataAccess<LogHistoryEntry>
{
    private Regex _logHeaderMatch = new Regex(@"(?<Date>\d{1,2}\/\d{1,2}\/\d{1,4})[^\n]*");

    public Task Add(LogHistoryEntry entry)
    {
        throw new NotImplementedException();
    }

    public async Task<List<LogHistoryEntry>> GetAll()
    {
        List<LogHistoryEntry> list = new();

        foreach (var dataSource in dataSources)
        {
            var entries = await dataSource.GetText();
            list.AddRange(ParseLogEntries(entries));
        }

        return list;
    }

    private IEnumerable<LogHistoryEntry> ParseLogEntries(string text)
    {
        var lines = text.Replace("\r", "").Split('\n');

        LogHistoryEntry? entry = null;
        foreach (var line in lines)
        {
            var match = _logHeaderMatch.Match(line);

            if (match.Success)
            {
                if (entry != null)
                {
                    FinaliseLogEntry(entry);
                    yield return entry;
                }

                entry = StartNewEntry(line, match);
            }
            else if (entry != null)
            {
                entry.NoteText += Environment.NewLine + line;
            }
        }

        if (entry != null)
        {
            FinaliseLogEntry(entry);
            yield return entry;
        }

    }

    private void FinaliseLogEntry(LogHistoryEntry entry)
    {
        entry.NoteText = entry.NoteText.Trim();
    }

    private static LogHistoryEntry StartNewEntry(string line, Match match)
    {
        LogHistoryEntry? entry;
        string dateValue = match.Groups["Date"].Value;
        DateTime parsedDate;
        if (!DateTime.TryParseExact(dateValue, "d/M/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) &&
            !DateTime.TryParseExact(dateValue, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
            //!DateTime.TryParseExact(dateValue, "d/M/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ||
            //!DateTime.TryParseExact(dateValue, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) 
            )
        {
                throw new FormatException("Invalid date format. Expected format: dd/MM/yy or dd/MM/yyyy");
        }

        entry = new LogHistoryEntry
        {
            Date = DateOnly.FromDateTime(parsedDate),
            NoteText = line.Substring(match.Length).Trim()
        };
        return entry;
    }
}