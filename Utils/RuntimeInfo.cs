﻿using System.Diagnostics;

namespace AdditionalTiers.Utils;

internal static class RuntimeInfo {
    internal static string ModVersion;
    internal static string GameVersion;

    internal static bool IsDebug;

    internal static MelonLogger.Instance Logger;

    internal static void Initialize(MelonAssembly melonAssembly, MelonLogger.Instance instance) {
        var version = melonAssembly.Assembly.GetName().Version;
        if (version != null) ModVersion = version.ToString();
        GameVersion = UnityInformationHandler.GameVersion;

        IsDebug = melonAssembly.Assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Any(da => da.IsJITTrackingEnabled);

        Logger = instance;
    }
}