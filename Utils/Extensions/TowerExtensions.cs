namespace AdditionalTiers.Utils.Extensions;
internal static class TowerExtensions {
    public static string GetNameMod(this TowerModel towerModel, string name) {
        return name + (towerModel.tiers.Sum() > 0 ? $"-{towerModel.tiers[0]}{towerModel.tiers[1]}{towerModel.tiers[2]}" : "");
    }

    public static void SetIcons(this TowerModel towerModel, string id, bool builtin = false) {
        var wrapped = $"Ui[{id}]";
        towerModel.icon = new SpriteReference
        {
            guidRef = builtin ? id : wrapped
        };
        towerModel.portrait = new SpriteReference
        {
            guidRef = builtin ? id : wrapped
        };
    }

    public static void SetDisplay(this TowerModel towerModel, string id) {
        towerModel.display = new PrefabReference { guidRef = id };
        foreach (var model in towerModel.behaviors)
            if (model.Is<DisplayModel>(out var dm))
                dm.display = new PrefabReference { guidRef = id };
    }
}