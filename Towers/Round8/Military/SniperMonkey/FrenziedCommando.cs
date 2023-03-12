using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdditionalTiers.Utils.Extensions;
using AdditionalTiers.Utils.Towers;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace AdditionalTiers.Towers.Round8.Military.SniperMonkey;
internal class FrenziedCommando : AddedTiers {
    internal override string Name => "Frenzied Commando";
    internal override string Description => "Sniper gone mad with power. He will take matters into his own hands.";
    internal override string BaseTower => "SniperMonkey-005";
    internal override int Path => 2;

    internal override (double progress, bool shouldForm) GetStatus(Tower tower) {
        var perc = tower.damageDealt / 100_000.0;

        return (perc, perc > 1);
    }

    internal override void GenerateTowerModels(TowerModel baseTower, GameModel gameModel) {
        var tower = baseTower;

        tower.name = $"{Name} T6";
        tower.SetDisplay("Round8_FrenziedCommando#1");
        tower.SetIcons("Round8_FC_Portrait");
        tower.dontDisplayUpgrades = true;

        float damageStat = 10;

        foreach (var behavior in tower.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.05f;
                am.weapons[0].ejectY = 35.4f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
            }
        }

        tower.behaviors = tower.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        var T1 = tower.CloneCast();
        T1.name = $"{Name} T7";

        damageStat = 25;

