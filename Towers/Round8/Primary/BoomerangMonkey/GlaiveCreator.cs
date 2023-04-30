using AdditionalTiers.Utils.Towers;

namespace AdditionalTiers.Towers.Round8.Primary.BoomerangMonkey;
internal class GlaiveCreator : AddedTiers {
    internal override string Name => "Glaive Creator";
    internal override string Description => "Creator of all things Glaive. Bloons beware.";
    internal override string BaseTower => "BoomerangMonkey-502";
    internal override int Path => 0;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var percentage = tower.damageDealt / 100_000.0;

        return (percentage, percentage > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        baseTower.name = $"{Name} T6";
        baseTower.SetDisplay("Round8_GLord#10");
        baseTower.SetIcons("Round8_GC_Portrait");
        baseTower.range += 5;
        baseTower.dontDisplayUpgrades = true;

        float damageStat = 15;

        #region Legacy Code
        var flames = gameModel.towers.First(a => a.name.Equals("TackShooter-500")).CloneCast().behaviors.First(a => a.Is<AttackModel>() && !a.name.Contains("Meteor")).CloneCast<AttackModel>();
        flames.name = "AttackModel_AttackFlames_";
        flames.weapons[0].projectile.collisionPasses = new[] { 0 };

        foreach (var t in baseTower.behaviors) {
            if (t.Is<AttackModel>(out var am)) {
                am.range += 5;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (!projectileBehavior.Is<DamageModel>(out var dm)) continue;
                        dm.damage = damageStat / 3;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }

                if (!am.name.Contains("AttackModel_Attack_")) continue;
                {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                            tsm.Lifespan += 8;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var damageModifierForTagModel)) {
                            damageModifierForTagModel.damageMultiplier++;
                        }
                    }

