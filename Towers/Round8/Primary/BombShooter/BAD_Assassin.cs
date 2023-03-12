using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers.Round8.Primary.BombShooter;
internal class BAD_Assassin : AddedTiers {
    internal override string Name => "BAD Eradicator";
    internal override string Description => "The BAD Eradicator is a powerful upgrade designed to devastate entire waves of bloons. It is equipped with a variety of high-explosive bombs that can obliterate bloon layers with ease.";
    internal override string BaseTower => "BombShooter-050";
    internal override int Path => 1;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var perc = tower.damageDealt / 100_000.0;

        return (perc, perc > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        var tower = baseTower;

        tower.name = $"{Name} T6";
        tower.SetDisplay("Round8_BAD_Assassin#1");
        tower.SetIcons("Round8_BAD_Portrait");
        tower.range += 30;
        tower.dontDisplayUpgrades = true;

        float damageStat = 10;

        foreach (var behavior in tower.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.range += 30;

                am.weapons[0].Rate = 0.8f;
                am.weapons[0].ejectX = am.weapons[0].ejectY = am.weapons[0].ejectZ = 0;
                am.weapons[0].projectile.SetDisplay("Round8_BAD_Proj#0.9");

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 0.25f;
                        tsm.Speed += 0.005f;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                        cpocm.projectile.CapPierce(cpocm.projectile.pierce += 500);
                        cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat, cappedDamage = damageStat });
                    }
                    if (projBehavior.Is<CreateEffectOnContactModel>(out var ceocm)) {
                        ceocm.effectModel.assetId = new() { guidRef = "b1324f2f4c3809643b7ef1d8c112442a" };
                    }
                }
            }
            if (behavior.Is<AbilityModel>(out var abm)) {
                abm.icon = new() { guidRef = "Ui[Round8_BAD_AA]" };
                abm.Cooldown--;

                foreach (var aBehavior in abm.behaviors) {
                    if (aBehavior.Is<ActivateAttackModel>(out var aam)) {
                        aam.attacks[0].weapons[0].ejectX = aam.attacks[0].weapons[0].ejectY = aam.attacks[0].weapons[0].ejectZ = 0;
                        aam.attacks[0].weapons[0].projectile.SetDisplay("Round8_BAD_Proj#0.9");
                        aam.attacks[0].weapons[0].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat * 1000, cappedDamage = damageStat * 1000 });
                    }
                }
            }
        }

        tower.behaviors = tower.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        damageStat = 30;
        var T1 = tower.CloneCast();
        T1.name = $"{Name} T7";
        T1.range += 15;
        foreach (var behavior in T1.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.range = T1.range;

                am.weapons[0].Rate = 0.6f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 0.25f;
                        tsm.Speed += 0.005f;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                        cpocm.projectile.CapPierce(cpocm.projectile.pierce += 500);
                        cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat, cappedDamage = damageStat });
                        cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new DamageModifierForTagModel("BAD_Assassin", "Bad", 500, 2000, false, true));
                    }
                }
            }
            if (behavior.Is<AbilityModel>(out var abm)) {
                abm.Cooldown--;

                foreach (var aBehavior in abm.behaviors) {
                    if (aBehavior.Is<ActivateAttackModel>(out var aam)) {
                        aam.attacks[0].weapons[0].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat * 1000, cappedDamage = damageStat * 1000 });
                    }
                }
            }
        }

        damageStat = 100;
        var T2 = tower.CloneCast();
        T2.name = $"{Name} T8";
        T2.range += 15;
        foreach (var behavior in T2.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.range = T2.range;

                am.weapons[0].Rate = 0.6f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 0.25f;
                        tsm.Speed += 0.005f;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                        cpocm.projectile.CapPierce(cpocm.projectile.pierce += 500);
                        cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat, cappedDamage = damageStat });
                        cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new DamageModifierForTagModel("BAD_Assassin_2", "Bad", 5000, 2000, false, true));
                    }
                }
            }
            if (behavior.Is<AbilityModel>(out var abm)) {
                abm.Cooldown--;

                foreach (var aBehavior in abm.behaviors) {
                    if (aBehavior.Is<ActivateAttackModel>(out var aam)) {
                        aam.attacks[0].weapons[0].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat * 10000, cappedDamage = damageStat * 10000 });
                    }
                }
            }
        }

        damageStat = 500;
        var T3 = tower.CloneCast();
        T3.name = $"{Name} T9";
        T3.range += 15;
        foreach (var behavior in T3.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.range = T3.range;

                am.weapons[0].Rate = 0.15f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 0.25f;
                        tsm.Speed += 0.005f;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                        cpocm.projectile.CapPierce(cpocm.projectile.pierce += 500);
                        cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat, cappedDamage = damageStat });
                        cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new DamageModifierForTagModel("BAD_Assassin_3", "Bad", 15000, 2000, false, true));
                    }
                }
            }
            if (behavior.Is<AbilityModel>(out var abm)) {
                abm.Cooldown--;

                foreach (var aBehavior in abm.behaviors) {
                    if (aBehavior.Is<ActivateAttackModel>(out var aam)) {
                        aam.attacks[0].weapons[0].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat * 20000, cappedDamage = damageStat * 20000 });
                    }
                }
            }
        }

        damageStat = 30000;
        var T4 = tower.CloneCast();
        T4.name = $"{Name} T10";
        T4.range += 10;
        foreach (var behavior in T4.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.range = T4.range;

                am.weapons[0].Rate = 0.05f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 0.25f;
                        tsm.Speed += 0.005f;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                        cpocm.projectile.CapPierce(cpocm.projectile.pierce += 500);
                        cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat, cappedDamage = damageStat });
                        cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new DamageModifierForTagModel("BAD_Assassin_3", "Bad", 50000, 2000, false, true));
                    }
                }
            }
            if (behavior.Is<AbilityModel>(out var abm)) {
                abm.Cooldown--;

                foreach (var aBehavior in abm.behaviors) {
                    if (aBehavior.Is<ActivateAttackModel>(out var aam)) {
                        aam.attacks[0].weapons[0].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = damageStat * 10000, cappedDamage = damageStat * 10000 });
                    }
                }
            }
        }


        TowerRegister.Register(0, tower, "Bomb", 45_000, "Round8_BAD_Portrait", 0.8, 10, -0.2, 20, 15, "", false, $"{Name} T7");
        TowerRegister.Register(1, T1, "Bomb", 80_000, "Round8_BAD_Portrait", 0.6, 30, -0.2, 70, 15, "", false, $"{Name} T8");
        TowerRegister.Register(2, T2, "Bomb", 150_000, "Round8_BAD_Portrait", 0.4, 100, -0.35, 400, 15, "", false, $"{Name} T9");
        TowerRegister.Register(3, T3, "Bomb", 200_000, "Round8_BAD_Portrait", 0.15, 500, -0.1, 29500, 10, "", false, $"{Name} T10");
        TowerRegister.Register(4, T4, "Bomb", 0, "Round8_BAD_Portrait", 0.05, 30000, 0, 0, 0, "", true, "");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (tower.towerModel.name.StartsWith(Name)) {
            tower.Node.graphic.GetComponent<Animator>().StopPlayback();
            tower.Node.graphic.GetComponent<Animator>().Play("Attack");
        }
    }
}
