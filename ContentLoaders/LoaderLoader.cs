using AdditionalTiers.Towers;

namespace AdditionalTiers.ContentLoaders;
internal static class LoaderLoader {
    private static readonly Dictionary<Type, Loader> _loaders = new();

    internal static Loader<T> GetLoaderInstance<T>() {
        return (Loader<T>)_loaders[typeof(T)];
    }
    
    internal static void Initialize(MelonAssembly masm) {
        foreach (var type in masm.Assembly.GetValidTypes()) {
            if (!type.IsAbstract && type.IsAssignableTo(typeof(Loader))) {
                _loaders[type.BaseType.GenericTypeArguments[0]] = (Loader)Activator.CreateInstance(type);
            }
        }
    }
}