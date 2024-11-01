using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using MyTrueGear;

namespace Subnautica_TrueGear
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private static TrueGearMod _TrueGear = null;

        private static int exosuitHealth = 0;
        private static int seamothHealth = 0;
        private static bool isUnderwaterForSwimming = false;
        private static bool isDeath = true;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            Harmony.CreateAndPatchAll(typeof(Plugin));
            _TrueGear = new TrueGearMod();

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private static void PlayerDeath()
        {
            isDeath = true;
            _TrueGear.StopHeartBeat();
            _TrueGear.StopLaserCutting();
            _TrueGear.StopLowFood();
            _TrueGear.StopLowOxygen();
            _TrueGear.StopLowWater();
            _TrueGear.StopPrecursorTeleporter();
            _TrueGear.StopWeld();
            _TrueGear.StopExtinguisher();
            _TrueGear.StopStasisRifleCharging();
            _TrueGear.StopAirBladder();
            _TrueGear.StopScannerTool();
            _TrueGear.StopBuilderTool();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "OnKill")]
        private static void Player_OnKill_Postfix()
        {
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("PlayerDeath");
            _TrueGear.Play("PlayerDeath");
            PlayerDeath();
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(Player), "OnDestroy")]
        //private static void Player_OnDestroy_Postfix()
        //{
        //    Logger.LogInfo("-----------------------------------------------");
        //    Logger.LogInfo("PlayerDestroy");
        //    isDeath = true;
        //}

        //[HarmonyPostfix, HarmonyPatch(typeof(Player), "OnDisable")]
        //private static void Player_OnDisable_Postfix()
        //{
        //    Logger.LogInfo("-----------------------------------------------");
        //    Logger.LogInfo("PlayerDisable");
        //    isDeath = true;
        //}



        [HarmonyPrefix, HarmonyPatch(typeof(Player), "UpdateIsUnderwater")]
        private static void Player_UpdateIsUnderwater_Prefix(Player __instance)
        {
            isUnderwaterForSwimming = __instance.isUnderwaterForSwimming.value;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "UpdateIsUnderwater")]
        private static void Player_UpdateIsUnderwater_Postfix(Player __instance)
        {
            if (isUnderwaterForSwimming == __instance.isUnderwaterForSwimming.value)
            {
                isUnderwaterForSwimming = __instance.isUnderwaterForSwimming.value;
                return;
            }
            if (isDeath)
            {
                return;
            }
            if (isUnderwaterForSwimming)
            {
                Logger.LogInfo("-----------------------------------------------");
                Logger.LogInfo("ExitWater");
                _TrueGear.Play("ExitWater");
            }
            else
            {
                Logger.LogInfo("-----------------------------------------------");
                Logger.LogInfo("EnterWater");
                _TrueGear.Play("EnterWater");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LaserCutter), "StartLaserCuttingFX")]
        private static void LaserCutter_StartLaserCuttingFX_Postfix()
        {
            if (isDeath)
            {
                return;
            }
            _TrueGear.StartLaserCutting();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LaserCutter), "StopLaserCuttingFX")]
        private static void LaserCutter_StopLaserCuttingFX_Postfix()
        {
            _TrueGear.StopLaserCutting();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AirBladder), "OnRightHandDown")]
        private static void AirBladder_OnRightHandDown_Postfix(bool __result)
        {
            if (!__result)
            {
                return;
            }
            _TrueGear.StartAirBladder();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AirBladder), "OnHolster")]
        private static void AirBladder_OnRemoved_Postfix()
        {
            _TrueGear.StopAirBladder();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AirBladder), "RemoveOxygen")]
        private static void AirBladder_RemoveOxygen_Postfix(AirBladder __instance)
        {
            if (__instance.oxygen > 0)
            {
                return;
            }
            _TrueGear.StopAirBladder();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AirBladder), "UpdateInflateState")]
        private static void AirBladder_UpdateInflateState_Postfix(AirBladder __instance)
        {
            if (Time.time - __instance.inflateStartTime < 0f)
            {
                return;
            }
            if (__instance.reclaimOxygen)
            {

                _TrueGear.StopAirBladder();
                return;
            }
            if (!__instance.deflating)
            {
                if (__instance.oxygen < __instance.oxygenCapacity && !__instance.isUnderwater)
                {

                    _TrueGear.StopAirBladder();
                }
                return;
            }
        }



        [HarmonyPostfix, HarmonyPatch(typeof(Constructor), "OnRightHandDown")]
        private static void Constructor_OnRightHandDown_Postfix(bool __result)
        {
            if (!__result)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("UseConstructor");
            _TrueGear.Play("UseConstructor");
            Logger.LogInfo(__result);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CyclopsExternalDamageManager), "OnTakeDamage")]
        private static void CyclopsExternalDamageManager_OnTakeDamage_Postfix(CyclopsExternalDamageManager __instance, DamageInfo damageInfo)
        {
            if (damageInfo == null)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("CyclopsExternalTakeDamage");
            _TrueGear.Play("CyclopsExternalTakeDamage");
            Logger.LogInfo(damageInfo);
            Logger.LogInfo(damageInfo.damage);
            Logger.LogInfo(damageInfo.type);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CyclopsEngineChangeState), "OnClick")]
        private static void CyclopsEngineChangeState_OnClick_Postfix(CyclopsEngineChangeState __instance)
        {
            if (isDeath)
            {
                return;
            }
            if (__instance.motorMode.engineOn)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("CyclopsEngineChangeStateOnClick");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(uGUI_ExosuitHUD), "Update")]
        private static void uGUI_ExosuitHUD_OnClick_Prefix(uGUI_ExosuitHUD __instance)
        {
            exosuitHealth = __instance.lastHealth;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_ExosuitHUD), "Update")]
        private static void uGUI_ExosuitHUD_OnClick_Postfix(uGUI_ExosuitHUD __instance)
        {
            if (exosuitHealth == __instance.lastHealth)
            {
                return;
            }
            if (isDeath)
            {
                return;
            }
            if (exosuitHealth < 0)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("ExosuitDamage");
            _TrueGear.Play("ExosuitDamage");
            Logger.LogInfo(exosuitHealth);
            Logger.LogInfo(__instance.lastHealth);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(uGUI_SeamothHUD), "Update")]
        private static void uGUI_SeamothHUD_OnClick_Prefix(uGUI_SeamothHUD __instance)
        {
            seamothHealth = __instance.lastHealth;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_SeamothHUD), "Update")]
        private static void uGUI_SeamothHUD_OnClick_Postfix(uGUI_SeamothHUD __instance)
        {
            if (seamothHealth == __instance.lastHealth)
            {
                return;
            }
            if (isDeath)
            {
                return;
            }
            if (seamothHealth < 0)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("SeamothDamage");
            _TrueGear.Play("SeamothDamage");
            Logger.LogInfo(seamothHealth);
            Logger.LogInfo(__instance.lastHealth);
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "ApplyJumpForce")]
        //private static void Exosuit_ApplyJumpForce_Postfix(Exosuit __instance)
        //{
        //    if (__instance.onGround)
        //    {
        //        Logger.LogInfo("-----------------------------------------------");
        //        Logger.LogInfo("ExosuitJump");
        //        _TrueGear.Play("ExosuitJump");
        //    }
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "OnLand")]
        private static void Exosuit_OnLand_Postfix(Exosuit __instance)
        {
            if (!__instance.playerFullyEntered)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("ExosuitOnLand");
            _TrueGear.Play("ExosuitOnLand");
        }




        [HarmonyPostfix, HarmonyPatch(typeof(Knife), "IsValidTarget")]
        private static void Knife_IsValidTarget_Postfix(Knife __instance, bool __result)
        {
            if (isDeath)
            {
                return;
            }
            if (__result)
            {
                Logger.LogInfo("-----------------------------------------------");
                Logger.LogInfo("KnifeIsValidTarget");
                _TrueGear.Play("RightHandMeleeHit");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_FoodBar), "OnPulse")]
        private static void uGUI_FoodBar_OnPulse_Postfix(uGUI_FoodBar __instance)
        {
            if (isDeath)
            {
                return;
            }
            if (__instance.pulseDelay < 0.85f)
            {
                _TrueGear.StartLowFood();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_FoodBar), "OnEat")]
        private static void uGUI_FoodBar_OnEat_Postfix(uGUI_FoodBar __instance)
        {
            _TrueGear.StopLowFood();
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("Eating");
            _TrueGear.Play("Eating");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_HealthBar), "OnPulse")]
        private static void uGUI_HealthBar_OnPulse_Postfix(uGUI_HealthBar __instance)
        {
            if (isDeath)
            {
                return;
            }
            if (__instance.pulseDelay < 1.85f)
            {
                _TrueGear.StartHeartBeat();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_HealthBar), "OnHealDamage")]
        private static void uGUI_HealthBar_OnHealDamage_Postfix(uGUI_HealthBar __instance)
        {
            Logger.LogInfo("-----------------------------------------------");
            _TrueGear.StopHeartBeat();
            Logger.LogInfo("Healing");
            _TrueGear.Play("Healing");
        }


        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_OxygenBar), "OnPulse")]
        private static void uGUI_OxygenBar_OnPulse_Postfix(uGUI_OxygenBar __instance)
        {
            if (isDeath)
            {
                return;
            }
            if (__instance.pulseDelay < 2f && isUnderwaterForSwimming)
            {
                _TrueGear.StartLowOxygen();
            }
            else if (__instance.pulseDelay >= 2f || !isUnderwaterForSwimming)
            {
                _TrueGear.StopLowOxygen();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_WaterBar), "OnPulse")]
        private static void uGUI_WaterBar_OnPulse_Postfix(uGUI_WaterBar __instance)
        {
            if (isDeath)
            {
                return;
            }
            if (__instance.pulseDelay < 0.85f)
            {
                _TrueGear.StartLowWater();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(uGUI_WaterBar), "OnDrink")]
        private static void uGUI_WaterBar_OnHealDamage_Postfix(uGUI_WaterBar __instance)
        {
            Logger.LogInfo("-----------------------------------------------");
            _TrueGear.StopLowWater();
            Logger.LogInfo("Drinking");
            _TrueGear.Play("Drinking");
        }


        [HarmonyPostfix, HarmonyPatch(typeof(Inventory), "OnAddItem")]
        private static void Inventory_OnAddItem_Postfix(Inventory __instance)
        {
            if (isDeath)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("PickupItem");
            _TrueGear.Play("ChestSlotInputItem");
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(PropulsionCannon), "GrabObject")]
        //private static void PropulsionCannon_GrabObject_Postfix(PropulsionCannon __instance)
        //{
        //    Logger.LogInfo("-----------------------------------------------");
        //    Logger.LogInfo("PropulsionCannonGrabObject");
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(PropulsionCannon), "OnShoot")]
        private static void PropulsionCannon_OnShoot_Postfix(PropulsionCannon __instance, bool __result)
        {
            if (__instance.grabbedObject != null)
            {
                Logger.LogInfo("-----------------------------------------------");
                Logger.LogInfo("PropulsionCannonGrabItem");
                _TrueGear.Play("LeftHandPickupItem");
                _TrueGear.Play("RightHandPickupItem");
                return;
            }
            if (!__instance.canGrab)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("PropulsionCannonShoot");
            _TrueGear.Play("PropulsionCannonShoot");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ScannerTool), "OnRightHandDown")]
        private static void ScannerTool_OnRightHandDown_Postfix(ScannerTool __instance)
        {
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("ScannerToolOnRightHandDown");
            _TrueGear.Play("RightHandPickupItem");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ScannerTool), "PlayScanFX")]
        private static void ScannerTool_PlayScanFX_Postfix(ScannerTool __instance)
        {
            _TrueGear.StartScannerTool();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ScannerTool), "StopScanFX")]
        private static void ScannerTool_StopScanFX_Postfix(ScannerTool __instance)
        {
            _TrueGear.StopScannerTool();
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(Vehicle), "PlaySplashSound")]
        //private static void Vehicle_PlaySplashSound_Postfix(Vehicle __instance)
        //{
        //    if (isDeath)
        //    {
        //        return;
        //    }
        //    Logger.LogInfo("-----------------------------------------------");
        //    Logger.LogInfo("VehiclePlaySplashSound");
        //}

        private static bool canStasisRifleFire = false;

        [HarmonyPostfix, HarmonyPatch(typeof(StasisRifle), "Fire")]
        private static void StasisRifle_Fire_Postfix(StasisRifle __instance)
        {
            if (!canStasisRifleFire)
            {
                return;
            }
            canStasisRifleFire = false;
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("StasisRifleShoot");
            _TrueGear.StopStasisRifleCharging();
            _TrueGear.Play("StasisRifleShoot");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(StasisRifle), "OnRightHandDown")]
        private static void StasisRifle_OnRightHandDown_Postfix(StasisRifle __instance)
        {
            if (!__instance.isCharging)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("StasisRifleOnRightHandDown");
            _TrueGear.StartStasisRifleCharging();
            canStasisRifleFire = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "OnTakeDamage")]
        private static void Player_OnTakeDamage_Postfix(Player __instance)
        {
            if (isDeath)
            {
                return;
            }
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("PlayerOnTakeDamage");
            _TrueGear.Play("PoisonDamage");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PrecursorTeleporter), "OnPlayerCinematicModeEnd")]
        private static void PrecursorTeleporter_OnPlayerCinematicModeEnd_Postfix(PrecursorTeleporter __instance)
        {
            _TrueGear.StartPrecursorTeleporter();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "CompleteTeleportation")]
        private static void Player_CompleteTeleportation_Postfix(Player __instance)
        {
            _TrueGear.StopPrecursorTeleporter();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Welder), "Weld")]
        private static void Welder_Weld_Postfix(Welder __instance)
        {
            _TrueGear.StartWeld();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Welder), "StopWeldingFX")]
        private static void Welder_StopWeldingFX_Postfix(Welder __instance)
        {
            _TrueGear.StopWeld();
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(Player), "LateUpdate")]
        //private static void Player_LateUpdate_Postfix(Player __instance)
        //{
        //    Logger.LogInfo("-----------------------------------------------");
        //    Logger.LogInfo("StopWeld");
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(FireExtinguisher), "UseExtinguisher")]
        private static void FireExtinguisher_UseExtinguisher_Postfix(FireExtinguisher __instance)
        {
            _TrueGear.StartExtinguisher();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(FireExtinguisher), "Update")]
        private static void FireExtinguisher_Update_Postfix(FireExtinguisher __instance)
        {
            if (!__instance.usedThisFrame || __instance.fuel <= 0f)
            {
                _TrueGear.StopExtinguisher();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "PlayBash")]
        private static void Player_PlayBash_Postfix(Player __instance)
        {
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("PlayBash");
            _TrueGear.Play("LeftHandMeleeHit");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Player), "UnfreezeStats")]
        private static void Player_UnfreezeStats_Postfix(Player __instance)
        {
            isDeath = false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(BuilderTool), "OnRightHandDown")]
        private static void BuilderTool_OnRightHandDown_Postfix(bool __result)
        {
            Logger.LogInfo("-----------------------------------------------");
            Logger.LogInfo("BuilderToolOnRightHandDown");
            _TrueGear.Play("RightHandPickupItem");
            Logger.LogInfo(__result);
        }
        private static List<TechType> techTypes = new List<TechType>() { TechType.ExosuitClawArmModule, TechType.ExosuitGrapplingArmModule, TechType.ExosuitTorpedoArmModule, TechType.ExosuitPropulsionArmModule };
        [HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "SlotLeftDown")]
        private static void Exosuit_SlotLeftDown_Postfix(Exosuit __instance)
        {
            if (__instance.mainAnimator.GetBool("use_tool_left"))
            {
                if (techTypes.Contains(__instance.currentLeftArmType))
                {
                    Logger.LogInfo("-----------------------------------------------");
                    Logger.LogInfo("LeftHandMeleeHit");
                    _TrueGear.Play("LeftHandMeleeHit");
                }
                if (__instance.currentLeftArmType == TechType.ExosuitDrillArmModule)
                {
                    _TrueGear.StartLeftHandDrill();
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "SlotLeftUp")]
        private static void Exosuit_SlotLeftUp_Postfix(Exosuit __instance)
        {
            if (__instance.currentLeftArmType == TechType.ExosuitDrillArmModule)
            {
                _TrueGear.StopLeftHandDrill();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "SlotRightDown")]
        private static void Exosuit_SlotRightDown_Postfix(Exosuit __instance)
        {
            if (__instance.mainAnimator.GetBool("use_tool_right"))
            {
                if (techTypes.Contains(__instance.currentRightArmType))
                {
                    Logger.LogInfo("-----------------------------------------------");
                    Logger.LogInfo("RightHandMeleeHit");
                    _TrueGear.Play("RightHandMeleeHit");
                }
                if (__instance.currentRightArmType == TechType.ExosuitDrillArmModule)
                {
                    _TrueGear.StartRightHandDrill();
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Exosuit), "SlotRightUp")]
        private static void Exosuit_SlotRightUp_Postfix(Exosuit __instance)
        {
            if (__instance.currentRightArmType == TechType.ExosuitDrillArmModule)
            {
                _TrueGear.StopRightHandDrill();
            }
        }



        [HarmonyPostfix, HarmonyPatch(typeof(BuilderTool), "SetBeamActive")]
        private static void BuilderTool_SetBeamActive_Postfix(BuilderTool __instance, bool state)
        {
            if (state)
            {
                _TrueGear.StartBuilderTool();
                return;
            }
            _TrueGear.StopBuilderTool();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(EscapePod), "Awake")]
        private static void EscapePod_Awake_Postfix()
        {
            isDeath = false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(IngameMenu), "OnDeselect")]
        private static void IngameMenu_Close_Postfix()
        {
            _TrueGear.NoPause();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(IngameMenu), "Open")]
        private static void IngameMenu_Open_Postfix()
        {
            _TrueGear.Pause();
        }


        [HarmonyPostfix, HarmonyPatch(typeof(IngameMenu), "QuitGame")]
        private static void IngameMenu_QuitGame_Postfix(IngameMenu __instance, bool quitToDesktop)
        {
            PlayerDeath();
            _TrueGear.NoPause();
        }
    }
}
