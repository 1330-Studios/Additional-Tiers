namespace AdditionalTiers.Utils.Towers;

internal record struct UMM_Tower(int CurrentUpgrade, string Name, TowerModel TowerModel, string TowerType,
    int UpgradeCost, string Portrait, double CurrentSPA, int CurrentDamage, double NextSPA, int NextDamage,
    int NextRange, string Extra, bool MaxUpgrade, string NextUpgradeName);