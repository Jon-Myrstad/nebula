﻿using HarmonyLib;
using NebulaAPI;
using NebulaModel.Packets.Universe;
using NebulaWorld;
using NebulaWorld.Factory;

namespace NebulaPatcher.Patches.Dynamic
{
    [HarmonyPatch(typeof(UIPlanetDetail))]
    class UIPlanetDetail_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(UIPlanetDetail.OnNameInputEndEdit))]
        public static void OnNameInputEndEdit_Postfix(UIPlanetDetail __instance)
        {
            if (Multiplayer.IsActive && !Multiplayer.Session.Factories.IsIncomingRequest.Value)
            {
                if (__instance.planet != null && !string.IsNullOrEmpty(__instance.planet.overrideName))
                {
                    // Send packet with new planet name
                    Multiplayer.Session.Network.SendPacket(new NameInputPacket(__instance.planet.overrideName, NebulaModAPI.STAR_NONE, __instance.planet.id, ((LocalPlayer)Multiplayer.Session.LocalPlayer).Id));
                }
            }
        }
    }
}
