using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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