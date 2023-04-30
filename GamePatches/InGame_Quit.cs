using AdditionalTiers.ContentLoaders;
using AdditionalTiers.Towers;
using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.GamePatches;

[HarmonyPatch(typeof(InGame), nameof(InGame.Quit))]
[HarmonyPatch(typeof(InGame), nameof(InGame.Restart))]
internal class InGame_Quit {
    [HarmonyPostfix]
    internal static void Postfix() {
        foreach (var addedTier in LoaderLoader.GetLoaderInstance<AddedTiers>().Get())
            addedTier.InGameQuit();
        TransformationManager.Clear();
    }
}