        AbilityModel abilityModel = gameModel.GetTower("DartlingGunner", 0, 4, 0).behaviors.First(a => a.Is<AbilityModel>()).CloneCast<AbilityModel>();
        abilityModel.name = abilityModel.displayName = abilityModel._name = "FC_Adrenaline";
        abilityModel.icon = new() { guidRef = "Ui[Round8_FC_AA]" };
        abilityModel.behaviors = abilityModel.behaviors.Remove(a => a.Is<ActivateAttackModel>()).Add(
            new ActivateRateSupportZoneModel("ActivateRateSupportZoneModel_", "Rate:Support", true, .01f, 1, 1, true, 20, new("DM_", new() { guidRef = "" }, 0), "", "", Array.Empty<TowerFilterModel>(), false),
            new ActivateDamageModifierSupportZoneModel("ActivateDamageModifierSupportZoneModel_", "Damage:Support", true, 1, 1, true, 20,
                new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 100, 1, false, true), Array.Empty<TowerFilterModel>())
            );
        foreach (var abeh in abilityModel.behaviors)
            if (abeh.Is<CreateSoundOnAbilityModel>(out var csoam))
                csoam.sound = null;
        abilityModel.cooldown = 50;
        abilityModel.livesCost = 10;

        foreach (var behavior in T1.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.04f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                }
            }
        }

        T1.behaviors = T1.behaviors.Add(abilityModel);

        var T2 = T1.CloneCast();
        T2.name = $"{Name} T8";

        damageStat = 100;

        foreach (var behavior in T2.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.03f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                }
            }
        }

        var bs = gameModel.GetTower("BombShooter").CloneCast();
        var cpocm = bs.behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>();
        cpocm.projectile.pierce = 1000;
        cpocm.projectile.radius = 10;
        cpocm.projectile.SetDisplay("Round8_S_Explosion#1");
        foreach (var cbeh in cpocm.projectile.behaviors) {
            if (cbeh.Is<DamageModel>(out var cdm)) {
                cdm.damage = 50;
            }
        }

        var T3 = T2.CloneCast();
        T3.name = $"{Name} T9";

        damageStat = 100;

        foreach (var behavior in T3.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.02f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                }

                am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(cpocm);
            }
        }

        var T4 = T3.CloneCast();
        T4.name = $"{Name} T10";

        damageStat = 300;

        foreach (var behavior in T4.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.01f;

                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var create)) {
                        foreach (var cbeh in create.projectile.behaviors) {
                            if (cbeh.Is<DamageModel>(out var cdm)) {
                                cdm.damage = damageStat;
                            }
                        }
                    }
                }
            }
        }

        var T5 = T4.CloneCast();
        T5.name = $"{Name} T11";

        foreach (var behavior in T5.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons = am.weapons.Add(am.weapons[0]);
            }
        }

        var T6 = T5.CloneCast();
        T6.name = $"{Name} T12";

        damageStat = 500;

        foreach (var behavior in T6.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var create)) {
                        foreach (var cbeh in create.projectile.behaviors) {
                            if (cbeh.Is<DamageModel>(out var cdm)) {
                                cdm.damage = damageStat;
                            }
                        }
                    }
                }
            }
        }

        var T7 = T6.CloneCast();
        T7.name = $"{Name} T13";

        damageStat = 1000;

        foreach (var behavior in T7.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var create)) {
                        foreach (var cbeh in create.projectile.behaviors) {
                            if (cbeh.Is<DamageModel>(out var cdm)) {
                                cdm.damage = damageStat;
                            }
                        }
                    }
                }
            }
        }
        

        AbilityModel abilityModel2 = gameModel.GetTower("IceMonkey", 0, 5, 0).behaviors.First(a => a.Is<AbilityModel>()).CloneCast<AbilityModel>();
        abilityModel2.name = abilityModel2.displayName = abilityModel2._name = "FC_TimeStop";
        abilityModel2.icon = new() { guidRef = "Ui[Round8_FC_AA2]" };

        foreach (var abeh in abilityModel2.behaviors) {
            if (abeh.Is<ActivateAttackModel>(out var aam)) {
                aam.Lifespan = 2.5f;
                foreach (var apb in aam.attacks[0].weapons[0].projectile.behaviors) {
                    if (apb.Is<FreezeModel>(out var FM)) {
                        FM.overlayType = "";
                    }
                }
            }

            if (abeh.Is<CreateSoundOnAbilityModel>(out var csoam))
                csoam.sound = null;
        }
        abilityModel2.behaviors = abilityModel2.behaviors.Remove(a=>a.Is<CreateEffectOnAbilityModel>())
            .Add(new ActivateRateSupportZoneModel("ActivateRateSupportZoneModel_", "Rate:Support", true, .001f, 1, 1, true, 35, new("DM_", new() { guidRef = "" }, 0), "", "", Array.Empty<TowerFilterModel>(), false));
        abilityModel2.activateOnPreLeak = true;

        var T8 = T7.CloneCast();
        T8.name = $"{Name} T14";

        damageStat = 10000;

        foreach (var behavior in T8.behaviors) {
            if (behavior.Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.005f;
                foreach (var projBehavior in am.weapons[0].projectile.behaviors) {
                    if (projBehavior.Is<DamageModel>(out var dm)) {
                        dm.damage = damageStat;
                    }
                    if (projBehavior.Is<CreateProjectileOnContactModel>(out var create)) {
                        foreach (var cbeh in create.projectile.behaviors) {
                            if (cbeh.Is<DamageModel>(out var cdm)) {
                                cdm.damage = damageStat;
                            }
                        }
                    }
                }
            }
        }

        T8.behaviors = T8.behaviors.Add(abilityModel2);

        TowerRegister.Register(currentUpgrade: 0, towerModel: tower, towerType: "Bullets", upgradeCost: 30_000, portrait: "Round8_FC_Portrait", currentSPA: 0.05, currentDamage: 10,
            nextSPA: -0.01, nextDamage: 15, nextRange: 0, extra: "Adrenaline", maxUpgrade: false, nextUpgradeName: $"{Name} T7");

        TowerRegister.Register(currentUpgrade: 1, towerModel: T1, towerType: "Bullets", upgradeCost: 45_000, portrait: "Round8_FC_Portrait", currentSPA: 0.04, currentDamage: 25,
            nextSPA: -0.01, nextDamage: 75, nextRange: 0, extra: "", maxUpgrade: false, nextUpgradeName: $"{Name} T8");

        TowerRegister.Register(currentUpgrade: 2, towerModel: T2, towerType: "Bullets", upgradeCost: 60_000, portrait: "Round8_FC_Portrait", currentSPA: 0.03, currentDamage: 100,
            nextSPA: -0.01, nextDamage: 50, nextRange: 0, extra: "Explosive Ammunition", maxUpgrade: false, nextUpgradeName: $"{Name} T9");

        TowerRegister.Register(currentUpgrade: 3, towerModel: T3, towerType: "Bullets", upgradeCost: 90_000, portrait: "Round8_FC_Portrait", currentSPA: 0.02, currentDamage: 150,
            nextSPA: -0.01, nextDamage: 150, nextRange: 0, extra: "", maxUpgrade: false, nextUpgradeName: $"{Name} T10");

        TowerRegister.Register(currentUpgrade: 4, towerModel: T4, towerType: "Bullets", upgradeCost: 115_000, portrait: "Round8_FC_Portrait", currentSPA: 0.01, currentDamage: 300,
            nextSPA: 0, nextDamage: 0, nextRange: 0, extra: "Double Tap", maxUpgrade: false, nextUpgradeName: $"{Name} T11");

        TowerRegister.Register(currentUpgrade: 5, towerModel: T5, towerType: "Bullets", upgradeCost: 144_000, portrait: "Round8_FC_Portrait", currentSPA: 0.01, currentDamage: 300,
            nextSPA: 0, nextDamage: 200, nextRange: 0, extra: "Lead Bullets", maxUpgrade: false, nextUpgradeName: $"{Name} T12");

        TowerRegister.Register(currentUpgrade: 6, towerModel: T6, towerType: "Bullets", upgradeCost: 150_000, portrait: "Round8_FC_Portrait", currentSPA: 0.01, currentDamage: 500,
            nextSPA: 0, nextDamage: 500, nextRange: 0, extra: "", maxUpgrade: false, nextUpgradeName: $"{Name} T13");

        TowerRegister.Register(currentUpgrade: 7, towerModel: T7, towerType: "Bullets", upgradeCost: 180_000, portrait: "Round8_FC_Portrait", currentSPA: 0.01, currentDamage: 1000,
            nextSPA: -0.005, nextDamage: 9000, nextRange: 0, extra: "Time Stop", maxUpgrade: false, nextUpgradeName: $"{Name} T14");

        TowerRegister.Register(currentUpgrade: 8, towerModel: T8, towerType: "Bullets", upgradeCost: 0, portrait: "Round8_FC_Portrait", currentSPA: 0.005, currentDamage: 10000,
            nextSPA: 0, nextDamage: 0, nextRange: 0, extra: "", maxUpgrade: true, nextUpgradeName: "");
    }

    internal override void Animation(Attack attack, Tower tower) {
        if (tower.towerModel.name.StartsWith(Name)) {
            tower.Node.graphic.GetComponent<Animator>().StopPlayback();
            tower.Node.graphic.GetComponent<Animator>().Play("Attack");
        }
    }
}
