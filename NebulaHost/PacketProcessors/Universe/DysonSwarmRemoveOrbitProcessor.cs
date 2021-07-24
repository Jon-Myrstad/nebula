﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets;
using NebulaModel.Packets.Universe;
using NebulaWorld.Universe;

namespace NebulaNetwork.PacketProcessors.Universe
{
    [RegisterPacketProcessor]
    class DysonSwarmRemoveOrbitProcessor : PacketProcessor<DysonSwarmRemoveOrbitPacket>
    {
        private PlayerManager playerManager;

        public DysonSwarmRemoveOrbitProcessor()
        {
            playerManager = MultiplayerHostSession.Instance.PlayerManager;
        }

        public override void ProcessPacket(DysonSwarmRemoveOrbitPacket packet, NebulaConnection conn)
        {
            Player player = playerManager.GetPlayer(conn);
            if (player != null)
            {
                playerManager.SendPacketToOtherPlayers(packet, player);
                using (DysonSphere_Manager.IncomingDysonSwarmPacket.On())
                {
                    GameMain.data.dysonSpheres[packet.StarIndex]?.swarm?.RemoveOrbit(packet.OrbitId);
                }
            }
        }
    }
}
