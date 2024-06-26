﻿using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Digger
{
    public class DrillChargeStart : BaseSkillState
    {
        private GameObject chargePrefab = EntityStates.LemurianBruiserMonster.ChargeMegaFireball.chargeEffectPrefab;
        private GameObject chargeInstance;
        private float chargeDuration = 0.6f;
        private int charge = 0;
        private Vector3 left = new Vector3(0, 0, 1);

        protected bool ShouldKeepChargingAuthority()
        {
            return (base.fixedAge < chargeDuration) && base.IsKeyDownAuthority() && (charge * (.8f + (.2f * base.attackSpeedStat)) <= 38f);
        }

        protected EntityState GetNextStateAuthority()
        {
            return new DrillCharge
            {
                charged = charge
            };
        }

        public override void OnEnter()
        {
            base.OnEnter();
            base.StartAimMode(0.6f, false);

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = this.chargeDuration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matOnFire");
                temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
            }

            Ray aimRay = base.GetAimRay();
            if (base.isAuthority)
            {
                Vector3 direct = aimRay.direction;

                Vector2 move = new Vector2(characterMotor.moveDirection.x, characterMotor.moveDirection.z);
                Vector2 aim = new Vector2(aimRay.direction.x, aimRay.direction.z);
                float forward = Vector2.Dot(move, aim.normalized);
                Vector2 aimO = new Vector2(aimRay.direction.z, -1 * aimRay.direction.x);
                float right = Vector2.Dot(move, aimO.normalized);

                Quaternion major = Quaternion.FromToRotation(left, direct);
                chargeInstance = Object.Instantiate<GameObject>(chargePrefab, aimRay.origin, transform.rotation * major);
                chargeInstance.transform.localScale *= 0.0125f;
            }

            base.PlayAnimation("Gesture, Override", "DrillChargeStart");
            Util.PlaySound(DiggerPlugin.Sounds.DrillChargeStart, base.gameObject);
        }

        public override void OnExit()
        {
            if (chargeInstance)
            {
                EntityState.Destroy(chargeInstance);
            }

            base.PlayAnimation("Gesture, Override", "BufferEmpty");

            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!ShouldKeepChargingAuthority())
            {
                outer.SetNextState(GetNextStateAuthority());
                return;
            }
            Ray aimRay = base.GetAimRay();
            Vector3 direct = aimRay.direction;
            Quaternion major = Quaternion.FromToRotation(left, direct);
            chargeInstance.transform.rotation = transform.rotation * major;
            chargeInstance.transform.position = aimRay.origin;

            charge++;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }

    public class DrillCharge : BaseSkillState
    {
        public static float damageCoefficient = DiggerPlugin.DiggerPlugin.drillChargeDamage.Value;
        public float baseDuration = 0.4f;
        public static float styleCoefficient = 0.4f;

        private float duration;
        public GameObject explodePrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFX");
        private EffectData effectData;

        private int frameCounter = 0;
        public int charged;
        private int type;
        private BlastAttack blastAttack;
        //private StyleSystem.StyleComponent styleComponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
            Ray aimRay = base.GetAimRay();
            //this.styleComponent = base.GetComponent<StyleSystem.StyleComponent>();
            base.characterMotor.disableAirControlUntilCollision = false;

            base.GetModelChildLocator().FindChild("DrillChargeEffect").GetComponent<ParticleSystem>().Play();

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = this.duration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 15f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matOnFire");
                temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
            }

            //bool flag = false;
            //float angle = Vector3.Angle(new Vector3(0, -1, 0), aimRay.direction);
            //if (angle > 120) flag = true;

            this.type = 0;
            if (charged > 8)
            {
                this.type = 1;
                base.GetModelAnimator().SetFloat("DrillCharge.offset", Util.Remap(base.characterMotor.velocity.y, -70, 70, -1, 1));
                base.PlayAnimation("FullBody, Override", "DrillCharge");

                /*if (flag)
                {
                    this.type = 1;
                    base.PlayAnimation("FullBody, Override", "DrillChargeVertical", "Spin.playbackRate", 5f);
                }
                else
                {
                    this.type = 2;
                    base.PlayAnimation("FullBody, Override", "DrillChargeHorizontal", "Spin.playbackRate", 5f);
                }*/
            }
            else base.PlayAnimation("FullBody, Override", "DrillChargeShort");


            if (NetworkServer.active) base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);

            Util.PlaySound(DiggerPlugin.Sounds.DrillCharge, base.gameObject);

            if (base.isAuthority)
            {
                blastAttack = new BlastAttack
                {
                    radius = 10f,
                    procCoefficient = 1,
                    position = aimRay.origin,
                    attacker = base.gameObject,
                    crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master),
                    baseDamage = base.characterBody.damage * DrillCharge.damageCoefficient,
                    falloffModel = BlastAttack.FalloffModel.None,
                    baseForce = 3f,
                    damageType = DamageType.Generic,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                };
                blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);

                effectData = new EffectData();
                effectData.scale = 1.5f;
                effectData.color = new Color32(234, 234, 127, 100);

                base.characterMotor.velocity += 75 * aimRay.direction;
            }
        }

        public override void OnExit()
        {
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.5f);
            }

            if (!base.characterMotor.disableAirControlUntilCollision) base.characterMotor.velocity *= 0.4f;

            if (this.type == 0) base.PlayAnimation("FullBody, Override", "DrillChargeShortEnd");
            else
            {
                if (!base.isGrounded) base.PlayAnimation("FullBody, Override", "Flip", "Flip.playbackRate", 0.3f);
                else base.PlayAnimation("FullBody, Override", "BufferEmpty");
            }
            //if (this.type == 1) base.PlayAnimation("FullBody, Override", "DrillChargeVertiEnd");
            //if (this.type == 2) base.PlayAnimation("FullBody, Override", "DrillChargeHoriEnd");

            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                this.blastAttack.position = aimRay.origin;
                this.effectData.origin = aimRay.origin;

                if (base.fixedAge >= this.duration && base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }

                float boost = charged * (.8f + (.2f * base.attackSpeedStat));
                int roundDown = Mathf.FloorToInt(boost);
                int scale = Mathf.Max(2, 40 - roundDown);

                if (this.frameCounter % scale == 0)
                {
                    BlastAttack.Result result = blastAttack.Fire();
                    int hitCount = result.hitCount;

                    //if (this.styleComponent) this.styleComponent.AddStyle(hitCount * DrillCharge.styleCoefficient);

                    EffectManager.SpawnEffect(this.explodePrefab, effectData, false);
                }

                this.frameCounter++;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
