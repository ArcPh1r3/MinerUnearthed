﻿using System;
using System.Collections.Generic;
using BepInEx;
using R2API;
using R2API.Utils;
using EntityStates;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;
using KinematicCharacterController;
using EntityStates.Digger;
using BepInEx.Configuration;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace DiggerPlugin
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rob.Aatrox", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rob.Direseeker", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.DestroyedClone.AncientScepter", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.KomradeSpectre.Aetherium", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Sivelos.SivsItems", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TeamMoonstorm.Starstorm2", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, "DiggerUnearthed", "1.5.3")]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "SurvivorAPI",
        "LoadoutAPI",
        "BuffAPI",
        "LanguageAPI",
        "SoundAPI",
        "EffectAPI",
        "UnlockablesAPI"
    })]

    public class DiggerPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.rob.DiggerUnearthed";

        public const string characterName = "Miner";
        public const string characterSubtitle = "Destructive Drug Addict";
        public const string characterOutro = "..and so he left, adrenaline still rushing through his veins.";
        public const string characterLore = "\nAmong the cold steel of the freighter's ruins and the air of the freezing night, the dim blue light of an audio journal catches your eye. The journal lies in utter disrepair among the remains of the cargo, and yet it persists, remaining functional. The analog buttons on the device's casing pulse slowly, as if inviting anybody to listen to their warped entries, ever-decaying in coherency..." +
                "\n\n<color=#8990A7><CLICK></color>" +
                "\n\n<color=#8990A7>/ / - - L  O  G     1 - - / /</color>" +
                "\n''First log. Date, uhhh... Oh-five, oh-one, twenty-fifty-five. Brass decided to pull us out of the mining job. After months of digging, after months of blood, swe<color=#8990A7>-///////-</color>nd millions spent on this operation, we're just dropping it? Bull<color=#8990A7>-//-</color>.''" +
                "\n\n<color=#8990A7>/ / - - L  O  G     5 - - / /</color>" +
                "\n''<color=#8990A7>-////////-</color>nter of the asteroid. I've never seen anything like it. It was like a dr<color=#8990A7>-////////-</color>nd fractal... The inside is almost like a kaleidoscope. It's gotta be worth<color=#8990A7>-/////-</color>nd brass wants us gone, ASAP. Well, they can kiss m<color=#8990A7>-////-</color>. And I'm going out there tonight to dig the rest of it out... If I can fence this, the next thousand paychecks won't hold a candle. I've been waiting for an opportunity like this.''" +
                "\n\n<color=#8990A7>/ / - - L  O  G     7 - - / /</color>" +
                "\n''Pretty sure they know everything. They're questioning people now. I stashed the artifact in a vent in<color=#8990A7>-//////-</color>, should be safe. Freighter called the UE<color=#8990A7>-////-</color>t's docking in a few months. I just have to hold out and sneak on.''" +
                "\n\n<color=#8990A7>/ / - - L  O  G     15 - - / /</color>" +
                "\n''They're looking for me. Not long before they find me. Hiding in the walls of the ship like a f<color=#8990A7>-//////-</color>rat. Freighter's off schedule. I gave everything for this. I gave <i>everything</i> for this.''" +
                "\n\n<color=#8990A7>/ / - - L  O  G     16 - - / /</color>" +
                "\n''Somewhere in the hull. Nobody comes back here. Not even maintenance. I should be safe until the ship arrives.''" +
                "\n\n<color=#8990A7>/ / - - L  O  G     18 - - / /</color>" +
                "\n''Ship still on hold. Can't get an idea of when it won't be.'' <color=#8990A7><Groan>.</color> ''This is ago<color=#8990A7>-//-</color>zing.''" +
                "\n\n <color=#8990A7>/ / - - L  O  G     29 - - / /</color>" +
                "\n''<color=#8990A7>-//////////-</color>'s here. Finally. Finally. Have to get the artifact on board. Security everywhere, though. Mercs...'' <color=#8990A7><Distant voices have an indiscernible conversation.></color> ''Okay. Shut up. Shut up and go. I'm going. Going. Tonight.''" +
                "\n\n <color=#8990A7>/ / - - L  O  G     31 - - / /</color>" +
                "\n''<color=#8990A7>-//////-</color>omething's happening to the ship. Cargo flying out. Lost the artifact. Lost everything. . . . Lost everythi<color=#8990A7>-//-</color>.''" +
                "\n\nThe audio journal's screen sparks and pops, leaving you in complete darkness, complemented by the deafening silence brought about by the ominous last words of the miner.";

        public static DiggerPlugin instance;

        public static GameObject characterPrefab;
        public static GameObject characterDisplay;

        public static List<GameObject> bodyPrefabs = new List<GameObject>();
        public static List<GameObject> masterPrefabs = new List<GameObject>();
        public static List<GameObject> projectilePrefabs = new List<GameObject>();
        public static List<SurvivorDef> survivorDefs = new List<SurvivorDef>();

        public GameObject doppelganger;

        public static event Action awake;
        public static event Action start;

        public static readonly Color characterColor = new Color(0.956862745f, 0.874509803f, 0.603921568f);

        public SkillLocator skillLocator;

        public static float adrenalineCap;

        public static GameObject backblastEffect;
        public static GameObject crushExplosionEffect;

        public static SkillDef scepterSpecialSkillDef;

        public static bool hasAatrox = false;
        public static bool direseekerInstalled = false;
        public static bool aetheriumInstalled = false;
        public static bool sivsItemsInstalled = false;
        public static bool starstormInstalled = false;
        public static uint blacksmithSkinIndex = 4;

        public static ConfigEntry<bool> forceUnlock;
        public static ConfigEntry<float> maxAdrenaline;
        public static ConfigEntry<bool> extraSkins;
        public static ConfigEntry<bool> styleUI;
        public static ConfigEntry<bool> enableDireseeker;
        public static ConfigEntry<bool> enableDireseekerSurvivor;
        public static ConfigEntry<bool> fatAcrid;

        public static ConfigEntry<float> gougeDamage;
        public static ConfigEntry<float> crushDamage;

        public static ConfigEntry<float> drillChargeDamage;
        public static ConfigEntry<float> drillChargeCooldown;

        public static ConfigEntry<float> drillBreakDamage;
        public static ConfigEntry<float> drillBreakCooldown;

        public static ConfigEntry<KeyCode> restKeybind;
        public static ConfigEntry<KeyCode> tauntKeybind;
        public static ConfigEntry<KeyCode> jokeKeybind;

        public DiggerPlugin()
        {
            awake += DiggerPlugin_Load;
            start += DiggerPlugin_LoadStart;
        }

        private void DiggerPlugin_Load()
        {
            instance = this;

            ConfigShit();
            Assets.PopulateAssets();

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.TeamMoonstorm.Starstorm2"))
            {
                starstormInstalled = true;
                blacksmithSkinIndex++;
            }

            CreateDisplayPrefab();
            CreatePrefab();
            RegisterCharacter();
            Skins.RegisterSkins();

            //direseeker compat
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rob.Direseeker"))
            {
                direseekerInstalled = true;
            }
            //aetherium item displays- dll won't compile without a reference to aetherium
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.KomradeSpectre.Aetherium"))
            {
                aetheriumInstalled = true;
            }
            //sivs item displays- dll won't compile without a reference
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Sivelos.SivsItems"))
            {
                sivsItemsInstalled = true;
            }
            //scepter stuff- dll won't compile without a reference to TILER2 and ClassicItems
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.DestroyedClone.AncientScepter"))
            {
                ScepterSkillSetup();
                ScepterSetup();
            }

            ItemDisplays.RegisterDisplays();
            Unlockables.RegisterUnlockables();
            RegisterEffects();
            Buffs.RegisterBuffs();
            CreateDoppelganger();

            Direseeker.CreateDireseeker();

            //the il is broken and idk how to fix, sorry
            //ILHook();
            Hook();

            new ContentPacks().Initialize();
        }

        private void DiggerPlugin_LoadStart()
        {
            if (styleUI.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rob.Aatrox"))
            {
                //hasAatrox = true;
                //AddStyleMeter();
            }

            //Direseeker.LateSetup();
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private void ScepterSetup()
        {
            AncientScepter.AncientScepterItem.instance.RegisterScepterSkill(scepterSpecialSkillDef, "MinerBody", SkillSlot.Special, 0);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private void AddStyleMeter()
        {
            characterPrefab.AddComponent<Aatrox.GenericStyleController>();
        }

        public void Awake()
        {
            Action awake = DiggerPlugin.awake;
            if (awake == null)
            {
                return;
            }
            awake();
        }
        public void Start()
        {
            Action start = DiggerPlugin.start;
            if (start == null)
            {
                return;
            }
            start();
        }

        //shit is right
        private void ConfigShit()
        {
            forceUnlock = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Force Unlock"), false, new ConfigDescription("Unlocks the Miner by default", null, Array.Empty<object>()));
            maxAdrenaline = base.Config.Bind<float>(new ConfigDefinition("01 - General Settings", "Adrenaline Cap"), 50, new ConfigDescription("Max Adrenaline stacks allowed", null, Array.Empty<object>()));
            adrenalineCap = maxAdrenaline.Value - 1;
            extraSkins = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Extra Skins"), false, new ConfigDescription("Enables a bunch of extra skins", null, Array.Empty<object>()));
            styleUI = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Style Rank"), true, new ConfigDescription("Enables a style ranking system taken from Devil May Cry (only if Aatrox is installed as well)", null, Array.Empty<object>()));
            enableDireseeker = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Enable Direseeker"), true, new ConfigDescription("Enables the new boss", null, Array.Empty<object>()));
            enableDireseekerSurvivor = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Direseeker Survivor"), false, new ConfigDescription("Enables the new boss as a survivor?", null, Array.Empty<object>()));
            fatAcrid = base.Config.Bind<bool>(new ConfigDefinition("01 - General Settings", "Perro Grande"), false, new ConfigDescription("Enables fat Acrid as a lategame scav-tier boss", null, Array.Empty<object>()));

            restKeybind = base.Config.Bind<KeyCode>(new ConfigDefinition("02 - Keybinds", "Rest Emote"), KeyCode.Alpha1, new ConfigDescription("Keybind used for the Rest emote", null, Array.Empty<object>()));
            tauntKeybind = base.Config.Bind<KeyCode>(new ConfigDefinition("02 - Keybinds", "Taunt Emote"), KeyCode.Alpha2, new ConfigDescription("Keybind used for the Taunt emote", null, Array.Empty<object>()));
            jokeKeybind = base.Config.Bind<KeyCode>(new ConfigDefinition("02 - Keybinds", "Joke Emote"), KeyCode.Alpha3, new ConfigDescription("Keybind used for the Joke emote", null, Array.Empty<object>()));

            gougeDamage = base.Config.Bind<float>(new ConfigDefinition("03 - Gouge", "Damage"), 2.75f, new ConfigDescription("Damage coefficient", null, Array.Empty<object>()));

            crushDamage = base.Config.Bind<float>(new ConfigDefinition("04 - Crush", "Demage"), 3.2f, new ConfigDescription("Damage coefficient", null, Array.Empty<object>()));

            drillChargeDamage = base.Config.Bind<float>(new ConfigDefinition("05 - Drill Charge", "Damage"), 1.8f, new ConfigDescription("Damage coefficient per hit", null, Array.Empty<object>()));
            drillChargeCooldown = base.Config.Bind<float>(new ConfigDefinition("05 - Drill Charge", "Cooldown"), 7f, new ConfigDescription("Base cooldown", null, Array.Empty<object>()));

            drillBreakDamage = base.Config.Bind<float>(new ConfigDefinition("06 - Drill Crack Hammer", "Damage"), 2f, new ConfigDescription("Damage coefficient", null, Array.Empty<object>()));
            drillBreakCooldown = base.Config.Bind<float>(new ConfigDefinition("06 - Crack Hammer", "Cooldown"), 3f, new ConfigDescription("Base cooldown", null, Array.Empty<object>()));
        }

        private void RegisterEffects()
        {
            backblastEffect = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFX"), "MinerBackblastEffect", true);

            if (!backblastEffect.GetComponent<EffectComponent>()) backblastEffect.AddComponent<EffectComponent>();
            if (!backblastEffect.GetComponent<VFXAttributes>()) backblastEffect.AddComponent<VFXAttributes>();
            if (!backblastEffect.GetComponent<NetworkIdentity>()) backblastEffect.AddComponent<NetworkIdentity>();

            backblastEffect.GetComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;

            EffectAPI.AddEffect(backblastEffect);

            crushExplosionEffect = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFX"), "DiggerCrushExplosionEffect", true);

            if (!crushExplosionEffect.GetComponent<EffectComponent>()) crushExplosionEffect.AddComponent<EffectComponent>();
            if (!crushExplosionEffect.GetComponent<VFXAttributes>()) crushExplosionEffect.AddComponent<VFXAttributes>();
            if (!crushExplosionEffect.GetComponent<NetworkIdentity>()) crushExplosionEffect.AddComponent<NetworkIdentity>();

            crushExplosionEffect.GetComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;
            crushExplosionEffect.GetComponent<EffectComponent>().applyScale = true;
            crushExplosionEffect.GetComponent<EffectComponent>().parentToReferencedTransform = true;

            EffectAPI.AddEffect(crushExplosionEffect);
        }

        private void Hook()
        {
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            On.RoR2.SceneDirector.Start += SceneDirector_Start;
        }

        private void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "dampcavesimple")
            {
                GameObject hammer = Instantiate(Assets.blacksmithHammer);
                hammer.transform.position = new Vector3(76, -143.5f, -526);
                hammer.transform.rotation = Quaternion.Euler(new Vector3(340, 340, 70));
                hammer.transform.localScale = new Vector3(200, 200, 200);

                GameObject anvil = Instantiate(Assets.blacksmithAnvil);
                anvil.transform.position = new Vector3(76.8f, -142.5f, -530);
                anvil.transform.rotation = Quaternion.Euler(new Vector3(10, 90, 0));
                anvil.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            orig(self);
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo info)
        {
            bool isCleaving = false;
            if (info.attacker)
            {
                CharacterBody cb = info.attacker.GetComponent<CharacterBody>();
                if (cb)
                {
                    if (cb.baseNameToken == "MINER_NAME" && info.damageType.HasFlag(DamageType.ApplyMercExpose))
                    {
                        info.damageType = DamageType.Generic;
                        isCleaving = true;
                    }
                }
            }

            orig(self, info);

            if (isCleaving && !info.rejected)
            {
                if (self.body) self.body.AddTimedBuff(Buffs.cleaveBuff, 2.5f * info.procCoefficient);
            }
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            if (self)
            {
                if (self.HasBuff(Buffs.goldRushBuff))
                {
                    int count = self.GetBuffCount(Buffs.goldRushBuff);
                    self.attackSpeed += (count * 0.1f);
                    self.moveSpeed += (count * 0.15f);
                    self.regen += (count * 0.25f);
                }

                if (self.HasBuff(Buffs.cleaveBuff))
                {
                    int count = self.GetBuffCount(Buffs.cleaveBuff);
                    self.armor -= (count * 3f);
                }
            }
        }

        private void ILHook()
        {
            /*IL.RoR2.CharacterBody.RecalculateStats += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                    x => x.MatchLdloc(50),
                    x => x.MatchLdloc(51)
                    );
                //x => x.MatchLdloc(52),
                //x => x.MatchDiv(),
                //x => x.MatchMul()
                //);
                c.Index += 2;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<CharacterBody, float>>((charBody) =>
                {
                    float output = 0f;
                    if (charBody.HasBuff(goldRush))
                    {
                        output = 0.4f;
                    }
                    return output;
                });
                c.Emit(OpCodes.Add);
            };

            IL.RoR2.CharacterBody.RecalculateStats += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                    x => x.MatchMul(),
                    x => x.MatchAdd(),
                    x => x.MatchStloc(58)
                    );
                c.Index += 2;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<CharacterBody, float>>((charBody) =>
                {
                    float output = 0f;
                    if (charBody.HasBuff(goldRush))
                    {
                        output = charBody.GetBuffCount(goldRush) * 0.12f;
                    }
                    return output;
                });
                c.Emit(OpCodes.Add);
            };

            IL.RoR2.CharacterBody.RecalculateStats += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                    x => x.MatchMul(),
                    x => x.MatchLdloc(43),
                    x => x.MatchMul(),
                    x => x.MatchStloc(47)
                    );
                c.Index += 3;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<CharacterBody, float>>((charBody) =>
                {
                    float output = 0f;
                    if (charBody.HasBuff(goldRush))
                    {
                        output = charBody.GetBuffCount(goldRush) * 1;
                    }
                    return output;
                });
                c.Emit(OpCodes.Add);
            };*/
        }

        private static GameObject CreateModel(GameObject main, int index)
        {
            Destroy(main.transform.Find("ModelBase").gameObject);
            Destroy(main.transform.Find("CameraPivot").gameObject);
            Destroy(main.transform.Find("AimOrigin").gameObject);

            GameObject model = null;

            if (index == 0) model = Assets.mainAssetBundle.LoadAsset<GameObject>("mdlMiner");
            else if (index == 1) model = Assets.mainAssetBundle.LoadAsset<GameObject>("MinerDisplay");

            return model;
        }

        private static void CreateDisplayPrefab()
        {
            //spaghetti incoming
            GameObject tempDisplay = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), "MinerDisplay");

            GameObject model = CreateModel(tempDisplay, 1);

            GameObject gameObject = new GameObject("ModelBase");
            gameObject.transform.parent = tempDisplay.transform;
            gameObject.transform.localPosition = new Vector3(0f, -0.9f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject gameObject2 = new GameObject("CameraPivot");
            gameObject2.transform.parent = gameObject.transform;
            gameObject2.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            gameObject2.transform.localRotation = Quaternion.identity;
            gameObject2.transform.localScale = Vector3.one;

            GameObject gameObject3 = new GameObject("AimOrigin");
            gameObject3.transform.parent = gameObject.transform;
            gameObject3.transform.localPosition = new Vector3(0f, 1.8f, 0f);
            gameObject3.transform.localRotation = Quaternion.identity;
            gameObject3.transform.localScale = Vector3.one;

            Transform transform = model.transform;
            transform.parent = gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            ModelLocator modelLocator = tempDisplay.GetComponent<ModelLocator>();
            modelLocator.modelTransform = transform;
            modelLocator.modelBaseTransform = gameObject.transform;

            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            CharacterModel characterModel = model.AddComponent<CharacterModel>();
            characterModel.body = null;
            characterModel.baseRendererInfos = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = model.GetComponentInChildren<SkinnedMeshRenderer>().material,
                    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("DiamondPickL").GetComponent<MeshRenderer>().material,
                    renderer = childLocator.FindChild("DiamondPickL").GetComponent<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("DiamondPickR").GetComponent<MeshRenderer>().material,
                    renderer = childLocator.FindChild("DiamondPickR").GetComponent<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("JokeC4").GetComponentInChildren<MeshRenderer>().material,
                    renderer = childLocator.FindChild("JokeC4").GetComponentInChildren<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                }
            };
            characterModel.autoPopulateLightInfos = true;
            characterModel.invisibilityCount = 0;
            characterModel.temporaryOverlays = new List<TemporaryOverlay>();

            Shader hotpoo = Resources.Load<Shader>("Shaders/Deferred/hgstandard");
            characterModel.baseRendererInfos[0].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[1].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[2].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[3].defaultMaterial.shader = hotpoo;

            characterModel.SetFieldValue("mainSkinnedMeshRenderer", characterModel.baseRendererInfos[0].renderer.gameObject.GetComponent<SkinnedMeshRenderer>());

            characterDisplay = PrefabAPI.InstantiateClone(tempDisplay.GetComponent<ModelLocator>().modelBaseTransform.gameObject, "MinerDisplay", true);

            characterDisplay.AddComponent<MenuSound>();
        }

        private static void CreatePrefab()
        {
            characterPrefab = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), "MinerBody");

            characterPrefab.GetComponent<NetworkIdentity>().localPlayerAuthority = true;

            GameObject model = CreateModel(characterPrefab, 0);

            GameObject gameObject = new GameObject("ModelBase");
            gameObject.transform.parent = characterPrefab.transform;
            gameObject.transform.localPosition = new Vector3(0f, -0.9f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject gameObject2 = new GameObject("CameraPivot");
            gameObject2.transform.parent = gameObject.transform;
            gameObject2.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            gameObject2.transform.localRotation = Quaternion.identity;
            gameObject2.transform.localScale = Vector3.one;

            GameObject gameObject3 = new GameObject("AimOrigin");
            gameObject3.transform.parent = gameObject.transform;
            gameObject3.transform.localPosition = new Vector3(0f, 1.5f, 0f);
            gameObject3.transform.localRotation = Quaternion.identity;
            gameObject3.transform.localScale = Vector3.one;

            Transform transform = model.transform;
            transform.parent = gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            CharacterDirection characterDirection = characterPrefab.GetComponent<CharacterDirection>();
            characterDirection.moveVector = Vector3.zero;
            characterDirection.targetTransform = gameObject.transform;
            characterDirection.overrideAnimatorForwardTransform = null;
            characterDirection.rootMotionAccumulator = null;
            characterDirection.modelAnimator = model.GetComponentInChildren<Animator>();
            characterDirection.driveFromRootRotation = false;
            characterDirection.turnSpeed = 720f;

            CharacterBody bodyComponent = characterPrefab.GetComponent<CharacterBody>();
            bodyComponent.name = "MinerBody";
            bodyComponent.baseNameToken = "MINER_NAME";
            bodyComponent.subtitleNameToken = "MINER_SUBTITLE";
            bodyComponent.bodyFlags = CharacterBody.BodyFlags.ImmuneToExecutes;
            bodyComponent.rootMotionInMainState = false;
            bodyComponent.mainRootSpeed = 0;
            bodyComponent.baseMaxHealth = 120;
            bodyComponent.levelMaxHealth = 48;
            bodyComponent.baseRegen = 0.5f;
            bodyComponent.levelRegen = 0.25f;
            bodyComponent.baseMaxShield = 0;
            bodyComponent.levelMaxShield = 0;
            bodyComponent.baseMoveSpeed = 7;
            bodyComponent.levelMoveSpeed = 0;
            bodyComponent.baseAcceleration = 80;
            bodyComponent.baseJumpPower = 15;
            bodyComponent.levelJumpPower = 0;
            bodyComponent.baseDamage = 12;
            bodyComponent.levelDamage = 2.4f;
            bodyComponent.baseAttackSpeed = 1;
            bodyComponent.levelAttackSpeed = 0;
            bodyComponent.baseCrit = 1;
            bodyComponent.levelCrit = 0;
            bodyComponent.baseArmor = 20;
            bodyComponent.levelArmor = 0;
            bodyComponent.baseJumpCount = 1;
            bodyComponent.sprintingSpeedMultiplier = 1.45f;
            bodyComponent.wasLucky = false;
            bodyComponent.hideCrosshair = false;
            bodyComponent.crosshairPrefab = Resources.Load<GameObject>("Prefabs/Crosshair/SimpleDotCrosshair");
            bodyComponent.aimOriginTransform = gameObject3.transform;
            bodyComponent.hullClassification = HullClassification.Human;
            bodyComponent.portraitIcon = Assets.charPortrait;
            bodyComponent.isChampion = false;
            bodyComponent.currentVehicle = null;
            bodyComponent.skinIndex = 0U;
            bodyComponent.bodyColor = characterColor;

            LoadoutAPI.AddSkill(typeof(DiggerMain));
            LoadoutAPI.AddSkill(typeof(BaseEmote));
            LoadoutAPI.AddSkill(typeof(Rest));
            LoadoutAPI.AddSkill(typeof(Taunt));
            LoadoutAPI.AddSkill(typeof(Joke));

            var stateMachine = bodyComponent.GetComponent<EntityStateMachine>();
            stateMachine.mainStateType = new SerializableEntityStateType(typeof(DiggerMain));

            CharacterMotor characterMotor = characterPrefab.GetComponent<CharacterMotor>();
            characterMotor.walkSpeedPenaltyCoefficient = 1f;
            characterMotor.characterDirection = characterDirection;
            characterMotor.muteWalkMotion = false;
            characterMotor.mass = 100f;
            characterMotor.airControl = 0.25f;
            characterMotor.disableAirControlUntilCollision = false;
            characterMotor.generateParametersOnAwake = true;

            CameraTargetParams cameraTargetParams = characterPrefab.GetComponent<CameraTargetParams>();
            cameraTargetParams.cameraParams = Resources.Load<GameObject>("Prefabs/CharacterBodies/MercBody").GetComponent<CameraTargetParams>().cameraParams;
            cameraTargetParams.cameraPivotTransform = null;
            cameraTargetParams.aimMode = CameraTargetParams.AimType.Standard;
            cameraTargetParams.recoil = Vector2.zero;
            cameraTargetParams.idealLocalCameraPos = Vector3.zero;
            cameraTargetParams.dontRaycastToPivot = false;

            ModelLocator modelLocator = characterPrefab.GetComponent<ModelLocator>();
            modelLocator.modelTransform = transform;
            modelLocator.modelBaseTransform = gameObject.transform;

            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            CharacterModel characterModel = model.AddComponent<CharacterModel>();
            characterModel.body = bodyComponent;
            characterModel.baseRendererInfos = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = model.GetComponentInChildren<SkinnedMeshRenderer>().material,
                    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("DiamondPickL").GetComponent<MeshRenderer>().material,
                    renderer = childLocator.FindChild("DiamondPickL").GetComponent<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("DiamondPickR").GetComponent<MeshRenderer>().material,
                    renderer = childLocator.FindChild("DiamondPickR").GetComponent<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = childLocator.FindChild("JokeC4").GetComponentInChildren<MeshRenderer>().material,
                    renderer = childLocator.FindChild("JokeC4").GetComponentInChildren<MeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                }
            };
            characterModel.autoPopulateLightInfos = true;
            characterModel.invisibilityCount = 0;
            characterModel.temporaryOverlays = new List<TemporaryOverlay>();

            Shader hotpoo = Resources.Load<Shader>("Shaders/Deferred/hgstandard");
            characterModel.baseRendererInfos[0].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[1].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[2].defaultMaterial.shader = hotpoo;
            characterModel.baseRendererInfos[3].defaultMaterial.shader = hotpoo;

            characterModel.SetFieldValue("mainSkinnedMeshRenderer", characterModel.baseRendererInfos[0].renderer.gameObject.GetComponent<SkinnedMeshRenderer>());

            TeamComponent teamComponent = null;
            if (characterPrefab.GetComponent<TeamComponent>() != null) teamComponent = characterPrefab.GetComponent<TeamComponent>();
            else teamComponent = characterPrefab.GetComponent<TeamComponent>();
            teamComponent.hideAllyCardDisplay = false;
            teamComponent.teamIndex = TeamIndex.None;

            HealthComponent healthComponent = characterPrefab.GetComponent<HealthComponent>();
            healthComponent.shield = 0f;
            healthComponent.barrier = 0f;
            healthComponent.magnetiCharge = 0f;
            healthComponent.body = null;
            healthComponent.dontShowHealthbar = false;
            healthComponent.globalDeathEventChanceCoefficient = 1f;

            characterPrefab.GetComponent<Interactor>().maxInteractionDistance = 3f;
            characterPrefab.GetComponent<InteractionDriver>().highlightInteractor = true;

            CharacterDeathBehavior characterDeathBehavior = characterPrefab.GetComponent<CharacterDeathBehavior>();
            characterDeathBehavior.deathStateMachine = characterPrefab.GetComponent<EntityStateMachine>();
            //characterDeathBehavior.deathState = new SerializableEntityStateType(typeof(GenericCharacterDeath));

            SfxLocator sfxLocator = characterPrefab.GetComponent<SfxLocator>();
            //sfxLocator.deathSound = Sounds.DeathSound;
            sfxLocator.barkSound = "";
            sfxLocator.openSound = "";
            sfxLocator.landingSound = "Play_char_land";
            sfxLocator.fallDamageSound = "Play_char_land_fall_damage";
            sfxLocator.aliveLoopStart = "";
            sfxLocator.aliveLoopStop = "";

            Rigidbody rigidbody = characterPrefab.GetComponent<Rigidbody>();
            rigidbody.mass = 100f;
            rigidbody.drag = 0f;
            rigidbody.angularDrag = 0f;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            rigidbody.interpolation = RigidbodyInterpolation.None;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rigidbody.constraints = RigidbodyConstraints.None;

            CapsuleCollider capsuleCollider = characterPrefab.GetComponent<CapsuleCollider>();
            capsuleCollider.isTrigger = false;
            capsuleCollider.material = null;
            capsuleCollider.center = new Vector3(0f, 0f, 0f);
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 1.82f;
            capsuleCollider.direction = 1;

            KinematicCharacterMotor kinematicCharacterMotor = characterPrefab.GetComponent<KinematicCharacterMotor>();
            kinematicCharacterMotor.CharacterController = characterMotor;
            kinematicCharacterMotor.Capsule = capsuleCollider;
            kinematicCharacterMotor.Rigidbody = rigidbody;

            kinematicCharacterMotor.DetectDiscreteCollisions = false;
            kinematicCharacterMotor.GroundDetectionExtraDistance = 0f;
            kinematicCharacterMotor.MaxStepHeight = 0.2f;
            kinematicCharacterMotor.MinRequiredStepDepth = 0.1f;
            kinematicCharacterMotor.MaxStableSlopeAngle = 55f;
            kinematicCharacterMotor.MaxStableDistanceFromLedge = 0.5f;
            kinematicCharacterMotor.PreventSnappingOnLedges = false;
            kinematicCharacterMotor.MaxStableDenivelationAngle = 55f;
            kinematicCharacterMotor.RigidbodyInteractionType = RigidbodyInteractionType.None;
            kinematicCharacterMotor.PreserveAttachedRigidbodyMomentum = true;
            kinematicCharacterMotor.HasPlanarConstraint = false;
            kinematicCharacterMotor.PlanarConstraintAxis = Vector3.up;
            kinematicCharacterMotor.StepHandling = StepHandlingMethod.None;
            kinematicCharacterMotor.LedgeHandling = true;
            kinematicCharacterMotor.InteractiveRigidbodyHandling = true;
            kinematicCharacterMotor.SafeMovement = false;

            HurtBoxGroup hurtBoxGroup = model.AddComponent<HurtBoxGroup>();

            HurtBox mainHurtbox = model.transform.Find("MainHurtbox").gameObject.AddComponent<HurtBox>();
            mainHurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
            mainHurtbox.healthComponent = healthComponent;
            mainHurtbox.isBullseye = true;
            mainHurtbox.damageModifier = HurtBox.DamageModifier.Normal;
            mainHurtbox.hurtBoxGroup = hurtBoxGroup;
            mainHurtbox.indexInGroup = 0;

            hurtBoxGroup.hurtBoxes = new HurtBox[]
            {
                mainHurtbox
            };

            hurtBoxGroup.mainHurtBox = mainHurtbox;
            hurtBoxGroup.bullseyeCount = 1;

            HitBoxGroup hitBoxGroup = model.AddComponent<HitBoxGroup>();

            GameObject crushHitbox = new GameObject("CrushHitbox");
            crushHitbox.transform.parent = childLocator.FindChild("SwingCenter");
            crushHitbox.transform.localPosition = new Vector3(0f, 0f, 0f);
            crushHitbox.transform.localRotation = Quaternion.identity;
            crushHitbox.transform.localScale = Vector3.one * 0.08f;

            HitBox hitBox = crushHitbox.AddComponent<HitBox>();
            crushHitbox.layer = LayerIndex.projectile.intVal;

            hitBoxGroup.hitBoxes = new HitBox[]
            {
                hitBox
            };

            hitBoxGroup.groupName = "Crush";

            HitBoxGroup chargeHitBoxGroup = model.AddComponent<HitBoxGroup>();

            GameObject chargeHitbox = new GameObject("ChargeHitbox");
            chargeHitbox.transform.parent = childLocator.FindChild("SwingCenter");
            chargeHitbox.transform.localPosition = new Vector3(0f, 0f, 0f);
            chargeHitbox.transform.localRotation = Quaternion.identity;
            chargeHitbox.transform.localScale = Vector3.one * 0.05f;

            HitBox chargeHitBox = chargeHitbox.AddComponent<HitBox>();
            chargeHitbox.layer = LayerIndex.projectile.intVal;

            chargeHitBoxGroup.hitBoxes = new HitBox[]
            {
                chargeHitBox
            };

            chargeHitBoxGroup.groupName = "Charge";

            FootstepHandler footstepHandler = model.AddComponent<FootstepHandler>();
            footstepHandler.baseFootstepString = "Play_player_footstep";
            footstepHandler.sprintFootstepOverrideString = "";
            footstepHandler.enableFootstepDust = true;
            footstepHandler.footstepDustPrefab = Resources.Load<GameObject>("Prefabs/GenericFootstepDust");

            RagdollController ragdollController = model.GetComponent<RagdollController>();

            PhysicMaterial physicMat = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<RagdollController>().bones[1].GetComponent<Collider>().material;

            foreach (Transform i in ragdollController.bones)
            {
                if (i)
                {
                    i.gameObject.layer = LayerIndex.ragdoll.intVal;
                    Collider j = i.GetComponent<Collider>();
                    if (j)
                    {
                        j.material = physicMat;
                        j.sharedMaterial = physicMat;
                    }
                    Rigidbody k = i.GetComponent<Rigidbody>();
                    if (k) k.drag = 0.1f;
                }
            }

            AimAnimator aimAnimator = model.AddComponent<AimAnimator>();
            aimAnimator.directionComponent = characterDirection;
            aimAnimator.pitchRangeMax = 60f;
            aimAnimator.pitchRangeMin = -60f;
            aimAnimator.yawRangeMin = -90f;
            aimAnimator.yawRangeMax = 90f;
            aimAnimator.pitchGiveupRange = 30f;
            aimAnimator.yawGiveupRange = 10f;
            aimAnimator.giveupDuration = 3f;
            aimAnimator.inputBank = characterPrefab.GetComponent<InputBankTest>();

            GameObject particlesObject = childLocator.FindChild("AdrenalineFire").gameObject;
            if (particlesObject)
            {
                characterPrefab.AddComponent<AdrenalineParticleTimer>().init(particlesObject);
            }
        }

        private void RegisterCharacter()
        {
            string desc = "The Miner is a fast paced and highly mobile melee survivor who prioritizes getting long kill combos to build stacks of his passive.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Once you get a good number of stacks of Adrenaline, Crush will be your best source of damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Note that charging Drill Charge only affects damage dealt. Aim at the ground or into enemies to deal concentrated damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > You can tap Backblast to travel a short distance. Hold it to go further." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > To The Stars when used low to the ground is great at dealing high amounts of damage to enemies with large hitboxes." + Environment.NewLine + Environment.NewLine;

            string outro = characterOutro;

            LanguageAPI.Add("MINER_NAME", characterName);
            LanguageAPI.Add("MINER_DESCRIPTION", desc);
            LanguageAPI.Add("MINER_SUBTITLE", characterSubtitle);
            LanguageAPI.Add("MINER_LORE", characterLore);
            LanguageAPI.Add("MINER_OUTRO_FLAVOR", outro);

            characterDisplay.AddComponent<NetworkIdentity>();

            string unlockString = "";
            if (!forceUnlock.Value) unlockString = "MINER_UNLOCKABLE_REWARD_ID";

            SurvivorDef survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();
            survivorDef.displayNameToken = "MINER_NAME";
            survivorDef.unlockableDef = null;
            survivorDef.descriptionToken = "MINER_DESCRIPTION";
            survivorDef.primaryColor = characterColor;
            survivorDef.bodyPrefab = characterPrefab;
            survivorDef.displayPrefab = characterDisplay;
            survivorDef.outroFlavorToken = "MINER_OUTRO_FLAVOR";
            survivorDef.hidden = false;
            survivorDef.desiredSortPosition = 17f;

            SkillSetup();

            bodyPrefabs.Add(characterPrefab);
            survivorDefs.Add(survivorDef);
        }

        private void CreateDoppelganger()
        {
            doppelganger = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterMasters/MercMonsterMaster"), "MinerMonsterMaster");
            doppelganger.GetComponent<CharacterMaster>().bodyPrefab = characterPrefab;

            masterPrefabs.Add(doppelganger);
        }

        private void SkillSetup()
        {
            foreach (GenericSkill obj in characterPrefab.GetComponentsInChildren<GenericSkill>())
            {
                BaseUnityPlugin.DestroyImmediate(obj);
            }

            skillLocator = characterPrefab.GetComponent<SkillLocator>();

            PassiveSetup();
            PrimarySetup();
            SecondarySetup();
            UtilitySetup();
            SpecialSetup();
        }

        private void PassiveSetup()
        {
            LanguageAPI.Add("MINER_PASSIVE_NAME", "Gold Rush");
            LanguageAPI.Add("MINER_PASSIVE_DESCRIPTION", "Gain <style=cIsHealth>ADRENALINE</style> on receiving gold, increasing <style=cIsDamage>attack speed</style>, <style=cIsUtility>movement speed</style>, and <style=cIsHealing>health regen</style>. <style=cIsUtility>Any increase in gold refreshes all stacks.</style>");

            skillLocator.passiveSkill.enabled = true;
            skillLocator.passiveSkill.skillNameToken = "MINER_PASSIVE_NAME";
            skillLocator.passiveSkill.skillDescriptionToken = "MINER_PASSIVE_DESCRIPTION";
            skillLocator.passiveSkill.icon = Assets.iconP;
        }

        private void PrimarySetup()
        {
            LoadoutAPI.AddSkill(typeof(Gouge));

            LanguageAPI.Add("KEYWORD_CLEAVING", "<style=cKeywordName>Cleaving</style><style=cSub>Applies a stacking debuff that lowers <style=cIsDamage>armor</style> by <style=cIsHealth>3 per stack</style>.</style>");

            string desc = "<style=cIsUtility>Agile.</style> Wildly swing at nearby enemies for <style=cIsDamage>" + 100f * gougeDamage.Value + "% damage</style>, <style=cIsHealth>cleaving</style> their armor.";

            LanguageAPI.Add("MINER_PRIMARY_GOUGE_NAME", "Gouge");
            LanguageAPI.Add("MINER_PRIMARY_GOUGE_DESCRIPTION", desc);

            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(Gouge));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Any;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon1;
            mySkillDef.skillDescriptionToken = "MINER_PRIMARY_GOUGE_DESCRIPTION";
            mySkillDef.skillName = "MINER_PRIMARY_GOUGE_NAME";
            mySkillDef.skillNameToken = "MINER_PRIMARY_GOUGE_NAME";
            mySkillDef.keywordTokens = new string[] {
                "KEYWORD_AGILE",
                "KEYWORD_CLEAVING"
            };

            LoadoutAPI.AddSkillDef(mySkillDef);

            skillLocator.primary = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            skillLocator.primary.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = skillLocator.primary.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

            LoadoutAPI.AddSkill(typeof(Crush));

            desc = "<style=cIsUtility>Agile.</style> Crush nearby enemies for <style=cIsDamage>" + 100f * crushDamage.Value + "% damage</style>. <style=cIsUtility>Range increases with attack speed</style>.";

            LanguageAPI.Add("MINER_PRIMARY_CRUSH_NAME", "Crush");
            LanguageAPI.Add("MINER_PRIMARY_CRUSH_DESCRIPTION", desc);

            mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(Crush));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Any;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon1B;
            mySkillDef.skillDescriptionToken = "MINER_PRIMARY_CRUSH_DESCRIPTION";
            mySkillDef.skillName = "MINER_PRIMARY_CRUSH_NAME";
            mySkillDef.skillNameToken = "MINER_PRIMARY_CRUSH_NAME";
            mySkillDef.keywordTokens = new string[] {
                "KEYWORD_AGILE"
            };

            LoadoutAPI.AddSkillDef(mySkillDef);

            Array.Resize(ref skillFamily.variants, skillFamily.variants.Length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }

        private void SecondarySetup()
        {
            LoadoutAPI.AddSkill(typeof(DrillChargeStart));
            LoadoutAPI.AddSkill(typeof(DrillCharge));

            string desc = "Charge up for 1 second, then dash into enemies for up to <style=cIsDamage>6x" + 100f * drillChargeDamage.Value + "% damage</style>. <style=cIsUtility>You cannot be hit during and following the dash.</style>";

            LanguageAPI.Add("MINER_SECONDARY_CHARGE_NAME", "Drill Charge");
            LanguageAPI.Add("MINER_SECONDARY_CHARGE_DESCRIPTION", desc);

            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(DrillChargeStart));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = drillChargeCooldown.Value;
            mySkillDef.beginSkillCooldownOnSkillEnd = true;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = true;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.forceSprintDuringState = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon2;
            mySkillDef.skillDescriptionToken = "MINER_SECONDARY_CHARGE_DESCRIPTION";
            mySkillDef.skillName = "MINER_SECONDARY_CHARGE_NAME";
            mySkillDef.skillNameToken = "MINER_SECONDARY_CHARGE_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            skillLocator.secondary = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            skillLocator.secondary.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = skillLocator.secondary.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

            LoadoutAPI.AddSkill(typeof(DrillBreakStart));
            LoadoutAPI.AddSkill(typeof(DrillBreak));

            desc = "Dash forward, exploding for <style=cIsDamage>2x" + 100f * drillBreakDamage.Value + "% damage</style> on contact with an enemy. <style=cIsUtility>You cannot be hit during and following the dash.</style>";

            LanguageAPI.Add("MINER_SECONDARY_BREAK_NAME", "Crack Hammer");
            LanguageAPI.Add("MINER_SECONDARY_BREAK_DESCRIPTION", desc);

            mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(DrillBreakStart));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = drillBreakCooldown.Value;
            mySkillDef.beginSkillCooldownOnSkillEnd = true;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = true;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.forceSprintDuringState = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon2B;
            mySkillDef.skillDescriptionToken = "MINER_SECONDARY_BREAK_DESCRIPTION";
            mySkillDef.skillName = "MINER_SECONDARY_BREAK_NAME";
            mySkillDef.skillNameToken = "MINER_SECONDARY_BREAK_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            Array.Resize(ref skillFamily.variants, skillFamily.variants.Length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }

        private void UtilitySetup()
        {
            LoadoutAPI.AddSkill(typeof(BackBlast));

            LanguageAPI.Add("MINER_UTILITY_BACKBLAST_NAME", "Backblast");
            LanguageAPI.Add("MINER_UTILITY_BACKBLAST_DESCRIPTION", "<style=cIsUtility>Stunning.</style> Blast backwards a variable distance, hitting all enemies in a large radius for <style=cIsDamage>" + 100f * BackBlast.damageCoefficient + "% damage</style>. <style=cIsUtility>You cannot be hit while dashing.</style>");

            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(BackBlast));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 5;
            mySkillDef.beginSkillCooldownOnSkillEnd = true;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon3;
            mySkillDef.skillDescriptionToken = "MINER_UTILITY_BACKBLAST_DESCRIPTION";
            mySkillDef.skillName = "MINER_UTILITY_BACKBLAST_NAME";
            mySkillDef.skillNameToken = "MINER_UTILITY_BACKBLAST_NAME";
            mySkillDef.keywordTokens = new string[] {
                "KEYWORD_STUNNING"
            };

            LoadoutAPI.AddSkillDef(mySkillDef);

            skillLocator.utility = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            skillLocator.utility.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = skillLocator.utility.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };

            LoadoutAPI.AddSkill(typeof(CaveIn));

            LanguageAPI.Add("MINER_UTILITY_CAVEIN_NAME", "Cave In");
            LanguageAPI.Add("MINER_UTILITY_CAVEIN_DESCRIPTION", "<style=cIsUtility>Stunning.</style> Blast backwards a short distance, <style=cIsUtility>pulling</style> together all enemies in a large radius. You cannot be hit while dashing.");

            mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(CaveIn));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 5;
            mySkillDef.beginSkillCooldownOnSkillEnd = true;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Skill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = true;
            mySkillDef.cancelSprintingOnActivation = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon3B;
            mySkillDef.skillDescriptionToken = "MINER_UTILITY_CAVEIN_DESCRIPTION";
            mySkillDef.skillName = "MINER_UTILITY_CAVEIN_NAME";
            mySkillDef.skillNameToken = "MINER_UTILITY_CAVEIN_NAME";
            mySkillDef.keywordTokens = new string[] {
                "KEYWORD_STUNNING"
            };

            LoadoutAPI.AddSkillDef(mySkillDef);

            Array.Resize(ref skillFamily.variants, skillFamily.variants.Length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }

        private void SpecialSetup()
        {
            LoadoutAPI.AddSkill(typeof(ToTheStars));

            LanguageAPI.Add("MINER_SPECIAL_TOTHESTARS_NAME", "To The Stars!");
            LanguageAPI.Add("MINER_SPECIAL_TOTHESTARS_DESCRIPTION", "Jump into the air, shooting a wide spray of projectiles downwards for <style=cIsDamage>30x" + 100f * ToTheStars.damageCoefficient + "% damage</style> total.");

            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(ToTheStars));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 6f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.PrioritySkill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.cancelSprintingOnActivation = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon4;
            mySkillDef.skillDescriptionToken = "MINER_SPECIAL_TOTHESTARS_DESCRIPTION";
            mySkillDef.skillName = "MINER_SPECIAL_TOTHESTARS_NAME";
            mySkillDef.skillNameToken = "MINER_SPECIAL_TOTHESTARS_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            skillLocator.special = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            LoadoutAPI.AddSkillFamily(newFamily);
            skillLocator.special.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = skillLocator.special.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }

        private void ScepterSkillSetup()
        {
            LoadoutAPI.AddSkill(typeof(FallingComet));

            LanguageAPI.Add("MINER_SPECIAL_SCEPTERTOTHESTARS_NAME", "Falling Comet");
            LanguageAPI.Add("MINER_SPECIAL_SCEPTERTOTHESTARS_DESCRIPTION", "Jump into the air, shooting a wide spray of explosive projectiles downwards for <style=cIsDamage>30x" + 100f * FallingComet.damageCoefficient + "% damage</style> total, then fall downwards creating a huge blast on impact that deals <style=cIsDamage>" + 100f * FallingComet.blastDamageCoefficient + "% damage</style> and <style=cIsDamage>ignites</style> enemies hit.");

            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();
            mySkillDef.activationState = new SerializableEntityStateType(typeof(FallingComet));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 6f;
            mySkillDef.beginSkillCooldownOnSkillEnd = false;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.PrioritySkill;
            mySkillDef.resetCooldownTimerOnUse = false;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.cancelSprintingOnActivation = true;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            mySkillDef.icon = Assets.icon4S;
            mySkillDef.skillDescriptionToken = "MINER_SPECIAL_SCEPTERTOTHESTARS_DESCRIPTION";
            mySkillDef.skillName = "MINER_SPECIAL_SCEPTERTOTHESTARS_NAME";
            mySkillDef.skillNameToken = "MINER_SPECIAL_SCEPTERTOTHESTARS_NAME";

            LoadoutAPI.AddSkillDef(mySkillDef);

            scepterSpecialSkillDef = mySkillDef;
        }
    }

    public class MenuSound : MonoBehaviour
    {
        private uint playID;

        private void OnEnable()
        {
            this.playID = Util.PlaySound(Sounds.Select, base.gameObject);
        }

        private void OnDestroy()
        {
            AkSoundEngine.StopPlayingID(this.playID);
        }
    }

    public class BlacksmithHammerComponent : MonoBehaviour
    {
        public static event Action<bool> HammerGet = delegate { };

        private void Awake()
        {
            InvokeRepeating("Sex", 0.5f, 0.5f);
        }

        private void Sex()
        {
            Collider[] array = Physics.OverlapSphere(transform.position, 2.5f, LayerIndex.defaultLayer.mask);
            for (int i = 0; i < array.Length; i++)
            {
                CharacterBody component = array[i].GetComponent<CharacterBody>();
                if (component)
                {
                    if (component.baseNameToken == "MINER_NAME")
                    {
                        HammerGet?.Invoke(true);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    public static class Sounds
    {
        public static readonly string Crush = "Crush";
        public static readonly string DrillChargeStart = "DrillCharging";
        public static readonly string DrillCharge = "DrillCharge";
        public static readonly string CrackHammer = "CrackHammer";
        public static readonly string Backblast = "Backblast";
        public static readonly string ToTheStars = "ToTheStars";
        public static readonly string ToTheStarsExplosion = "Explosive";

        public static readonly string Swing = "MinerSwing";
        public static readonly string Hit = "MinerHit";
        public static readonly string Select = "MinerSelect";
    }
}