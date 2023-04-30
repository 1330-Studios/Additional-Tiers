using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers.Round8.Primary.DartMonkey;
internal class OmegaJuggernaut : AddedTiers {
    internal override string Name => "Omega Juggernaut";
    internal override string Description => "Now I have become death, the destroyer of worlds.";
    internal override string BaseTower => "DartMonkey-500";
    internal override int Path => 0;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var percentage = tower.damageDealt / 100_000.0;

        return (percentage, percentage > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        baseTower.name = $"{Name} T6";
        baseTower.SetDisplay("Round8_OJ#1");
        baseTower.SetIcons("Round8_OJ_Portrait");
        baseTower.dontDisplayUpgrades = true;

        float damageStat = 10;
        foreach (var behavior in baseTower.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = .75f;
            var originalProj = am.weapons[0].projectile.CloneCast();
            am.weapons[0].projectile.SetDisplay("Round8_OJ_Proj#1.5");

            foreach (var projBehavior in originalProj.behaviors) {
                if (projBehavior.Is<CreateProjectileOnExhaustFractionModel>(out var createProjectileOnExhaustFractionModel)) {
                    createProjectileOnExhaustFractionModel.emission.Cast<ArcEmissionModel>().count /= 3;
                }
            }

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                    dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                }

                if (!projBehavior.Is<CreateProjectileOnExhaustFractionModel>(
                        out var createProjectileOnExhaustFractionModel)) continue;
                createProjectileOnExhaustFractionModel.projectile = originalProj;
                createProjectileOnExhaustFractionModel.emission = new ArcEmissionModel("AEM__", 2, 0, 360, null, false);
            }
        }

        baseTower.behaviors = baseTower.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamouflageDetectionModel_", true));

        var T1 = baseTower.CloneCast();
        T1.name = $"{Name} T7";

        damageStat = 25;

        foreach (var behavior in T1.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.65f;
            am.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 45, null, false);

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        var T2 = T1.CloneCast();
        T2.name = $"{Name} T8";
        T2.SetDisplay("Round8_OJ_7#1");

        damageStat = 50;

        foreach (var behavior in T2.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.4f;
            am.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 45, null, false);
            var originalProj = am.weapons[0].projectile.CloneCast();
            am.weapons[0].projectile.SetDisplay("Round8_OJ_Proj7#1.5");

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }

                if (!projBehavior.Is<CreateProjectileOnExhaustFractionModel>(
                        out var createProjectileOnExhaustFractionModel)) continue;
                createProjectileOnExhaustFractionModel.projectile = originalProj;
                createProjectileOnExhaustFractionModel.emission = new ArcEmissionModel("AEM_", 2, 0, 360, null, false);
            }
        }

        var T3 = T2.CloneCast();
        T3.name = $"{Name} T9";

        damageStat = 75;

        foreach (var behavior in T3.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.33f;
            am.weapons[0].emission = new ArcEmissionModel("AEM_", 4, 0, 55, null, false);

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        var T4 = T3.CloneCast();
        T4.name = $"{Name} T10";

        damageStat = 500;

        foreach (var behavior in T4.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.25f;

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        var T5 = T4.CloneCast();
        T5.name = $"{Name} T11";
        T5.SetDisplay("Round8_OJ_11#1");
        T5.range += 15;

        damageStat = 1000;

        foreach (var behavior in T5.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.range = T5.range;
            am.weapons[0].Rate = 0.15f;
            var originalProj = am.weapons[0].projectile.CloneCast();
            am.weapons[0].projectile.SetDisplay("Round8_OJ_Proj11#1.5");

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }

                if (!projBehavior.Is<CreateProjectileOnExhaustFractionModel>(out var createProjectileOnExhaustFractionModel)) continue;
                createProjectileOnExhaustFractionModel.projectile = originalProj;
                createProjectileOnExhaustFractionModel.emission = new ArcEmissionModel("AEM_", 2, 90, 360, null, false);
            }
        }

        var T6 = T5.CloneCast();
        T6.name = $"{Name} T12";
        T6.range += 35;

        damageStat = 2000;

        foreach (var behavior in T6.behaviors) {
            if (!behavior.Is<AttackModel>(out var am)) continue;
            am.range = T6.range;
            am.weapons[0].Rate = 0.15f;

            foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                if (projBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        TowerRegister.Register(currentUpgrade: 0, towerModel: baseTower, towerType: "", upgradeCost: 30_000, portrait: "Round8_OJ_Portrait", currentSPA: 0.75, currentDamage: 10,
            nextSPA: -0.1, nextDamage: 15, nextRange: 0, extra: "Triple Shots", maxUpgrade: false, nextUpgradeName: $"{Name} T7");
        TowerRegister.Register(currentUpgrade: 1, towerModel: T1, towerType: "", upgradeCost: 55_000, portrait: "Round8_OJ_Portrait", currentSPA: 0.65, currentDamage: 25,
            nextSPA: -0.25, nextDamage: 25, nextRange: 0, extra: "Amethyst Boost", maxUpgrade: false, nextUpgradeName: $"{Name} T8");
        TowerRegister.Register(currentUpgrade: 2, towerModel: T2, towerType: "", upgradeCost: 68_500, portrait: "Round8_OJ_Portrait", currentSPA: 0.4, currentDamage: 50,
            nextSPA: -.07, nextDamage: 25, nextRange: 0, extra: "Quad Shots", maxUpgrade: false, nextUpgradeName: $"{Name} T9");
        TowerRegister.Register(currentUpgrade: 3, towerModel: T3, towerType: "", upgradeCost: 125_000, portrait: "Round8_OJ_Portrait", currentSPA: 0.33, currentDamage: 75,
            nextSPA: -.08, nextDamage: 440, nextRange: 0, extra: "Reinforced Steel", maxUpgrade: false, nextUpgradeName: $"{Name} T10");
        TowerRegister.Register(currentUpgrade: 4, towerModel: T4, towerType: "", upgradeCost: 150_000, portrait: "Round8_OJ_Portrait", currentSPA: 0.25, currentDamage: 500,
            nextSPA: -.1, nextDamage: 500, nextRange: 15, extra: "Gold Plating", maxUpgrade: false, nextUpgradeName: $"{Name} T11");
        TowerRegister.Register(currentUpgrade: 5, towerModel: T5, towerType: "", upgradeCost: 165_000, portrait: "Round8_OJ_Portrait", currentSPA: 0.15, currentDamage: 1000,
            nextSPA: 0, nextDamage: 1000, nextRange: 35, extra: "Super Range", maxUpgrade: false, nextUpgradeName: $"{Name} T12");
        TowerRegister.Register(currentUpgrade: 6, towerModel: T6, towerType: "", upgradeCost: 0, portrait: "Round8_OJ_Portrait", currentSPA: 0.15, currentDamage: 2000,
            nextSPA: 0, nextDamage: 0, nextRange: 0, extra: "", maxUpgrade: true, nextUpgradeName: "");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (!tower.towerModel.name.StartsWith(Name)) return;
        tower.Node.graphic.GetComponent<Animator>().StopPlayback();
        tower.Node.graphic.GetComponent<Animator>().Play("Attack");
    }
}