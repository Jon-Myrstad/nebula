﻿using HarmonyLib;
using NebulaWorld;
using NebulaWorld.Statistics;

namespace NebulaPatcher.Patches.Dynamic
{
    [HarmonyPatch(typeof(FactoryProductionStat))]
    class FactoryProductionStat_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(FactoryProductionStat.GameTick))]
        public static bool GameTick_Prefix(FactoryProductionStat __instance)
        {
            //Do not run in single player for host
            if (!Multiplayer.IsActive || ((LocalPlayer)Multiplayer.Session.LocalPlayer).IsHost)
            {
                return true;
            }

            //Multiplayer clients should not include their own calculated statistics
            if (!Multiplayer.Session.Statistics.IsIncomingRequest)
            {
                __instance.ClearRegisters();
                return false;
            }

            return true;
        }
    }
}
