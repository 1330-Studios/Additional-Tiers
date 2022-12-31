namespace AdditionalTiers.Utils.Towers;
internal class TowerLookup : Singleton<TowerLookup> {
    private readonly Dictionary<string, TowerModel> towers = new();

    public TowerModel this[string id] {
        get => towers[id];
        set => towers[id] = value;
    }

    public bool Contains(string id) {
        return towers.ContainsKey(id);
    }
}
