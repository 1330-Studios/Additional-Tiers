﻿#pragma warning disable IDE0005
global using System;
global using System.Buffers;
global using System.Collections;
global using System.Collections.Concurrent;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
global using System.Globalization;
global using System.IO;
global using System.IO.Compression;
global using System.IO.Enumeration;
global using System.Linq;
global using System.Reflection;
global using System.Reflection.Emit;
global using System.Resources;
global using System.Runtime;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Text.Unicode;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Tasks.Sources;
global using System.Timers;

global using AdditionalTiers;
global using AdditionalTiers.Utils;
global using AdditionalTiers.Utils.Extensions;

global using HarmonyLib;
global using HarmonyLib.Tools;

global using Il2Cpp;

global using Il2CppAOT;

global using AdditionalTiers.Utils.Attack;

global using Il2CppAssets;
global using Il2CppAssets.Scripts.Models;
global using Il2CppAssets.Scripts.Models.Audio;
global using Il2CppAssets.Scripts.Models.Behaviors;
global using Il2CppAssets.Scripts.Models.ContestedTerritory;
global using Il2CppAssets.Scripts.Models.Difficulty;
global using Il2CppAssets.Scripts.Models.Effects;
global using Il2CppAssets.Scripts.Models.GenericBehaviors;
global using Il2CppAssets.Scripts.Models.GeraldoItems;
global using Il2CppAssets.Scripts.Models.Knowledge;
global using Il2CppAssets.Scripts.Models.Map;
global using Il2CppAssets.Scripts.Models.Map.Actions;
global using Il2CppAssets.Scripts.Models.Map.Gizmos;
global using Il2CppAssets.Scripts.Models.Map.Spawners;
global using Il2CppAssets.Scripts.Models.Map.Triggers;
global using Il2CppAssets.Scripts.Models.Physics;
global using Il2CppAssets.Scripts.Models.Power;
global using Il2CppAssets.Scripts.Models.Powers;
global using Il2CppAssets.Scripts.Models.Powers.Effects;
global using Il2CppAssets.Scripts.Models.Powers.Mods;
global using Il2CppAssets.Scripts.Models.PowerSets;
global using Il2CppAssets.Scripts.Models.Profile;
global using Il2CppAssets.Scripts.Models.Rounds;
global using Il2CppAssets.Scripts.Models.ServerEvents;
global using Il2CppAssets.Scripts.Models.SimulationBehaviors;
global using Il2CppAssets.Scripts.Models.SimulationBehaviors.Mods;
global using Il2CppAssets.Scripts.Models.Skins;
global using Il2CppAssets.Scripts.Models.Store;
global using Il2CppAssets.Scripts.Models.Store.Loot;
global using Il2CppAssets.Scripts.Models.Towers;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.PlacementBehaviors;
global using Il2CppAssets.Scripts.Models.Towers.Filters;
global using Il2CppAssets.Scripts.Models.Towers.Mods;
global using Il2CppAssets.Scripts.Models.Towers.Mutators;
global using Il2CppAssets.Scripts.Models.Towers.Mutators.Conditions;
global using Il2CppAssets.Scripts.Models.Towers.Mutators.Conditions.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Pets;
global using Il2CppAssets.Scripts.Models.Towers.PlacementBehaviors;
global using Il2CppAssets.Scripts.Models.Towers.Projectiles;
global using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.Mutators;
global using Il2CppAssets.Scripts.Models.Towers.Props;
global using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
global using Il2CppAssets.Scripts.Models.Towers.Upgrades;
global using Il2CppAssets.Scripts.Models.Towers.Weapons;
global using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
global using Il2CppAssets.Scripts.Models.TowerSelectionMenuTheme;
global using Il2CppAssets.Scripts.Models.TowerSets;
global using Il2CppAssets.Scripts.Models.TowerSets.Mods;
global using Il2CppAssets.Scripts.Models.Types;
global using Il2CppAssets.Scripts.Simulation;
global using Il2CppAssets.Scripts.Simulation.Action;
global using Il2CppAssets.Scripts.Simulation.Audio;
global using Il2CppAssets.Scripts.Simulation.Display;
global using Il2CppAssets.Scripts.Simulation.Effects;
global using Il2CppAssets.Scripts.Simulation.Factory;
global using Il2CppAssets.Scripts.Simulation.Freeplay;
global using Il2CppAssets.Scripts.Simulation.GenericBehavior;
global using Il2CppAssets.Scripts.Simulation.GeraldoItems;
global using Il2CppAssets.Scripts.Simulation.Input;
global using Il2CppAssets.Scripts.Simulation.Map.Actions;
global using Il2CppAssets.Scripts.Simulation.Map.Gizmos;
global using Il2CppAssets.Scripts.Simulation.Map.Triggers;
global using Il2CppAssets.Scripts.Simulation.Objects;
global using Il2CppAssets.Scripts.Simulation.Physics;
global using Il2CppAssets.Scripts.Simulation.Powers;
global using Il2CppAssets.Scripts.Simulation.Powers.Effects;
global using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
global using Il2CppAssets.Scripts.Simulation.Towers;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.PlacementBehaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Emissions;
global using Il2CppAssets.Scripts.Simulation.Towers.Emissions.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Emmisions;
global using Il2CppAssets.Scripts.Simulation.Towers.Filters;
global using Il2CppAssets.Scripts.Simulation.Towers.Mutators;
global using Il2CppAssets.Scripts.Simulation.Towers.Mutators.Conditions;
global using Il2CppAssets.Scripts.Simulation.Towers.Mutators.Conditions.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Pets;
global using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
global using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Props;
global using Il2CppAssets.Scripts.Simulation.Towers.TowerFilters;
global using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
global using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Track;
global using Il2CppAssets.Scripts.Simulation.Track.RoundManagers;
global using Il2CppAssets.Scripts.Simulation.Track.Spawners;
global using Il2CppAssets.Scripts.Simulation.Tracking;
global using Il2CppAssets.Scripts.Simulation.Utils;
global using Il2CppAssets.Scripts.Unity.Bridge;
global using Il2CppAssets.Scripts.Unity.UI.InGameMenu.StoreMenu;
global using Il2CppAssets.Scripts.Unity.UI_New;
global using Il2CppAssets.Scripts.Unity.UI_New.Achievements;
global using Il2CppAssets.Scripts.Unity.UI_New.AppleArcade;
global using Il2CppAssets.Scripts.Unity.UI_New.Callouts;
global using Il2CppAssets.Scripts.Unity.UI_New.Callouts.CalloutTypes;
global using Il2CppAssets.Scripts.Unity.UI_New.Callouts.CalloutTypes.CalloutLinked;
global using Il2CppAssets.Scripts.Unity.UI_New.Callouts.CalloutTypes.CalloutNeutral;
global using Il2CppAssets.Scripts.Unity.UI_New.ChallengeEditor;
global using Il2CppAssets.Scripts.Unity.UI_New.ContestedTerritory;
global using Il2CppAssets.Scripts.Unity.UI_New.ContestedTerritory.CameraRig;
global using Il2CppAssets.Scripts.Unity.UI_New.ContestedTerritory.MonoScripts;
global using Il2CppAssets.Scripts.Unity.UI_New.Coop;
global using Il2CppAssets.Scripts.Unity.UI_New.DailyChallenge;
global using Il2CppAssets.Scripts.Unity.UI_New.EndGame;
global using Il2CppAssets.Scripts.Unity.UI_New.GameEvents;
global using Il2CppAssets.Scripts.Unity.UI_New.GameOver;
global using Il2CppAssets.Scripts.Unity.UI_New.HeroInGame;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.AbilitiesMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.ActionMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.BloonMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.BossBloons;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.EmotesMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.EmotesMenu.Emotes;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.Races;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.Removables;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.RightMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.RightMenu.Powers;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.StoreMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu.TowerSelectionMenuThemes;
global using Il2CppAssets.Scripts.Utils;

