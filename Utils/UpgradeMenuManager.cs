using AdditionalTiers.Assets;
using AdditionalTiers.Utils.Towers;

using Il2CppAssets.Scripts;

namespace AdditionalTiers.Utils;
internal static class UpgradeMenuManager {
    private static readonly AssetBundle Assets = AssetBundle.LoadFromMemory("AdditionalTiers.upgrademenu.bundle".GetEmbeddedResource());
    public static GameObject uiAsset = null!;
    public static GameObject canvasObject = null!;

    private static readonly Dictionary<string, UMM_Tower> towers = new();

    public static Tuple<string, ObjectId> lastTower;

    private static long lastPops = -1;

    public static void AddTower(int currentUpgrade, TowerModel towerModel, string towerType, int upgradeCost, string portrait, double currentSPA, int currentDamage, double nextSPA, int nextDamage, int nextRange, string extra, bool maxUpgrade, string nextUpgradeName) {
        towers[towerModel.name] = new UMM_Tower(currentUpgrade, towerModel.name, towerModel, towerType, upgradeCost, portrait, currentSPA, currentDamage, nextSPA, nextDamage, nextRange, extra, maxUpgrade, nextUpgradeName);
        MelonDebug.Msg($"AddTower for {towerModel.name} registered!");
    }

    public unsafe static void Update(InGame __instance) {
        if (__instance.bridge == null)
            return;

        if (canvasObject == null) {
            if (uiAsset == null)
                uiAsset = Assets.LoadAsset("Menu").Cast<GameObject>();

            uiAsset.name = "Menu (AT)";

            canvasObject = Object.Instantiate(uiAsset);

            canvasObject.SetActive(false);
        }

        Il2CppSystem.Collections.Generic.List<TowerToSimulation> atts;

        try {
            atts = __instance.bridge.GetAllTowers();
        } catch {
            return;
        }

        lock (atts) {
            if (atts == null || atts.Count <= 0)
                return;


            if (lastTower == null || lastTower == default || string.IsNullOrEmpty(lastTower.Item1) || lastTower.Item2 == default)
                return;

            foreach (var t in atts) {
                if (t == null || atts == null)
                    return;
                if (t?.Id == lastTower.Item2 && t?.damageDealt != lastPops) {
                    lastPops = t.damageDealt;
                    canvasObject.transform.FindChild("DAMAGEDEALT").GetComponent<Text>().text = $"Pops: {lastPops:N0}";
                }
            }
        }
    }

    [HarmonyPatch(typeof(TowerSelectionMenu), nameof(TowerSelectionMenu.SelectTower))]
    public sealed class TSM_Show {
        [HarmonyPrefix]
        public static bool Prefix(ref TowerSelectionMenu __instance, ref TowerToSimulation tower) {
            if (canvasObject.active)
                return false;

            if (towers.ContainsKey(tower.Def.name)) {
                __instance.Close();
                canvasObject.SetActive(true);
                lastTower = new Tuple<string, ObjectId>(tower.Def.name, tower.Id);
                UpdateUM(tower.Def.name, tower.Id);
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(InputManager), nameof(InputManager.CursorUp))]
    public sealed class IM_CursorUp {
        [HarmonyPrefix]
        public static bool Prefix() {
            bool keepUp = false;

            PointerEventData eventData = new PointerEventData(EventSystem.current) {
                position = Input.mousePosition
            };
            Il2CppSystem.Collections.Generic.List<RaycastResult> raysastResults = new();
            EventSystem.current.RaycastAll(eventData, raysastResults);

            for (int index = 0; index < raysastResults.Count; index++) {
                RaycastResult curRaysastResult = raysastResults[index];
                if (curRaysastResult.gameObject?.name == "BG") {
                    keepUp = true;
                    break;
                }
            }

            if (!keepUp) {
                canvasObject.SetActive(false);
            } else {
                try {
                    if (lastTower is not null)
                        UpdateUM(lastTower.Item1, lastTower.Item2);
                } catch { }
            }

            return true;
        }
    }

