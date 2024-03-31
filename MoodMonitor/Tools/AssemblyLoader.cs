public static class AssemblyLoader
{
    public static IEnumerable<Type> GetTypesWithInterface<T>()
    {
        var type = typeof(T);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
    }
}
