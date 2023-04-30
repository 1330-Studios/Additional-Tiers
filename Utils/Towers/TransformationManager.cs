using AdditionalTiers.Towers;

using Il2CppAssets.Scripts;

namespace AdditionalTiers.Utils.Towers;
internal static class TransformationManager {
    private static readonly List<Transformation> ActiveTransformations = new();

    internal static void Add(Transformation transformation) {
        if (transformation.AddedTiers == null)
            throw new ArgumentNullException(nameof(transformation));

        ActiveTransformations.Add(transformation);

        MelonDebug.Msg($"Added transformation to tower with ID {transformation.TowerID} named {transformation.AddedTiers.Name}");
    }

    internal static void Clear() {
        if (MelonDebug.IsEnabled())
            foreach (var obj in ActiveTransformations)
                MelonDebug.Msg($"Tower with ID {obj.TowerID} has been unregistered {obj.AddedTiers.Name}");

        ActiveTransformations.Clear();
    }

    internal static Transformation Get(Tower tower) {
        foreach (var transformation in ActiveTransformations.Where(transformation => transformation.TowerID == tower.Id))
        {
            return transformation;
        }

        ThrowHelper.ThrowNoMatchException();
        return default;
    }

    internal static bool Contains(Tower tower)
    {
        return ActiveTransformations.Any(transformation => transformation.TowerID == tower.Id);
    }

    internal struct Transformation {
        internal AddedTiers AddedTiers { get; set; }
        internal ObjectId TowerID { get; set; }

        internal Transformation(AddedTiers addedTiers, ObjectId towerId) {
            AddedTiers = addedTiers;
            TowerID = towerId;
        }
    }
}