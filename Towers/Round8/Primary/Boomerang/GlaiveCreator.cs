using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers.Round8.Primary.Boomerang;
internal class GlaiveCreator : AddedTiers {
    internal override string Name => "Glaive Creator";
    internal override string Description => "Creator of all things Glaive. Bloons beware.";
    internal override string BaseTower => "BoomerangMonkey-502";
    internal override int Path => 0;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var perc = tower.damageDealt / 100_000.0;

        return (perc, perc > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        var tower = baseTower;

        tower.name = $"{Name}";
        tower.SetDisplay("Round8_GLord#10");
        tower.SetIcons("Round8_GC_Portrait");
        tower.range += 5;
        tower.dontDisplayUpgrades = true;

        float damageStat = 15;

        #region Legacy Code
        var flames = gameModel.towers.First(a => a.name.Equals("TackShooter-500")).CloneCast().behaviors.First(a => a.Is<AttackModel>() && !a.name.Contains("Meteor")).CloneCast<AttackModel>();
        flames.name = "AttackModel_AttackFlames_";
        flames.weapons[0].projectile.collisionPasses = new int[] { 0 };

        for (int i = 0; i < tower.behaviors.Length; i++) {
            if (tower.behaviors[i].Is<AttackModel>(out var am)) {
                am.range += 5;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat / 3;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_Attack_")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                            tsm.Lifespan += 8;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                            dmftm.damageMultiplier++;
                        }
                    }

                    am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(
                        new Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel("CEOEM_", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" },
                        1, false, true, new EffectModel("Effect", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)));
                }
            } else if (tower.behaviors[i].Is<OrbitModel>(out var om)) {
                om.range += 5;
                om.count++;
            }
        }

        tower.behaviors = tower.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true), flames);

        var t1 = tower.CloneCast();
        t1.range += 15;
        t1.name = $"{Name} T1";

        damageStat = 30;

        for (int i = 0; i < t1.behaviors.Length; i++) {
            if (t1.behaviors[i].Is<AttackModel>(out var am)) {
                am.range += 15;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat / 3;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                    am.weapons[0].Rate -= 0.05f;
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                            dmftm.damageMultiplier++;
                        }
                    }
                }
            } else if (t1.behaviors[i].Is<OrbitModel>(out var om)) {
                om.range += 5;
                om.count++;
            }
        }

        var fireball = gameModel.towers.First(a => a.name.Contains("WizardMonkey-020")).CloneCast().behaviors.First(a => a.name.Contains("Fireball")).CloneCast<AttackModel>();
        fireball.name = "AttackModel_AttackFireball_";
        fireball.weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>().projectile.CapPierce(999999);

        var t2 = t1.CloneCast();
        t2.range += 15;
        t2.name = $"{Name} T2";

        t2.behaviors = t2.behaviors.Add(fireball);

        damageStat = 60;

        for (int i = 0; i < t2.behaviors.Length; i++) {
            if (t2.behaviors[i].Is<AttackModel>(out var am)) {
                am.range += 15;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat / 3;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                    am.weapons[0].Rate -= 0.15f;
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                            tsm.Speed *= 1.25f;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                            dmftm.damageMultiplier++;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_AttackFireball_")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                            foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                    cdm.damage = damageStat;
                                    cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                }
                            }
                        }
                    }
                }
            } else if (t2.behaviors[i].Is<OrbitModel>(out var om)) {
                om.count++;
            }
        }

        var t3 = t2.CloneCast();
        t3.name = $"{Name} T3";

        damageStat = 120;

        for (int i = 0; i < t3.behaviors.Length; i++) {
            if (t3.behaviors[i].Is<AttackModel>(out var am)) {
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat / 3;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                    am.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 30, null, false);
                    am.weapons[0].Rate -= 0.2f;

                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                            tsm.Speed *= 1.5f;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                            dmftm.damageMultiplier++;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_AttackFireball_")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                            foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                    cdm.damage = damageStat;
                                    cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                }
                            }
                        }
                    }
                }
            } else if (t3.behaviors[i].Is<OrbitModel>(out var om)) {
                om.count++;
            }
        }

        var t4 = t3.CloneCast();
        t4.name = $"{Name} T4";

        damageStat = 3072;

        for (int i = 0; i < t4.behaviors.Length; i++) {
            if (t4.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate *= 0.05f;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                    am.weapons[0].emission = new ArcEmissionModel("AEM_", 10, 0, 100, null, false);

                    if (am.name.Contains("AttackModel_Attack_")) {
                        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DMFTM_", "Moabs", 5, 10, false, true));
                    }
                    if (am.name.Contains("AttackModel_AttackFlames_")) {
                        am.weapons[0].Rate *= 0.05f;
                    }

                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                            tsm.Speed *= 1.75f;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                            dmftm.damageMultiplier++;
                        }
                    }
                }

                if (am.name.Contains("AttackModel_AttackFireball_")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                            foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                    cdm.damage = damageStat;
                                    cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                }
                            }
                        }
                    }
                }
            } else if (t4.behaviors[i].Is<OrbitModel>(out var om)) {
                om.count += 2;
            }
        }

        #endregion

        TowerRegister.Register(0, tower, "Fire", 100_000, "Round8_GC_Portrait", 0.6, 15, -0.05, 15, 15, "", false, $"{Name} T1");
        TowerRegister.Register(1, t1, "Fire", 185_000, "Round8_GC_Portrait", 0.55, 30, -0.15, 30, 15, "Fireball", false, $"{Name} T2");
        TowerRegister.Register(2, t2, "Fire", 200_000, "Round8_GC_Portrait", 0.4, 60, -0.2, 60, 0, "Triple Rangs", false, $"{Name} T3");
        TowerRegister.Register(3, t3, "Fire", 350_000, "Round8_GC_Portrait", 0.2, 120, -0.19, 60, 0, "Triple Rangs", false, $"{Name} T4");
        TowerRegister.Register(4, t4, "Fire", 0, "Round8_GC_Portrait", 0.01, 3072, 0, 0, 0, "", true, $"");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (tower.towerModel.name.StartsWith(Name) && !attack.attackModel.name.Contains("Orbit")) {
            tower.Node.graphic.GetComponent<Animator>().StopPlayback();
            tower.Node.graphic.GetComponent<Animator>().Play("Attack");
        }
    }
}