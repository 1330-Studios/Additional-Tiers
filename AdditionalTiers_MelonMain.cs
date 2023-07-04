using AdditionalTiers.ContentLoaders;
using AdditionalTiers.Towers;

[assembly: MelonAuthorColor(255, 200, 200, 255)]
[assembly: MelonColor(255, 255, 75, 255)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(AdditionalTiers_MelonMain), "Additional Tier Add-on", "37.3", "1330 Studios LLC")]

namespace AdditionalTiers;
public sealed class AdditionalTiers_MelonMain : MelonMod {
    public override void OnInitializeMelon() {
        RuntimeInfo.Initialize(MelonAssembly, LoggerInstance);
        RuntimeInitializer.Initialize(MelonAssembly);

        LoaderLoader.Initialize(MelonAssembly);

        LoaderLoader.GetLoaderInstance<AddedTiers>().Load(MelonAssembly);

        PrintActiveTiers();
    }

    private static void PrintActiveTiers() {
        StringBuilder sb = new();

        sb.AppendLine("╔Towers Loaded:══════════════════════════════════════════════════════════════════╗");
        foreach (var tower in LoaderLoader.GetLoaderInstance<AddedTiers>().Get().OrderByDescending(a => a.BaseTower.Contains("Paragon")))
            sb.AppendLine($"║\t{tower.BaseTower.Split('-')[0] + "-" + tower.BaseTower.Split('-')[1].Replace('2', '0').Replace('1', '0').Replace('0', 'x'),-20} - {$"\"{tower.Name}\"",-20} ".PadRight(75) + "║");
        sb.AppendLine("╚════════════════════════════════════════════════════════════════════════════════╝");

        var o_OutputEncoding = Console.OutputEncoding;

        Console.OutputEncoding = Encoding.UTF8;

        RuntimeInfo.Logger.Msg(System.Drawing.Color.OrangeRed, $"Additional Tier Add-on Loaded ({(RuntimeInfo.IsDebug?"DEBUG":"")} v{RuntimeInfo.ModVersion}){Environment.NewLine}{sb}");

        Console.OutputEncoding = o_OutputEncoding;
    }

    public override void OnGUI() {
        var guiCol = GUI.color;
        GUI.color = new Color32(255, 255, 255, 50);
        var guiStyle = new GUIStyle { normal = { textColor = Color.white } };
        GUI.Label(new Rect(10, Screen.height - 20, 100, 90), $"Additional Tiers Add-on v{RuntimeInfo.ModVersion} {(RuntimeInfo.IsDebug ? "DEBUG" : "")}", guiStyle);
        GUI.color = guiCol;
    }
}