global using Il2CppInternal.Cryptography;
global using Il2CppInternal.Runtime.Augments;
global using Il2CppInternal.Threading.Tasks.Tracing;

global using Il2CppInterop.Common;
global using Il2CppInterop.Common.Attributes;
global using Il2CppInterop.Common.Extensions;
global using Il2CppInterop.Common.Host;
global using Il2CppInterop.Common.Maps;
global using Il2CppInterop.Common.XrefScans;
global using Il2CppInterop.Generator;
global using Il2CppInterop.Generator.Contexts;
global using Il2CppInterop.Generator.Extensions;
global using Il2CppInterop.Generator.MetadataAccess;
global using Il2CppInterop.Generator.Passes;
global using Il2CppInterop.Generator.Runners;
global using Il2CppInterop.Generator.Utils;
global using Il2CppInterop.Generator.XrefScans;
global using Il2CppInterop.Runtime;
global using Il2CppInterop.Runtime.Attributes;
global using Il2CppInterop.Runtime.Injection;
global using Il2CppInterop.Runtime.InteropTypes;
global using Il2CppInterop.Runtime.InteropTypes.Arrays;
global using Il2CppInterop.Runtime.InteropTypes.Fields;
global using Il2CppInterop.Runtime.Runtime;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.Assembly;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.AssemblyName;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.Class;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.EventInfo;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.Exception;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.FieldInfo;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.Image;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.MethodInfo;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.ParameterInfo;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.PropertyInfo;
global using Il2CppInterop.Runtime.Runtime.VersionSpecific.Type;
global using Il2CppInterop.Runtime.Startup;
global using Il2CppInterop.Runtime.XrefScans;

