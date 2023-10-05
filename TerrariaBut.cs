using System.IO;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrariaBut.Common;

namespace TerrariaBut
{
	partial class TerrariaBut : Mod
	{
        internal enum MessageType : byte
        {
            CardEffect,
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            byte playernumber = reader.ReadByte();
            switch (msgType)
            {
                case MessageType.CardEffect:
                    TerrariaButPlayer cardplayer = Main.player[playernumber].GetModPlayer<TerrariaButPlayer>();
                    cardplayer.ReceivePlayerSync(reader);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        cardplayer.SyncPlayer(-1, whoAmI, false);
                    }
                    break;
            }
        }
    }

}