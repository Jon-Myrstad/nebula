﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets;
using NebulaModel.Packets.Trash;
using NebulaWorld;
using NebulaWorld.Trash;

namespace NebulaNetwork.PacketProcessors.Trash
{
    [RegisterPacketProcessor]
    class TrashSystemNewTrashCreatedProcessor : PacketProcessor<TrashSystemNewTrashCreatedPacket>
    {
        public override void ProcessPacket(TrashSystemNewTrashCreatedPacket packet, NebulaConnection conn)
        {
            int myId = SimulatedWorld.GenerateTrashOnPlayer(packet);

            //Check if myID is same as the ID from the host
            if (myId != packet.TrashId)
            {
                TrashManager.SwitchTrashWithIds(myId, packet.TrashId);
            }
        }
    }
}