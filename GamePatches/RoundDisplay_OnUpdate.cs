namespace AdditionalTiers.GamePatches;
[HarmonyPatch(typeof(RoundDisplay), nameof(RoundDisplay.OnUpdate))]
internal static class RoundDisplay_OnUpdate {
    public static List<(string, double)> Data = new();
    public static string style = "{0:n0}: {1:P2}";

    [HarmonyPostfix]
    public static void Fix(ref RoundDisplay __instance) {
        __instance.text.text = $"{__instance.cachedRoundDisp}\n";
        Data.Sort((s1, s2) => s1.Item1.CompareTo(s2.Item1));
        for (int i = 0; i < Data.Count; i++) {
            if (i < 5)
                __instance.text.text += string.Format(style, Data[i].Item1, Data[i].Item2) + "\n";
            else if (!Input.GetKey(KeyCode.F1)) {
                __instance.text.text += "Press and hold F1 for more...";
                break;
            } else
                __instance.text.text += string.Format(style, Data[i].Item1, Data[i].Item2) + "\n";
        }
        Data.Clear();
    }
}