namespace AdditionalTiers.Utils.Towers;
internal static class TowerRegister {
    // Just a little helper class.

    internal static void Register(int currentUpgrade, TowerModel towerModel, string towerType, int upgradeCost, string portrait, double currentSPA, int currentDamage, double nextSPA, int nextDamage, int nextRange, string extra, bool maxUpgrade, string nextUpgradeName) {
        UpgradeMenuManager.AddTower(currentUpgrade, towerModel, towerType, upgradeCost, portrait, currentSPA, currentDamage, nextSPA, nextDamage, nextRange, extra, maxUpgrade, nextUpgradeName);
        TowerLookup.Instance[towerModel.name] = towerModel;
    }
}