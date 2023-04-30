using AdditionalTiers.Towers;

namespace AdditionalTiers.ContentLoaders;
internal sealed class AddedTiersLoader : Loader<AddedTiers> {
    internal override void Load(MelonAssembly melonAssembly) {
        var typeCount = 0;
        foreach (var type in melonAssembly.Assembly.GetValidTypes()) {
            if (type.IsAbstract || type.ContainsGenericParameters || !type.IsAssignableTo(typeof(AddedTiers))) continue;
            Add((AddedTiers)Activator.CreateInstance(type));
            typeCount++;
        }

        RuntimeInfo.Logger.Msg($"Loaded {typeCount} Added Tier instances.");
    }
}