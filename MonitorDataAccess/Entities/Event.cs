namespace MonitorDataAccess.ExampleData;

public class Event
{
    public int categoryID { get; set; }
    public string categoryName { get; set; }
    public bool custom { get; set; }
    public int customeventid { get; set; }
    public E2ec e2ec { get; set; }
    public int eventid { get; set; }
    public string icon { get; set; }
    public int id { get; set; }
    public bool isDeleted { get; set; }
    public bool isExists { get; set; }
    public bool isSelected { get; set; }
    public string name { get; set; }
    public string nameAlias1 { get; set; }
    public string nameAlias2 { get; set; }
    public bool onlyIcon { get; set; }
    public int order { get; set; }
    public float usageDate { get; set; }
    public string usagedatetimezoneid { get; set; }
}
