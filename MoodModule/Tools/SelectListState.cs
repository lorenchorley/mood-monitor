namespace MoodModule.Tools;

public class SelectListState
{

    private int[] _values = Array.Empty<int>();

    public IEnumerable<int> Get()
    {
        return _values;

    }

    public void Set(IEnumerable<int> value)
    {
        int[] enPlus = value.Except(_values).ToArray();

        if (enPlus.Any())
        {
            _values = enPlus;
        }
        else
        {
            _values = Array.Empty<int>();
        }

    }

    public int? GetUniqueValueOrDefault()
    {
        return _values.SingleOrDefault() - 1;
    }

}
