namespace AdditionalTiers.Utils;
internal class Singleton<T> where T : new() {
    private static readonly Lazy<T> lazy = new(() => new T());

    public static T Instance { get => lazy.Value; }

    internal Singleton() { }
}
