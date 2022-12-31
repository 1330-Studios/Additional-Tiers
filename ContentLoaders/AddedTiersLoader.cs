using AdditionalTiers.Towers;

namespace AdditionalTiers.ContentLoaders;
internal sealed class AddedTiersLoader : Loader<AddedTiers> {
    internal override void Load(MelonAssembly masm) {
        int typeCount = 0;
        foreach (var type in masm.Assembly.GetValidTypes()) {
            if (!type.IsAbstract && !type.ContainsGenericParameters && type.IsAssignableTo(typeof(AddedTiers))) {
                Add((AddedTiers)Activator.CreateInstance(type));
                typeCount++;
            }
        }

        RuntimeInfo.Logger.Msg($"Loaded {typeCount} Added Tier instances.");
    }
}