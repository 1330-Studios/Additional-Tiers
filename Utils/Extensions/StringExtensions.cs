#nullable enable
namespace AdditionalTiers.Utils.Extensions;
internal static class StringExtensions {
    public static Dictionary<string, SpriteReference> Sprites = new();
    public static Dictionary<string, Stream> Resources = new();

    public static byte[]? GetEmbeddedResource(this string path) {
        if (Resources.TryGetValue(path, out var resource)) {
            return GetBytesFromStream(resource);
        }

        var manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        return manifestResourceStream == null ? Array.Empty<byte>() : GetBytesFromStream(Resources[path] = manifestResourceStream);
    }

    private static byte[] GetBytesFromStream(Stream stream) {
        stream.Seek(0, SeekOrigin.Begin);

        var task = Task.Run(async () => await ConvertToBytes(stream, CancellationToken.None));

        if (task.Exception != null)
            MelonLogger.Msg(task.Exception.Message);

        return task.Result;
    }

    private static async Task<byte[]> ConvertToBytes(Stream stream, CancellationToken token) {
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms, token);
        return ms.ToArray();
    }

    public static SpriteReference GetSpriteReference(this string name) {
        if (Sprites.TryGetValue(name, out var reference))
            return reference;

        var sr = new SpriteReference() { guidRef = name };
        Sprites[name] = sr;
        return sr;
    }
}
