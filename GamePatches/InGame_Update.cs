using AdditionalTiers.ContentLoaders;
using AdditionalTiers.Towers;
using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.GamePatches;

[HarmonyPatch(typeof(InGame), nameof(InGame.Update))]
internal static class InGame_Update {
    [HarmonyPostfix]
    internal static void Postfix(InGame __instance) {
        if (__instance == null || __instance.bridge == null || __instance.bridge.GetAllTowers() == null) {
            TransformationManager.Clear();
            return;
        }

        UpgradeMenuManager.Update(__instance);

        foreach (var towerToSimulation in __instance.bridge.GetAllTowers().ToArray().ToImmutableArray()) {
            var towerModel = towerToSimulation.tower.towerModel;
            if (TransformationManager.Contains(towerToSimulation.tower)) {
                var form = TransformationManager.Get(towerToSimulation.tower);
                form.AddedTiers.InGameUpdate(towerToSimulation);
            } else {
                foreach (var addedTier in LoaderLoader.GetLoaderInstance<AddedTiers>().Get()) {
                    if (towerModel.tiers[addedTier.Path] == 5 && addedTier.BaseTower.Split('-')[0].Trim().Equals(towerModel.baseId.Trim())) {
                        var (progress, shouldForm) = addedTier.GetStatus(towerToSimulation.tower);

                        if (shouldForm) {
                            addedTier.Upgrade(towerToSimulation);
                            TransformationManager.Add(new TransformationManager.Transformation(addedTier, towerToSimulation.tower.Id));
                            AbilityMenu.instance.TowerChanged(towerToSimulation);
                            AbilityMenu.instance.RebuildAbilities();
                        } else {
                            RoundDisplay_OnUpdate.Data.Add((addedTier.Name, progress));
                        }
                    }
                }
            }
        }
    }
}
