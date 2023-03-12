using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers;
internal abstract class AddedTiers {
    internal virtual string Name { get; }
    internal virtual string Description { get; }
    internal virtual string BaseTower { get; }
    internal virtual int Path { get; }

    internal virtual (double progress, bool shouldForm) GetStatus(Tower tower) { return default; }
    internal virtual void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) { }
    internal virtual void Upgrade(TowerToSimulation towerToSimulation) {
        towerToSimulation.tower.UpdateRootModel(TowerLookup.Instance[$"{Name} T6"]);
        towerToSimulation.tower.UpdatedModel(TowerLookup.Instance[$"{Name} T6"]);
    }

    internal virtual void InGameQuit() { }
    internal virtual void InGameUpdate(TowerToSimulation tts) { }

    internal virtual void Animation(Attack attack, Tower tower) { }
}