    public static void UpdateUM(string towerName, ObjectId id) {
        var info = towers.ContainsKey(towerName) ? towers[towerName] : default;

        var tower = InGame.Bridge.GetTower(id);
        canvasObject.transform.FindChild("NAME").GetComponent<Text>().fontSize = 13;
        canvasObject.transform.FindChild("NAME").GetComponent<Text>().text = info.Name;
        canvasObject.transform.FindChild("TOWERTYPE").GetComponent<Text>().text = info.TowerType;

        var upgradeButton = canvasObject.transform.FindChild("UPGRADEBUTTON").GetComponent<Button>();
        upgradeButton.onClick.RemoveAllListeners();

        if (!info.MaxUpgrade) {
            upgradeButton.onClick.AddListener(new Action(() => {
                if (InGame.Bridge.GetCash(InGame.Bridge.MyPlayerNumber) >= info.UpgradeCost) {
                    InGame.Bridge.SetCash(System.Math.Max(InGame.Bridge.GetCash(InGame.Bridge.MyPlayerNumber) - info.UpgradeCost, 0));
                    var tower = towers[info.NextUpgradeName].TowerModel;
                    lastTower = new Tuple<string, ObjectId>(tower.name, id);
                    InGame.Bridge.GetTower(id).tower.UpdateRootModel(tower);
                    InGame.Bridge.GetTower(id).tower.UpdatedModel(tower);
                    InGame.Bridge.GetTower(id).tower.worth += info.UpgradeCost;

                    AbilityMenu.instance.TowerChanged(InGame.Bridge.GetTower(id));
                    AbilityMenu.instance.RebuildAbilities();

                    UpdateUM(tower.name, id);
                }
            }));

            upgradeButton.transform.FindChild("UPGRADECOST").GetComponent<Text>().text = $"{info.UpgradeCost:C0}";
        } else {
            upgradeButton.transform.FindChild("UPGRADECOST").GetComponent<Text>().text = "MAXED";
        }

        var sellButton = canvasObject.transform.FindChild("SELLBUTTON").GetComponent<Button>();
        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(new Action(() => {
            InGame.Bridge.SellTower(id);
            canvasObject.SetActive(false);
        }));

        sellButton.transform.FindChild("SELLCOST").GetComponent<Text>().text = $"{(int)tower.sellFor:C0}";

        var targetingButton = canvasObject.transform.FindChild("TARGETING").GetComponent<Button>();
        targetingButton.onClick.RemoveAllListeners();
        targetingButton.onClick.AddListener(new Action(() => {
            InGame.Bridge.GetTower(id).tower.SetNextTargetType();
            UpdateUM(towerName, id);
        }));

        targetingButton.transform.FindChild("TARGETTYPE").GetComponent<Text>().text = tower.tower.TargetType.id;

        if (info.Portrait.StartsWith("Round8_")) {
            var assetName = info.Portrait.Trim().Replace("Round8_", "");
            var r8T = AssetPatches.Round8.LoadAsset(assetName).Cast<Texture2D>();
            r8T.mipMapBias = -1;
            canvasObject.transform.FindChild("PORTRAIT").GetComponent<Image>().sprite = Sprite.Create(r8T, new Rect(0, 0, r8T.width, r8T.height), new Vector2(), 10.2f);
        } else {
            var texture = info.Portrait.GetEmbeddedResource().ToTexture();
            texture.mipMapBias = -1;
            canvasObject.transform.FindChild("PORTRAIT").GetComponent<Image>().sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
        }


        var curStats = canvasObject.transform.FindChild("STATS");

        curStats.transform.FindChild("SPA").GetComponent<Text>().text = info.CurrentSPA.ToString() + " Seconds";
        curStats.transform.FindChild("DAMAGE").GetComponent<Text>().text = info.CurrentDamage.ToString("N0");
        curStats.transform.FindChild("RANGE").GetComponent<Text>().text = tower.tower.towerModel.range.ToString("N0");


        var upgradeStats = canvasObject.transform.FindChild("NEXTUPGRADE");
        upgradeStats.transform.FindChild("SPA").GetComponent<Text>().text = $"{(info.NextSPA >= 0 ? "+" : "")}{info.NextSPA}";
        upgradeStats.transform.FindChild("DAMAGE").GetComponent<Text>().text = $"{(info.NextDamage >= 0 ? "+" : "")}{info.NextDamage:N1}";
        upgradeStats.transform.FindChild("RANGE").GetComponent<Text>().text = $"{(info.NextRange >= 0 ? "+" : "")}{info.NextRange:N1}";

        if (!string.IsNullOrEmpty(info.Extra))
            upgradeStats.transform.FindChild("EXTRA").GetComponent<Text>().text = $"[+{info.Extra}]";
        else
            upgradeStats.transform.FindChild("EXTRA").GetComponent<Text>().text = "";
    }
}
