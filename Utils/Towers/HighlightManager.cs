namespace AdditionalTiers.Utils.Towers;
internal static class HighlightManager {
    [HarmonyPatch(typeof(Tower), nameof(Tower.Hilight))]
    internal static class Tower_Highlight {
        [HarmonyPostfix]
        public static void Highlight(ref Tower __instance) {
            if (__instance?.Node?.graphic?.genericRenderers != null)
            {
                foreach (var t in __instance.Node.graphic.genericRenderers)
                {
                    t.material.SetFloat("_Highlighted", 1);
                }
            }

            if (__instance?.attackBehaviorsInDependants == null) return;
            for (var i = 0; i < __instance.attackBehaviorsInDependants.Count; i++) {
                try {
                    for (var i1 = 0; i1 < __instance.attackBehaviorsInDependants[i]?.entity?.displayBehaviorCache?.node?.graphic?.genericRenderers.Count; i1++) {
                        __instance.attackBehaviorsInDependants[i].entity.displayBehaviorCache.node.graphic.genericRenderers[i1].material.SetFloat("_Highlighted", 1);
                    }
                } catch (NullReferenceException e) {
                    RuntimeInfo.Logger.Warning(e.Message);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.UnHighlight))]
    internal static class Tower_UnHighlight {
        [HarmonyPostfix]
        public static void UnHighlight(ref Tower __instance) {
            if (__instance?.Node?.graphic?.genericRenderers != null)
            {
                foreach (var t in __instance.Node.graphic.genericRenderers)
                {
                    t.material.SetFloat("_Highlighted", 0);
                }
            }

            if (__instance?.attackBehaviorsInDependants == null) return;
            for (var i = 0; i < __instance.attackBehaviorsInDependants.Count; i++) {
                try {
                    for (var i1 = 0; i1 < __instance.attackBehaviorsInDependants[i]?.entity?.displayBehaviorCache?.node?.graphic?.genericRenderers.Count; i1++) {
                        __instance.attackBehaviorsInDependants[i].entity.displayBehaviorCache.node.graphic.genericRenderers[i1].material.SetFloat("_Highlighted", 0);
                    }
                } catch (NullReferenceException e) {
                    RuntimeInfo.Logger.Warning(e.Message);
                }
            }
        }
    }
}