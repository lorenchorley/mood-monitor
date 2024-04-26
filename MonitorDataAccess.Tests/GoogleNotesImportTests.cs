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

        Assert.AreEqual(new DateOnly(2024, 01, 01), entries[0].Date);
        Assert.AreEqual("", entries[0].Annotation);
        Assert.AreEqual("some log text", entries[0].NoteText);

        Assert.AreEqual(new DateOnly(2024, 01, 22), entries[1].Date);
        Assert.AreEqual("", entries[1].Annotation);
        Assert.AreEqual("""
            some more log text
            on several lines

            with big gaps
            """, entries[1].NoteText);

    }

    [TestMethod]
    public async Task TestSampleLog_WithAnnotation()
    {
        // Arrange
        // =======
        string text = """
            Ps 01/01/24

            some log text


            R 22/01/24

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

        Assert.AreEqual(new DateOnly(2024, 01, 01), entries[0].Date);
        Assert.AreEqual("Ps", entries[0].Annotation);
        Assert.AreEqual("some log text", entries[0].NoteText);

        Assert.AreEqual(new DateOnly(2024, 01, 22), entries[1].Date);
        Assert.AreEqual("R", entries[1].Annotation);
        Assert.AreEqual("""
            some more log text
            on several lines

            with big gaps
            """, entries[1].NoteText);

    }
    
}