global using Il2CppNinjaKiwi;
global using Il2CppNinjaKiwi.CustomAnimation;
global using Il2CppNinjaKiwi.LiNK;
global using Il2CppNinjaKiwi.LiNK.Lobbies;
global using Il2CppNinjaKiwi.LiNK.Lobbies.LatencyMeasurements;
global using Il2CppNinjaKiwi.NKMulti;
global using Il2CppNinjaKiwi.NKMulti.IO;
global using Il2CppNinjaKiwi.NKMulti.Transfer;
global using Il2CppNinjaKiwi.Players.LiNKAccountControllers;

global using Il2CppTMPro;

global using MelonLoader;
global using MelonLoader.Assertions;
global using MelonLoader.InternalUtils;
global using MelonLoader.Lemons.Cryptography;
global using MelonLoader.Modules;
global using MelonLoader.MonoInternals;
global using MelonLoader.MonoInternals.ResolveInternals;
global using MelonLoader.NativeUtils;
global using MelonLoader.NativeUtils.PEParser;
global using MelonLoader.Preferences;
global using MelonLoader.Preferences.IO;
global using MelonLoader.TinyJSON;
global using MelonLoader.Utils;

global using Microsoft.CodeAnalysis;
global using Microsoft.Win32;
global using Microsoft.Win32.SafeHandles;

global using Mono;
global using Mono.Cecil;
global using Mono.Cecil.Cil;

global using MonoMod;
global using MonoMod.Cil;
global using MonoMod.ModInterop;
global using MonoMod.RuntimeDetour;
global using MonoMod.RuntimeDetour.HookGen;
global using MonoMod.RuntimeDetour.Platforms;
global using MonoMod.Utils;
global using MonoMod.Utils.Cil;

global using Tomlet;
global using Tomlet.Attributes;
global using Tomlet.Exceptions;
global using Tomlet.Models;

global using Unity;
global using Unity.Profiling;
global using Unity.Profiling.LowLevel;
global using Unity.Profiling.LowLevel.Unsafe;
global using Unity.Rendering.HybridV2;

global using UnityEditor.Experimental;

global using UnityEngine;
global using UnityEngine.AddressableAssets;
global using UnityEngine.AddressableAssets.Initialization;
global using UnityEngine.AddressableAssets.ResourceLocators;
global using UnityEngine.AddressableAssets.ResourceProviders;
global using UnityEngine.AddressableAssets.Utility;
global using UnityEngine.Device;
global using UnityEngine.Diagnostics;
global using UnityEngine.Events;
global using UnityEngine.EventSystems;
global using UnityEngine.Experimental.AssetBundlePatching;
global using UnityEngine.Experimental.GlobalIllumination;
global using UnityEngine.Experimental.Playables;
global using UnityEngine.Experimental.Rendering;
global using UnityEngine.Experimental.U2D;
global using UnityEngine.InputSystem.UI;
global using UnityEngine.Networking.PlayerConnection;
global using UnityEngine.Playables;
global using UnityEngine.PlayerLoop;
global using UnityEngine.Pool;
global using UnityEngine.Profiling;
global using UnityEngine.Profiling.Experimental;
global using UnityEngine.Profiling.Memory.Experimental;
global using UnityEngine.ResourceManagement;
global using UnityEngine.ResourceManagement.AsyncOperations;
global using UnityEngine.ResourceManagement.Diagnostics;
global using UnityEngine.ResourceManagement.Exceptions;
global using UnityEngine.ResourceManagement.ResourceLocations;
global using UnityEngine.ResourceManagement.ResourceProviders;
global using UnityEngine.ResourceManagement.Util;
global using UnityEngine.SceneManagement;
global using UnityEngine.Scripting;
global using UnityEngine.Scripting.APIUpdating;
global using UnityEngine.Search;
global using UnityEngine.SearchService;
global using UnityEngine.Serialization;
global using UnityEngine.Sprites;
global using UnityEngine.U2D;
global using UnityEngine.UI;
global using UnityEngine.UI.Collections;

global using UnityEngineInternal;

global using static HarmonyLib.AccessTools;

global using Math = System.Math;
global using MathF = System.MathF;
global using Object = UnityEngine.Object;
global using Screen = UnityEngine.Screen;
global using InputManager = Il2CppAssets.Scripts.Unity.UI_New.InGame.InputManager;

#pragma warning restore IDE0005