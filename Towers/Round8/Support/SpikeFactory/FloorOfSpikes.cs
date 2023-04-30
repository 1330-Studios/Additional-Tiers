using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers.Round8.Support.SpikeFactory;
internal class FloorOfSpikes : AddedTiers {
    internal override string Name => "Floor Of Spikes";
    internal override string Description => "The BAD Eradicator is a powerful upgrade designed to devastate entire waves of bloons. It is equipped with a variety of high-explosive bombs that can obliterate bloon layers with ease.";
    internal override string BaseTower => "SpikeFactory-250";
    internal override int Path => 1;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var percentage = tower.damageDealt / 100_000.0;

        return (percentage, percentage > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        baseTower.name = $"{Name} T6";
        baseTower.SetDisplay("Round8_FOS#1");
        baseTower.SetIcons("Round8_FOS_Portrait");
        baseTower.dontDisplayUpgrades = true;

        float damageStat = 5;

        foreach (var towerBehavior in baseTower.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.3f;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        damageStat = 10;
        var T1 = baseTower.CloneCast();
        T1.name = $"{Name} T7";

        foreach (var towerBehavior in T1.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.2f;
            am.weapons[0].projectile.pierce *= 2;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        damageStat = 30;
        var T2 = T1.CloneCast();
        T2.name = $"{Name} T8";

        foreach (var towerBehavior in T2.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.1f;
            am.weapons[0].projectile.pierce *= 5;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
        }

        damageStat = 100;
        var T3 = T2.CloneCast();
        T3.name = $"{Name} T9";

        foreach (var towerBehavior in T3.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.05f;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
            }
            am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Fortified", 5, 100, false, true));
        }

        damageStat = 1000;
        var T4 = T3.CloneCast();
        T4.name = $"{Name} T10";

        foreach (var towerBehavior in T4.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.01f;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
                if (projectileBehavior.Is<AgeModel>(out var agm)) {
                    agm.Lifespan /= 1.75f;
                }
            }
            am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Bad", 10, 500, false, true));
        }

        damageStat = 2000;
        var T5 = T4.CloneCast();
        T5.name = $"{Name} T11";

        foreach (var towerBehavior in T5.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            am.weapons[0].Rate = 0.01f;
            foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                if (projectileBehavior.Is<DamageModel>(out var dm)) {
                    dm.damage = damageStat;
                }
                if (projectileBehavior.Is<AgeModel>(out var agm)) {
                    agm.Lifespan /= 1.75f;
                }
            }

            am.weapons = am.weapons.Add(am.weapons[0].CloneCast(), am.weapons[0].CloneCast(), am.weapons[0].CloneCast());
        }

        damageStat = 10_000;
        var T6 = T5.CloneCast();
        T6.name = $"{Name} T12";

        foreach (var towerBehavior in T6.behaviors) {
            if (!towerBehavior.Is<AttackModel>(out var am)) continue;
            foreach (var weapon in am.weapons) {
                foreach (var projectileBehavior in weapon.projectile.behaviors) {
                    if (projectileBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                    if (projectileBehavior.Is<AgeModel>(out var agm)) {
                        agm.Lifespan /= 2f;
                    }
                }
            }
        }


        TowerRegister.Register(0, baseTower, "Spikes", 55_000, "Round8_FOS_Portrait", 0.3, 5, -0.1, 5, 0, "Extra Pierce", false, $"{Name} T7");
        TowerRegister.Register(1, T1, "Spikes", 75_000, "Round8_FOS_Portrait", 0.2, 10, -0.1, 20, 0, "Even More Pierce", false, $"{Name} T8");
        TowerRegister.Register(2, T2, "Spikes", 95_000, "Round8_FOS_Portrait", 0.1, 30, -0.05, 70, 0, "Fortified Shredding", false, $"{Name} T9");
        TowerRegister.Register(3, T3, "Spikes", 120_000, "Round8_FOS_Portrait", 0.05, 100, -0.04, 900, 0, "BAD Acupuncture", false, $"{Name} T10");
        TowerRegister.Register(4, T4, "Spikes", 140_000, "Round8_FOS_Portrait", 0.01, 1000, 0, 1000, 0, "Quad Spikes", false, $"{Name} T11");
        TowerRegister.Register(5, T5, "Spikes", 180_000, "Round8_FOS_Portrait", 0.01, 2000, 0, 8000, 0, "Supreme Damage", false, $"{Name} T12");
        TowerRegister.Register(6, T6, "Spikes", 0, "Round8_FOS_Portrait", 0.005, 10000, 0, 0, 0, "", true, "");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (!tower.towerModel.name.StartsWith(Name)) return;
        tower.Node.graphic.GetComponent<Animator>().StopPlayback();
        tower.Node.graphic.GetComponent<Animator>().Play("Attack");
    }
}