namespace AdditionalTiers.ContentLoaders;
internal static class LoaderLoader {
    private static readonly Dictionary<Type, Loader> _loaders = new();

    internal static Loader<T> GetLoaderInstance<T>() {
        return (Loader<T>)_loaders[typeof(T)];
    }
    
    internal static void Initialize(MelonAssembly melonAssembly) {
        foreach (var type in melonAssembly.Assembly.GetValidTypes()) {
            if (type.IsAbstract || !type.IsAssignableTo(typeof(Loader))) continue;
            if (type.BaseType != null)
                _loaders[type.BaseType.GenericTypeArguments[0]] = (Loader)Activator.CreateInstance(type);
        }
    }
}