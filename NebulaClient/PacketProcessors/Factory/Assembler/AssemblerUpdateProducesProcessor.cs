﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets.Factory.Assembler;
using NebulaModel.Packets;

namespace NebulaClient.PacketProcessors.Factory.Assembler
{
    [RegisterPacketProcessor]
    class AssemblerUpdateProducesProcessor : PacketProcessor<AssemblerUpdateProducesPacket>
    {
        public override void ProcessPacket(AssemblerUpdateProducesPacket packet, NebulaConnection conn)
        {
            AssemblerComponent[] pool = GameMain.galaxy.PlanetById(packet.PlanetId)?.factory?.factorySystem?.assemblerPool;
            if (pool != null && packet.AssemblerIndex != -1 && packet.AssemblerIndex < pool.Length && pool[packet.AssemblerIndex].id != -1)
            {
                pool[packet.AssemblerIndex].produced[packet.ProducesIndex] = packet.ProducesValue;
            }
        }
    }
}