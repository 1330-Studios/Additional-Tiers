namespace AdditionalTiers.Utils.Extensions;
internal static class NumberExtensions {
    public static float Map(this float value, float fromLow, float fromHigh, float toLow, float toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    public static int Map(this int value, int fromLow, int fromHigh, int toLow, int toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    public static float ToRadians(this float val) => 0.0174532925199433F * val;
}