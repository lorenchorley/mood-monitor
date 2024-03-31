using MonitorDataAccess.DataAccess;

namespace MonitorDataAccess.Tests;

[TestClass]
public class GoogleNotesImportTests
{
    [TestMethod]
    public async Task TestSampleLog()
    {
        // Arrange
        // =======
        string text = """
            01/01/24

            some log text


            22/01/24

            some more log text
            on several lines

            with big gaps

            
            """;

        var dataSource = new InMemoryTextDataSource(text);
        var dataAccess = new ImportFromGoogleNotesDataAccess(new ITextDataSource[] { dataSource });

        // Act
        // ===

        var entries = await dataAccess.GetAll();

        // Assert
        // ======

        Assert.AreEqual(2, entries.Count);

        Assert.AreEqual(entries[0].Date, new DateOnly(2024, 01, 01));
        Assert.AreEqual(entries[0].NoteText, "some log text");

        Assert.AreEqual(entries[0].Date, new DateOnly(2024, 01, 22));
        Assert.AreEqual(entries[0].NoteText, """
            some more log text
            on several lines

            with big gaps
            """);

    }
    
}