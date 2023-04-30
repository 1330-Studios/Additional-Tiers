namespace AdditionalTiers.Utils.Extensions;
internal static class TextureExtensions {
    internal static Texture2D ToTexture(this byte[] bytes) {
        var Tex2D = new Texture2D(2, 2);
        return ImageConversion.LoadImage(Tex2D, bytes) ? Tex2D : null;
    }
}
