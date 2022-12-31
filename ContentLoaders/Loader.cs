namespace AdditionalTiers.ContentLoaders;
internal abstract class Loader {
    internal virtual void Load(MelonAssembly masm) {
        throw new NotImplementedException();
    }
}

internal abstract class Loader<T> : Loader {
    private readonly List<T> loaded = new();

    internal void Add(T item) {
        MelonDebug.Msg($"Registered {item.GetType().Name}.");
        loaded.Add(item);
    }

    internal List<T> Get() {
        return loaded.ToImmutableList<T>().ToList();
    }
}