                    am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(
                        new Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel("CreateEffectOnExpireModel_", new PrefabReference { guidRef = "6d84b13b7622d2744b8e8369565bc058" },
                            1, false, true, new EffectModel("Effect", new PrefabReference { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)));
                }
            } else if (t.Is<OrbitModel>(out var om)) {
                om.range += 5;
                om.count++;
            }
        }

        baseTower.behaviors = baseTower.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), flames);

        var t1 = baseTower.CloneCast();
        t1.range += 15;
        t1.name = $"{Name} T7";

        damageStat = 30;

        foreach (var t in t1.behaviors) {
            if (t.Is<AttackModel>(out var am)) {
                am.range += 15;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (!projectileBehavior.Is<DamageModel>(out var dm)) continue;
                        dm.damage = damageStat / 3;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }

                if (!am.name.Contains("AttackModel_Attack_") &&
                    !am.name.Contains("AttackModel_AttackFlames_")) continue;
                {
                    am.weapons[0].Rate -= 0.05f;
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var damageModifierForTagModel)) {
                            damageModifierForTagModel.damageMultiplier++;
                        }
                    }
                }
            } else if (t.Is<OrbitModel>(out var om)) {
                om.range += 5;
                om.count++;
            }
        }

        var fireball = gameModel.towers.First(a => a.name.Contains("WizardMonkey-020")).CloneCast().behaviors.First(a => a.name.Contains("Fireball")).CloneCast<AttackModel>();
        fireball.name = "AttackModel_AttackFireball_";
        fireball.weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>().projectile.CapPierce(999999);

        var t2 = t1.CloneCast();
        t2.range += 15;
        t2.name = $"{Name} T8";

        t2.behaviors = t2.behaviors.Add(fireball);

        damageStat = 60;

        foreach (var t in t2.behaviors) {
            if (t.Is<AttackModel>(out var am)) {
                am.range += 15;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (!projectileBehavior.Is<DamageModel>(out var dm)) continue;
                        dm.damage = damageStat / 3;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
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
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var damageModifierForTagModel)) {
                            damageModifierForTagModel.damageMultiplier++;
                        }
                    }
                }

                if (!am.name.Contains("AttackModel_AttackFireball_")) continue;
                {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var createProjectileOnContactModel)) {
                            foreach (var createdProjectileBehavior in createProjectileOnContactModel.projectile.behaviors) {
                                if (!createdProjectileBehavior.Is<DamageModel>(out var cdm)) continue;
                                cdm.damage = damageStat;
                                cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            } else if (t.Is<OrbitModel>(out var om)) {
                om.count++;
            }
        }

        var t3 = t2.CloneCast();
        t3.name = $"{Name} T9";

        damageStat = 120;

        foreach (var t in t3.behaviors) {
            if (t.Is<AttackModel>(out var am)) {
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (!projectileBehavior.Is<DamageModel>(out var dm)) continue;
                        dm.damage = damageStat / 3;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
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
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var damageModifierForTagModel)) {
                            damageModifierForTagModel.damageMultiplier++;
                        }
                    }
                }

                if (!am.name.Contains("AttackModel_AttackFireball_")) continue;
                {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var createProjectileOnContactModel)) {
                            foreach (var createdProjectileBehavior in createProjectileOnContactModel.projectile.behaviors) {
                                if (!createdProjectileBehavior.Is<DamageModel>(out var cdm)) continue;
                                cdm.damage = damageStat;
                                cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            } else if (t.Is<OrbitModel>(out var om)) {
                om.count++;
            }
        }

        var t4 = t3.CloneCast();
        t4.name = $"{Name} T10";

        damageStat = 3072;

        foreach (var t in t4.behaviors) {
            if (t.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate *= 0.05f;
                if (am.name.Contains("Orbit")) {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (!projectileBehavior.Is<DamageModel>(out var dm)) continue;
                        dm.damage = damageStat;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }

                if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                    am.weapons[0].emission = new ArcEmissionModel("AEM_", 10, 0, 100, null, false);

                    if (am.name.Contains("AttackModel_Attack_")) {
                        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 5, 10, false, true));
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
                        } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var damageModifierForTagModel)) {
                            damageModifierForTagModel.damageMultiplier++;
                        }
                    }
                }

                if (!am.name.Contains("AttackModel_AttackFireball_")) continue;
                {
                    foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                        if (projectileBehavior.Is<DamageModel>(out var dm)) {
                            dm.damage = damageStat;
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var createProjectileOnContactModel)) {
                            foreach (var createdProjectileBehavior in createProjectileOnContactModel.projectile.behaviors) {
                                if (!createdProjectileBehavior.Is<DamageModel>(out var cdm)) continue;
                                cdm.damage = damageStat;
                                cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            } else if (t.Is<OrbitModel>(out var om)) {
                om.count += 2;
            }
        }

        #endregion

        TowerRegister.Register(0, baseTower, "Fire", 100_000, "Round8_GC_Portrait", 0.6, 15, -0.05, 15, 15, "", false, $"{Name} T7");
        TowerRegister.Register(1, t1, "Fire", 185_000, "Round8_GC_Portrait", 0.55, 30, -0.15, 30, 15, "Fireball", false, $"{Name} T8");
        TowerRegister.Register(2, t2, "Fire", 200_000, "Round8_GC_Portrait", 0.4, 60, -0.2, 60, 0, "Triple Rangs", false, $"{Name} T9");
        TowerRegister.Register(3, t3, "Fire", 350_000, "Round8_GC_Portrait", 0.2, 120, -0.19, 60, 0, "Triple Rangs", false, $"{Name} T10");
        TowerRegister.Register(4, t4, "Fire", 0, "Round8_GC_Portrait", 0.01, 3072, 0, 0, 0, "", true, $"");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (!tower.towerModel.name.StartsWith(Name) || attack.attackModel.name.Contains("Orbit")) return;
        tower.Node.graphic.GetComponent<Animator>().StopPlayback();
        tower.Node.graphic.GetComponent<Animator>().Play("Attack");
    }
}