using System.Diagnostics;

namespace AdditionalTiers.Utils;

internal static class RuntimeInfo {
    internal static string ModVersion;
    internal static string GameVersion;

    internal static bool ISDEBUG;

    internal static MelonLogger.Instance Logger;

    internal static int LastWin32Error => Marshal.GetLastWin32Error();
    internal static int LastSystemError => Marshal.GetLastSystemError();
    internal static int LastPInvokeError => Marshal.GetLastPInvokeError();

    internal static void Initialize(MelonAssembly masm, MelonLogger.Instance instance) {
        ModVersion = masm.Assembly.GetName().Version.ToString();
        GameVersion = UnityInformationHandler.GameVersion;

        ISDEBUG = masm.Assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Any(da => da.IsJITTrackingEnabled);

        Logger = instance;
    }
}