using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.GamePatches;
[HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
internal static class Weapon_SpawnDart {

    [HarmonyPostfix]
    private static void Postfix(Weapon __instance) {
        if (__instance == null || __instance.attack == null ||
            __instance.attack.tower == null || __instance.attack.tower.Node == null ||
            __instance.attack.tower.Node.graphic == null) return;

        if (TowerLookup.Instance.Contains(__instance.attack.tower.towerModel.name))
            TransformationManager.Get(__instance.attack.tower).AddedTiers.Animation(__instance.attack, __instance.attack.tower);
    }
}