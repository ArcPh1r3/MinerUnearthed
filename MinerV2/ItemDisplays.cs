﻿using UnityEngine;
using R2API;
using RoR2;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DiggerPlugin
{
    public static class ItemDisplays
    {
        public static List<ItemDisplayRuleSet.NamedRuleGroup> list;
        public static List<ItemDisplayRuleSet.NamedRuleGroup> list2;

        public static GameObject capacitorPrefab;

        private static Dictionary<string, GameObject> itemDisplayPrefabs = new Dictionary<string, GameObject>();

        public static void RegisterDisplays()
        {
            return;

            GameObject bodyPrefab = DiggerPlugin.characterPrefab;

            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            PopulateDisplays();

            ItemDisplayRuleSet itemDisplayRuleSet = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();

            list = new List<ItemDisplayRuleSet.NamedRuleGroup>();
            list2 = new List<ItemDisplayRuleSet.NamedRuleGroup>();

            //add item displays here
            #region Item Displays
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Jetpack",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.004f, -0.002f),
                            localAngles = new Vector3(45, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "GoldGat",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGoldGat"),
                            childName = "Chest",
                            localPos = new Vector3(0.0024f, 0.0075f, 0),
                            localAngles = new Vector3(0, 90, -45),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BFG",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBFG"),
                            childName = "Chest",
                            localPos = new Vector3(0.002f, 0.004f, 0),
                            localAngles = new Vector3(0, 0, 330),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "CritGlasses",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGlasses"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0016f, -0.0014f),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Syringe",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySyringeCluster"),
                            childName = "Chest",
                            localPos = new Vector3(0.002f, 0.0048f, 0),
                            localAngles = new Vector3(30, 90, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Behemoth",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBehemoth"),
                            childName = "Pelvis",
                            localPos = new Vector3(0, -0.002f, 0.0015f),
                            localAngles = new Vector3(90, 180, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Missile",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMissileLauncher"),
                            childName = "Chest",
                            localPos = new Vector3(-0.002f, 0.008f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Dagger",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDagger"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.005f, 0),
                            localAngles = new Vector3(0, 0, 45),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Hoof",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHoof"),
                            childName = "KneeL",
                            localPos = new Vector3(0, 0.002f, -0.0006f),
                            localAngles = new Vector3(70, 0, 0),
                            localScale = new Vector3(0.0011f, 0.0014f, 0.0008f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ChainLightning",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayUkulele"),
                            childName = "PickR",
                            localPos = new Vector3(-0.003f, 0.001f, 0),
                            localAngles = new Vector3(0, 0, 270),
                            localScale = new Vector3(0.008f, 0.008f, 0.008f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "GhostOnKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMask"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0014f, -0.0012f),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.006f, 0.006f, 0.006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Mushroom",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMushroom"),
                            childName = "ShoulderR",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 130),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AttackSpeedOnCrit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWolfPelt"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0024f, -0.001f),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.006f, 0.006f, 0.006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BleedOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTriTip"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0025f, 0.0025f),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "WardOnLevel",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarbanner"),
                            childName = "Chest",
                            localPos = new Vector3(0.0003f, 0,-0.0015f),
                            localAngles = new Vector3(0, 0, 90),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "HealOnCrit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayScythe"),
                            childName = "PickR",
                            localPos = new Vector3(0, 0.0015f, 0),
                            localAngles = new Vector3(0, 90, 270),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "HealWhileSafe",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySnail"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0, -0.0015f, -0.0005f),
                            localAngles = new Vector3(45, 180, 180),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Clover",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayClover"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0, 0.0025f, 0.0005f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BarrierOnOverHeal",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAegis"),
                            childName = "ElbowL",
                            localPos = new Vector3(0, 0, 0.001f),
                            localAngles = new Vector3(90, 180, 0),
                            localScale = new Vector3(0.0035f, 0.0035f, 0.0035f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "GoldOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBoneCrown"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "WarCryOnMultiKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPauldron"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0, 0, 0.001f),
                            localAngles = new Vector3(110, 0, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SprintArmor",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBuckler"),
                            childName = "ElbowR",
                            localPos = new Vector3(0, 0.0015f, 0.001f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "IceRing",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayIceRing"),
                            childName = "PickL",
                            localPos = new Vector3(-0.0016f, 0.0013f, -0.0001f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FireRing",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFireRing"),
                            childName = "PickR",
                            localPos = new Vector3(0.0014f, 0.00122f, -0.0003f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "UtilitySkillMagazine",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0, 0, -0.002f),
                            localAngles = new Vector3(-90, 0, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
                            childName = "ShoulderR",
                            localPos = new Vector3(0, 0, -0.002f),
                            localAngles = new Vector3(-90, 0, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "JumpBoost",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWaxBird"),
                            childName = "Head",
                            localPos = new Vector3(0, -0.0035f, 0),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ArmorReductionOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarhammer"),
                            childName = "PickR",
                            localPos = new Vector3(0.005f, 0.0015f, 0),
                            localAngles = new Vector3(0, 90, 90),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "NearbyDamageBonus",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDiamond"),
                            childName = "PickL",
                            localPos = new Vector3(-0.0024f, 0.0013f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDiamond"),
                            childName = "PickR",
                            localPos = new Vector3(0.002f, 0.0013f, -0.0005f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ArmorPlate",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRepulsionArmorPlate"),
                            childName = "LegL",
                            localPos = new Vector3(-0.0008f, 0.002f, 0),
                            localAngles = new Vector3(90, 90, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "CommandMissile",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMissileRack"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.004f, -0.002f),
                            localAngles = new Vector3(90, 180, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Feather",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFeather"),
                            childName = "ElbowL",
                            localPos = new Vector3(0, 0.0015f, 0),
                            localAngles = new Vector3(-90, -90, 0),
                            localScale = new Vector3(0.0004f, 0.0004f, 0.0004f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Crowbar",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayCrowbar"),
                            childName = "PickL",
                            localPos = new Vector3(-0.003f, 0.001f, 0.0005f),
                            localAngles = new Vector3(5, 90, 355),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FallBoots",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
                            childName = "FootR",
                            localPos = new Vector3(0, 0, 0.0005f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
                            childName = "FootL",
                            localPos = new Vector3(0, 0, 0.0005f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ExecuteLowHealthElite",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGuillotine"),
                            childName = "LegR",
                            localPos = new Vector3(0, 0, 0.0015f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "EquipmentMagazine",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBattery"),
                            childName = "Chest",
                            localPos = new Vector3(0.0004f, 0.0026f, 0.002f),
                            localAngles = new Vector3(0, -90, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "NovaOnHeal",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                            childName = "Head",
                            localPos = new Vector3(-0.0005f, 0, 0),
                            localAngles = new Vector3(0, 180, 20),
                            localScale = new Vector3(0.0075f, 0.0075f, 0.0075f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                            childName = "Head",
                            localPos = new Vector3(0.0005f, 0, 0),
                            localAngles = new Vector3(0, 180, 340),
                            localScale = new Vector3(-0.0075f, 0.0075f, 0.0075f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Infusion",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayInfusion"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.0015f, 0.001f, -0.001f),
                            localAngles = new Vector3(0, 45, 0),
                            localScale = new Vector3(0.006f, 0.006f, 0.006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Medkit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMedkit"),
                            childName = "Chest",
                            localPos = new Vector3(0, -0.0005f, -0.001f),
                            localAngles = new Vector3(290, 180, 0),
                            localScale = new Vector3(0.008f, 0.008f, 0.008f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Bandolier",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBandolier"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0028f, -0.0004f),
                            localAngles = new Vector3(45, 270, 90),
                            localScale = new Vector3(0.008f, 0.008f, 0.008f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BounceNearby",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHook"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0055f, -0.0015f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "IgniteOnKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGasoline"),
                            childName = "LegL",
                            localPos = new Vector3(0, 0.002f, 0.0015f),
                            localAngles = new Vector3(90, 90, 0),
                            localScale = new Vector3(0.0075f, 0.0075f, 0.0075f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "StunChanceOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStunGrenade"),
                            childName = "LegR",
                            localPos = new Vector3(0.001f, 0.002f, 0),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Firework",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFirework"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.0015f, 0.001f, 0.0015f),
                            localAngles = new Vector3(288, 282, 80),
                            localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LunarDagger",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLunarDagger"),
                            childName = "Chest",
                            localPos = new Vector3(-0.0015f, 0.005f, -0.002f),
                            localAngles = new Vector3(45, 120, 90),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Knurl",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayKnurl"),
                            childName = "Chest",
                            localPos = new Vector3(-0.001f, 0.0035f, 0.0015f),
                            localAngles = new Vector3(0, 116, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BeetleGland",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBeetleGland"),
                            childName = "Chest",
                            localPos = new Vector3(0.0035f, 0.005f, 0),
                            localAngles = new Vector3(270, 45, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SprintBonus",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySoda"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.002f, 0.001f, 0),
                            localAngles = new Vector3(270, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SecondarySkillMagazine",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDoubleMag"),
                            childName = "Chest",
                            localPos = new Vector3(0.0015f, 0.004f, -0.003f),
                            localAngles = new Vector3(315, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDoubleMag"),
                            childName = "Chest",
                            localPos = new Vector3(-0.0015f, 0.004f, -0.003f),
                            localAngles = new Vector3(315, 0, 0),
                            localScale = new Vector3(-0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "StickyBomb",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStickyBomb"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.002f, 0.002f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "TreasureCache",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayKey"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.001f, 0.001f, 0.0015f),
                            localAngles = new Vector3(0, 315, 90),
                            localScale = new Vector3(0.015f, 0.015f, 0.015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BossDamageBonus",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAPRound"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.001f, 0, 0.0015f),
                            localAngles = new Vector3(90, 315, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SlowOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBauble"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0045f, -0.003f, -0.002f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ExtraLife",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHippo"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.003f, 0.002f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.004f, 0.004f, 0.004f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "KillEliteFrenzy",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrainstalk"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "RepeatHeal",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayCorpseFlower"),
                            childName = "ShoulderR",
                            localPos = new Vector3(-0.001f, 0, 0),
                            localAngles = new Vector3(270, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AutoCastEquipment",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFossil"),
                            childName = "Chest",
                            localPos = new Vector3(-0.0023f, 0.0015f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "IncreaseHealing",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
                            childName = "Head",
                            localPos = new Vector3(0.0005f, 0.002f, 0),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
                            childName = "Head",
                            localPos = new Vector3(-0.0005f, 0.002f, 0),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, -0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "TitanGoldDuringTP",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGoldHeart"),
                            childName = "Chest",
                            localPos = new Vector3(-0.0016f, 0.0035f, 0.002f),
                            localAngles = new Vector3(0, 0, 285),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SprintWisp",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrokenMask"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0, 0.0005f, 0.001f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BarrierOnKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrooch"),
                            childName = "Chest",
                            localPos = new Vector3(-0.002f, 0.0015f, 0.001f),
                            localAngles = new Vector3(45, 45, 90),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "TPHealingNova",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGlowFlower"),
                            childName = "ShoulderL",
                            localPos = new Vector3(0.0015f, 0, 0),
                            localAngles = new Vector3(30, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LunarUtilityReplacement",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBirdFoot"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.002f, 0.002f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Thorns",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRazorwireLeft"),
                            childName = "ElbowR",
                            localPos = new Vector3(-0.0006f, 0, 0),
                            localAngles = new Vector3(270, 0, 0),
                            localScale = new Vector3(0.004f, 0.006f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LunarPrimaryReplacement",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBirdEye"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0015f, -0.001f),
                            localAngles = new Vector3(0, 90, 90),
                            localScale = new Vector3(0.003f, 0.0035f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "NovaOnLowHealth",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayJellyGuts"),
                            childName = "Head",
                            localPos = new Vector3(0.0008f, -0.001f, 0.001f),
                            localAngles = new Vector3(319, 180, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LunarTrinket",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBeads"),
                            childName = "ElbowL",
                            localPos = new Vector3(-0.00075f, 0.001f, 0),
                            localAngles = new Vector3(0, 0, 90),
                            localScale = new Vector3(0.015f, 0.015f, 0.015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Plant",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayInterstellarDeskPlant"),
                            childName = "ShoulderR",
                            localPos = new Vector3(0, -0.0015f, -0.001f),
                            localAngles = new Vector3(25, 0, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Bear",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBear"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.002f, 0.002f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0035f, 0.0035f, 0.0035f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "DeathMark",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDeathMark"),
                            childName = "HandL",
                            localPos = new Vector3(0, 0.001f, 0.0005f),
                            localAngles = new Vector3(90, 180, 0),
                            localScale = new Vector3(0.00035f, 0.00035f, 0.00035f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ExplodeOnDeath",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWilloWisp"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.002f, 0.001f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0006f, 0.0006f, 0.0006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Seed",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySeed"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.005f, -0.002f),
                            localAngles = new Vector3(270, 45, 0),
                            localScale = new Vector3(0.0006f, 0.0006f, 0.0006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SprintOutOfCombat",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWhip"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0018f, 0, -0.001f),
                            localAngles = new Vector3(0, 45, 15),
                            localScale = new Vector3(0.005f, 0.005f, 0.0025f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "CooldownOnCrit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySkull"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0035f, 0.0015f),
                            localAngles = new Vector3(270, 0, 0),
                            localScale = new Vector3(0.0035f, 0.0035f, 0.0035f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Phasing",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStealthkit"),
                            childName = "KneeL",
                            localPos = new Vector3(0, 0.002f, -0.001f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(0.0025f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "PersonalShield",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldGenerator"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0015f, 0.002f),
                            localAngles = new Vector3(45, 90, 270),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ShockNearby",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTeslaCoil"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.005f, -0.002f),
                            localAngles = new Vector3(315, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ShieldOnly",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
                            childName = "Head",
                            localPos = new Vector3(-0.001f, 0.002f, 0),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
                            childName = "Head",
                            localPos = new Vector3(0.001f, 0.002f, 0),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, -0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AlienHead",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAlienHead"),
                            childName = "PickR",
                            localPos = new Vector3(-0.003f, 0.001f, 0.001f),
                            localAngles = new Vector3(30, 90, 0),
                            localScale = new Vector3(0.015f, 0.015f, 0.015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "HeadHunter",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySkullCrown"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0024f, 0),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.004f, 0.0015f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "EnergizedOnEquipmentUse",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarHorn"),
                            childName = "Pelvis",
                            localPos = new Vector3(-0.003f, 0.0015f, 0),
                            localAngles = new Vector3(0, 0, 90),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "RegenOnKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySteakCurved"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.0022f, 0.002f),
                            localAngles = new Vector3(-45, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Tooth",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayToothMeshLarge"),
                            childName = "Head",
                            localPos = new Vector3(0, 0, -0.0003f),
                            localAngles = new Vector3(45, 0, 0),
                            localScale = new Vector3(0.1f, 0.1f, 0.1f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Pearl",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPearl"),
                            childName = "HandR",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "ShinyPearl",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShinyPearl"),
                            childName = "HandL",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BonusGoldPackOnKill",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTome"),
                            childName = "LegR",
                            localPos = new Vector3(0, 0.002f, 0.0015f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Squid",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySquidTurret"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0015f, -0.0005f),
                            localAngles = new Vector3(0, 270, 0),
                            localScale = new Vector3(0.002f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Icicle",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFrostRelic"),
                            childName = "Base",
                            localPos = new Vector3(0.01f, 0.015f, 0.015f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(1, 1, 1),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Talisman",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTalisman"),
                            childName = "Base",
                            localPos = new Vector3(-0.01f, 0.01f, 0.015f),
                            localAngles = new Vector3(90, 0, 0),
                            localScale = new Vector3(1, 1, 1),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LaserTurbine",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLaserTurbine"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.002f, -0.002f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0035f, 0.0035f, 0.0035f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FocusConvergence",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFocusedConvergence"),
                            childName = "Base",
                            localPos = new Vector3(-0.0075f, 0.015f, 0.01f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.1f, 0.1f, 0.1f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Incubator",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAncestralIncubator"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.004f, 0),
                            localAngles = new Vector3(315, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FireballsOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFireballsOnHit"),
                            childName = "PickR",
                            localPos = new Vector3(0.00325f, 0.004f, -0.0008f),
                            localAngles = new Vector3(-78, 270, 0),
                            localScale = new Vector3(0.0015f, 0.0008f, 0.0012f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "SiphonOnLowHealth",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySiphonOnLowHealth"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, 0, -0.0015f),
                            localAngles = new Vector3(0, 120, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BleedOnHitAndExplode",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBleedOnHitAndExplode"),
                            childName = "LegR",
                            localPos = new Vector3(-0.001f, 0.002f, 0),
                            localAngles = new Vector3(0, 0, 180),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "MonstersOnShrineUse",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMonstersOnShrineUse"),
                            childName = "LegR",
                            localPos = new Vector3(0, 0, 0.0015f),
                            localAngles = new Vector3(0, 270, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "RandomDamageZone",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRandomDamageZone"),
                            childName = "HandL",
                            localPos = new Vector3(0, 0.001f, 0.001f),
                            localAngles = new Vector3(0, 180, 90),
                            localScale = new Vector3(0.0015f, 0.0005f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Fruit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFruit"),
                            childName = "Chest",
                            localPos = new Vector3(-0.002f, 0.001f, 0),
                            localAngles = new Vector3(-45, -45, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixRed",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                            childName = "Head",
                            localPos = new Vector3(-0.001f, 0.002f, 0),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                            childName = "Head",
                            localPos = new Vector3(0.001f, 0.002f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, -0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixBlue",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0024f, -0.0015f),
                            localAngles = new Vector3(-45, 180, 0),
                            localScale = new Vector3(0.004f, 0.004f, 0.004f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0024f, -0.0005f),
                            localAngles = new Vector3(-60, 180, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixWhite",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteIceCrown"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.003f, 0),
                            localAngles = new Vector3(-90, 180, 0),
                            localScale = new Vector3(0.0003f, 0.0003f, 0.0003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixPoison",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteUrchinCrown"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.002f, 0),
                            localAngles = new Vector3(-90, 180, 0),
                            localScale = new Vector3(0.0006f, 0.0006f, 0.0006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "AffixHaunted",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteStealthCrown"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.003f, 0),
                            localAngles = new Vector3(-90, 180, 0),
                            localScale = new Vector3(0.0006f, 0.0006f, 0.0006f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "CritOnUse",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayNeuralImplant"),
                            childName = "Head",
                            localPos = new Vector3(0, 0.0015f, -0.0025f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "DroneBackup",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRadio"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.002f, 0.0014f, 0),
                            localAngles = new Vector3(340, 90, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Lightning",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.capacitorPrefab,
                            childName = "Chest",
                            localPos = new Vector3(0, 0, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "BurnNearby",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPotion"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, 0, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.0005f, 0.0005f, 0.0005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "CrippleWard",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEffigy"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, -0.00015f, 0),
                            localAngles = new Vector3(0, 270, 0),
                            localScale = new Vector3(0.008f, 0.008f, 0.008f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "QuestVolatileBattery",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBatteryArray"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.004f, -0.003f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "GainArmor",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayElephantFigure"),
                            childName = "KneeR",
                            localPos = new Vector3(0, 0.002f, 0.0016f),
                            localAngles = new Vector3(80, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Recycle",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRecycler"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.003f, -0.0028f),
                            localAngles = new Vector3(0, 90, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "FireBallDash",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEgg"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, 0, 0),
                            localAngles = new Vector3(270, 0, 0),
                            localScale = new Vector3(0.005f, 0.005f, 0.005f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Cleanse",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWaterPack"),
                            childName = "Chest",
                            localPos = new Vector3(0, 0.003f, -0.0024f),
                            localAngles = new Vector3(0, 180, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Tonic",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTonic"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, 0, 0),
                            localAngles = new Vector3(335, 90, 0),
                            localScale = new Vector3(0.003f, 0.003f, 0.003f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Gateway",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayVase"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.0025f, 0.0015f, 0),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Meteor",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMeteor"),
                            childName = "Base",
                            localPos = new Vector3(0, 0.02f, 0.015f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(1.5f, 1.5f, 1.5f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Saw",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySawmerang"),
                            childName = "Base",
                            localPos = new Vector3(0, 0.025f, 0.02f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.25f, 0.25f, 0.25f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Blackhole",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravCube"),
                            childName = "Base",
                            localPos = new Vector3(0, 0.02f, 0.015f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(1, 1, 1),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "Scanner",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayScanner"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.002f, 0, 0.001f),
                            localAngles = new Vector3(270, 90, 0),
                            localScale = new Vector3(0.002f, 0.002f, 0.002f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "DeathProjectile",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDeathProjectile"),
                            childName = "Pelvis",
                            localPos = new Vector3(0.002f, -0.0005f, -0.002f),
                            localAngles = new Vector3(0, 145, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            
            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "LifestealOnHit",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLifestealOnHit"),
                            childName = "Head",
                            localPos = new Vector3(-0.002f, 0.004f, 0),
                            localAngles = new Vector3(25, 90, 0),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
            {
                name = "TeamWarCry",
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTeamWarCry"),
                            childName = "Pelvis",
                            localPos = new Vector3(0, 0, 0.003f),
                            localAngles = new Vector3(0, 0, 0),
                            localScale = new Vector3(0.001f, 0.001f, 0.001f),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });
            #endregion

            //aetherium displays
            #region Aetherium
            if (DiggerPlugin.aetheriumInstalled)
            {
                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_ACCURSED_POTION",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("AccursedPotion"),
                                childName = "Pelvis",
                                localPos = new Vector3(-0.0025f, 0.0015f, 0),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.01f, 0.01f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_ALIEN_MAGNET",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("AlienMagnet"),
                                childName = "Base",
                                localPos = new Vector3(0.01f, 0.01f, 0.02f),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_VOID_HEART",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("VoidHeart"),
                                childName = "Chest",
                                localPos = new Vector3(0, 0.0025f, 0),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.001f, 0.001f, 0.001f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_SHARK_TEETH",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("SharkTeeth"),
                                childName = "LegL",
                                localPos = new Vector3(0, 0.005f, 0),
                                localAngles = new Vector3(0, 0, 300),
                                localScale = new Vector3(0.005f, 0.004f, 0.003f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_BLOOD_SOAKED_SHIELD",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("BloodSoakedShield"),
                                childName = "ElbowL",
                                localPos = new Vector3(0, 0.0015f, 0.001f),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_INSPIRING_DRONE",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("InspiringDrone"),
                                childName = "Base",
                                localPos = new Vector3(0.015f, 0, 0.02f),
                                localAngles = new Vector3(90, 0, 0),
                                localScale = new Vector3(0.002f, 0.002f, 0.002f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_FEATHERED_PLUME",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("FeatheredPlume"),
                                childName = "Head",
                                localPos = new Vector3(0, 0.001f, 0),
                                localAngles = new Vector3(45, 0, 0),
                                localScale = new Vector3(0.01f, 0.01f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_SHIELDING_CORE",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("ShieldingCore"),
                                childName = "Chest",
                                localPos = new Vector3(0, 0.003f, -0.0025f),
                                localAngles = new Vector3(0, 90, 0),
                                localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_UNSTABLE_DESIGN",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("UnstableDesign"),
                                childName = "Chest",
                                localPos = new Vector3(0, 0, -0.001f),
                                localAngles = new Vector3(0, 45, 0),
                                localScale = new Vector3(0.01f, 0.01f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_WEIGHTED_ANKLET",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("WeightedAnklet"),
                                childName = "KneeL",
                                localPos = new Vector3(0, 0.0015f, 0),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.0025f, 0.0025f, 0.0025f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_BLASTER_SWORD",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadAetheriumDisplay("BlasterSword"),
                            childName = "PickL",
                            localPos = new Vector3(-0.008f, 0.001f, 0),
                            localAngles = new Vector3(0, 0, 270),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplays.LoadAetheriumDisplay("BlasterSword"),
                            childName = "PickR",
                            localPos = new Vector3(0.008f, 0.001f, 0),
                            localAngles = new Vector3(0, 0, 90),
                            localScale = new Vector3(0.0015f, 0.0015f, 0.0015f),
                            limbMask = LimbFlags.None
                        }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ITEM_WITCHES_RING",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("WitchesRing"),
                                childName = "ElbowL",
                                localPos = new Vector3(0, 0.002f, 0),
                                localAngles = new Vector3(0, 270, 0),
                                localScale = new Vector3(0.004f, 0.004f, 0.004f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });
                
                list2.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "EQUIPMENT_JAR_OF_RESHAPING",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadAetheriumDisplay("JarOfReshaping"),
                                childName = "Pelvis",
                                localPos = new Vector3(0.0003f, 0, 0),
                                localAngles = new Vector3(0, 270, 0),
                                localScale = new Vector3(0.0005f, 0.0005f, 0.0005f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });
            }
            #endregion

            //sivsitems displays
            /*
            #region SivsItems
            if (DiggerPlugin.sivsItemsInstalled)
            {
                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "BeetlePlush",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("BeetlePlush"),
                                childName = "Chest",
                                localPos = new Vector3(0, 0.005f, -0.0025f),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.01f, 0.01f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "BisonShield",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("BisonShield"),
                                childName = "ElbowR",
                                localPos = new Vector3(0, 0, 0),
                                localAngles = new Vector3(0, 270, 0),
                                localScale = new Vector3(0.007f, 0.007f, 0.007f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "FlameGland",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("FlameGland"),
                                childName = "Chest",
                                localPos = new Vector3(0, 0, 0),
                                localAngles = new Vector3(0, 270, 0),
                                localScale = new Vector3(0.007f, 0.007f, 0.007f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "Geode",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("Geode"),
                                childName = "LegL",
                                localPos = new Vector3(-0.001f, 0.0025f, 0),
                                localAngles = new Vector3(0, 0, 270),
                                localScale = new Vector3(0.003f, 0.003f, 0.002f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "ImpEye",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("ImpEye"),
                                childName = "Head",
                                localPos = new Vector3(0, 0.0015f, -0.001f),
                                localAngles = new Vector3(0, 180, 0),
                                localScale = new Vector3(0.01f, 0.008f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "NullSeed",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("NullSeed"),
                                childName = "Base",
                                localPos = new Vector3(-0.01f, 0, 0.015f),
                                localAngles = new Vector3(90, 0, 0),
                                localScale = new Vector3(2, 2, 2),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "Tarbine",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("Tarbine"),
                                childName = "PickL",
                                localPos = new Vector3(0, 0.002f, 0.002f),
                                localAngles = new Vector3(0, 180, 270),
                                localScale = new Vector3(0.0075f, 0.0075f, 0.0075f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });

                list.Add(new ItemDisplayRuleSet.NamedRuleGroup
                {
                    name = "Tentacle",
                    displayRuleGroup = new DisplayRuleGroup
                    {
                        rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = ItemDisplays.LoadSivDisplay("Tentacle"),
                                childName = "Head",
                                localPos = new Vector3(0, 0.002f, -0.0002f),
                                localAngles = new Vector3(0, 0, 0),
                                localScale = new Vector3(0.01f, 0.01f, 0.01f),
                                limbMask = LimbFlags.None
                            }
                        }
                    }
                });
            }
            #endregion
            */

            //apply displays here

            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            ItemDisplayRuleSet.NamedRuleGroup[] value = list.ToArray();
            ItemDisplayRuleSet.NamedRuleGroup[] value2 = list2.ToArray();
            typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, value);
            typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).SetValue(itemDisplayRuleSet, value2);

            characterModel.itemDisplayRuleSet = itemDisplayRuleSet;
        }

        private static GameObject LoadDisplay(string name)
        {
            if (itemDisplayPrefabs.ContainsKey(name.ToLower()))
            {
                if (itemDisplayPrefabs[name.ToLower()]) return itemDisplayPrefabs[name.ToLower()];
            }
            return null;
        }

        private static void PopulateDisplays()
        {
            /*
            ItemDisplayRuleSet itemDisplayRuleSet = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>().itemDisplayRuleSet;

            capacitorPrefab = PrefabAPI.InstantiateClone(itemDisplayRuleSet.FindEquipmentDisplayRuleGroup("Lightning").rules[0].followerPrefab, "DisplayMinerLightning", true);
            capacitorPrefab.AddComponent<UnityEngine.Networking.NetworkIdentity>();

            var limbMatcher = capacitorPrefab.GetComponent<LimbMatcher>();

            limbMatcher.limbPairs[0].targetChildLimb = "ShoulderL";
            limbMatcher.limbPairs[1].targetChildLimb = "ElbowL";
            limbMatcher.limbPairs[2].targetChildLimb = "HandL";

            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            ItemDisplayRuleSet.NamedRuleGroup[] array = typeof(ItemDisplayRuleSet).GetField("namedItemRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
            ItemDisplayRuleSet.NamedRuleGroup[] array2 = typeof(ItemDisplayRuleSet).GetField("namedEquipmentRuleGroups", bindingAttr).GetValue(itemDisplayRuleSet) as ItemDisplayRuleSet.NamedRuleGroup[];
            ItemDisplayRuleSet.NamedRuleGroup[] array3 = array;

            for (int i = 0; i < array3.Length; i++)
            {
                ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
                for (int j = 0; j < rules.Length; j++)
                {
                    GameObject followerPrefab = rules[j].followerPrefab;
                    if (!(followerPrefab == null))
                    {
                        string name = followerPrefab.name;
                        string key = (name != null) ? name.ToLower() : null;
                        if (!itemDisplayPrefabs.ContainsKey(key))
                        {
                            itemDisplayPrefabs[key] = followerPrefab;
                        }
                    }
                }
            }

            array3 = array2;
            for (int i = 0; i < array3.Length; i++)
            {
                ItemDisplayRule[] rules = array3[i].displayRuleGroup.rules;
                for (int j = 0; j < rules.Length; j++)
                {
                    GameObject followerPrefab2 = rules[j].followerPrefab;
                    if (!(followerPrefab2 == null))
                    {
                        string name2 = followerPrefab2.name;
                        string key2 = (name2 != null) ? name2.ToLower() : null;
                        if (!itemDisplayPrefabs.ContainsKey(key2))
                        {
                            itemDisplayPrefabs[key2] = followerPrefab2;
                        }
                    }
                }
            }
            */
        }


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static GameObject LoadAetheriumDisplay(string name)
        {
            switch (name)
            {
                case "AccursedPotion":
                    return Aetherium.Items.AccursedPotion.ItemBodyModelPrefab;
                case "AlienMagnet":
                    return Aetherium.Items.AlienMagnet.ItemFollowerPrefab;
                case "BlasterSword":
                    return Aetherium.Items.BlasterSword.ItemBodyModelPrefab;
                case "BloodSoakedShield":
                    return Aetherium.Items.BloodSoakedShield.ItemBodyModelPrefab;
                case "FeatheredPlume":
                    return Aetherium.Items.FeatheredPlume.ItemBodyModelPrefab;
                case "InspiringDrone":
                    return Aetherium.Items.InspiringDrone.ItemFollowerPrefab;
                case "JarOfReshaping":
                    return Aetherium.Equipment.JarOfReshaping.ItemBodyModelPrefab;
                case "SharkTeeth":
                    return Aetherium.Items.SharkTeeth.ItemBodyModelPrefab;
                case "ShieldingCore":
                    return Aetherium.Items.ShieldingCore.ItemBodyModelPrefab;
                case "UnstableDesign":
                    return Aetherium.Items.UnstableDesign.ItemBodyModelPrefab;
                case "VoidHeart":
                    return Aetherium.Items.Voidheart.ItemBodyModelPrefab;
                case "WeightedAnklet":
                    return Aetherium.Items.WeightedAnklet.ItemBodyModelPrefab;
                case "WitchesRing":
                    return Aetherium.Items.WitchesRing.ItemBodyModelPrefab;
            }
            return null;
        }

        /*[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static GameObject LoadSivDisplay(string name)
        {
            switch (name)
            {
                case "BeetlePlush":
                    return SivsItemsRoR2.BeetlePlush.displayPrefab;
                case "BisonShield":
                    return SivsItemsRoR2.BisonShield.displayPrefab;
                case "FlameGland":
                    return SivsItemsRoR2.FlameGland.displayPrefab;
                case "Geode":
                    return SivsItemsRoR2.Geode.displayPrefab;
                case "ImpEye":
                    return SivsItemsRoR2.ImpEye.displayPrefab;
                case "NullSeed":
                    return SivsItemsRoR2.NullSeed.displayPrefab;
                case "Tarbine":
                    return SivsItemsRoR2.Tarbine.displayPrefab;
                case "Tentacle":
                    return SivsItemsRoR2.Tentacle.displayPrefab;
            }
            return null;
        }*/
    }
}
