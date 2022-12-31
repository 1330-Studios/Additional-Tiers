using AdditionalTiers.ContentLoaders;
using AdditionalTiers.Towers;

namespace AdditionalTiers.GamePatches;

[HarmonyPatch(typeof(GameModelLoader), nameof(GameModelLoader.Load))]
internal static class GameModelLoader_Load {
    internal static GameModel Model { get; set; } = null!;

    [HarmonyPostfix]
    internal static void Postfix(ref GameModel __result) {
        Model = __result;

        foreach (var addedTier in LoaderLoader.GetLoaderInstance<AddedTiers>().Get()) {
            var bTower = Model.towers.First(a => a.name.Equals(addedTier.BaseTower)).CloneCast();
            addedTier.GenerateTowerModels(bTower, Model);
        }
    }
}