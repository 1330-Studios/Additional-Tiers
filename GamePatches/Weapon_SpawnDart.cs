using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.GamePatches;
[HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
internal static class WeaponSpawnDart {

    [HarmonyPostfix]
    public static void Postfix(AttackBehavior __instance) {
        if (__instance?.attack?.tower?.Node == null || __instance.attack.tower.Node.graphic == null) return;

        if (TowerLookup.Instance.Contains(__instance.attack.tower.towerModel.name))
            TransformationManager.Get(__instance.attack.tower).AddedTiers.Animation(__instance.attack, __instance.attack.tower);
    }
}