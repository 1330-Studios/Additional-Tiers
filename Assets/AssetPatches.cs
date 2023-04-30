using AdditionalTiers.Utils.Components;

using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;

namespace AdditionalTiers.Assets;
internal sealed class AssetPatches {
    public static AssetBundle Round8 = AssetBundle.LoadFromMemory("AdditionalTiers.round8".GetEmbeddedResource());

    [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
    public sealed class DisplayFactory {
        [HarmonyPrefix]
        private static bool Prefix(Factory.__c__DisplayClass21_0 __instance) {
            var factory = __instance.__4__this;
            var prefabReference = __instance.objectId;
            var guid = prefabReference.guidRef;
            var onComplete = __instance.onComplete;

            var resourceManager = Addressables.Instance.ResourceManager;

            if (guid.StartsWith("Round8_")) {
                var round8Asset = Round8.LoadAsset(guid.Replace("Round8_", "").Split('#')[0]).Cast<GameObject>();
                round8Asset.name = guid.Replace("Round8_", "").Split('#')[0];

                var round8AssetInstance = Object.Instantiate(round8Asset, factory.DisplayRoot).AddComponent<UnityDisplayNode>();
                round8AssetInstance.Active = false;
                round8AssetInstance.name = guid.Replace("Round8_", "").Split('#')[0] + " (Clone)";
                var scale = round8AssetInstance.gameObject.AddComponent<Round8Scale>();
                scale.Scale = float.Parse(guid.Replace("Round8_", "").Split('#')[1]);
                round8AssetInstance.RecalculateGenericRenderers();

                factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(round8AssetInstance.gameObject, $"COULDN'T COMPLETE OPERATION FOR ROUND 8 ASSET {guid.Normalize()}");

                Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                var identity = Quaternion.identity;
                var gameObject2 = Object.Instantiate(round8AssetInstance.gameObject, vector, identity, factory.DisplayRoot);
                gameObject2.SetActive(true);
                var component = gameObject2.GetComponent<UnityDisplayNode>();
                var componentScale = gameObject2.GetComponent<Round8Scale>();
                componentScale.Scale = float.Parse(guid.Replace("Round8_", "").Split('#')[1]);
                component.Create();
                component.cloneOf = prefabReference;
                factory.active.Add(component);
                onComplete.Invoke(component);

                return false;
            }

            if (!guid.Equals("UpgradedText")) return true;
            var transform = Game.instance.prototypeObjects.transform;
            factory.FindAndSetupPrototypeAsync(new PrefabReference { guidRef = "3dcdbc19136c60846ab944ada06695c0" }, new Action<UnityDisplayNode>(node => {
                var gameObject = Object.Instantiate(node.gameObject, transform);
                gameObject.name = guid + " (Clone)";

                var instanceResourceManager = Addressables.Instance.ResourceManager;
                factory.prototypeHandles[prefabReference] = instanceResourceManager.CreateCompletedOperation(gameObject, "");
                var udn = gameObject.GetComponent<UnityDisplayNode>();

                udn.RecalculateGenericRenderers();
                var NKTextMeshPro = udn.GetComponentInChildren<TextMeshPro>();
                NKTextMeshPro.m_fontColorGradient = new VertexGradient(Color.red, Color.red, new Color(255, 255, 0), Color.white);
                udn.RecalculateGenericRenderers();

                Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                var identity = Quaternion.identity;
                var gameObject2 = Object.Instantiate(udn.gameObject, vector, identity, factory.DisplayRoot);
                gameObject2.SetActive(true);
                var component = gameObject2.GetComponent<UnityDisplayNode>();
                component.Create();
                component.cloneOf = prefabReference;
                factory.active.Add(component);
                onComplete.Invoke(component);
            }));
            return false;

        }
    }

    [HarmonyPatch(typeof(SpriteAtlas), nameof(SpriteAtlas.GetSprite))]
    public static class SpriteAtlas_GetSprite {
        [HarmonyPrefix]
        private static bool Prefix(Object __instance, string name, ref Sprite __result) {
            if (__instance.name != "Ui") return true;
            if (name.StartsWith("Round8_")) {
                var assetName = name.Trim().Replace("Round8_", "");
                var r8T = Round8.LoadAsset(assetName).Cast<Texture2D>();
                __result = Sprite.Create(r8T, new Rect(0, 0, r8T.width, r8T.height), new Vector2(), 10.2f);
                __result.texture.requestedMipmapLevel = -1;
                return false;
            }

            var resource = name.Trim().GetEmbeddedResource();
            if (!(resource?.Length > 0)) return true;
            var texture = resource.ToTexture();
            __result = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(), 10.2f);
            __result.texture.requestedMipmapLevel = -1;
            return false;
        }
    }
}