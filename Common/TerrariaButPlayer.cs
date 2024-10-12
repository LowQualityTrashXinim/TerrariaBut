using System;
using Terraria;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using TerrariaBut.Common.Utils;

namespace TerrariaBut.Common
{
    public enum PlayerStats
    {
        MaxHP,
    }
    internal class TerrariaButPlayer : ModPlayer
    {
        public const int maxStatCanBeAchieved = 99999;
        public int HPMax = 0;
        public override void UpdateEquips()
        {
            if(Main.rand.NextBool(200) && HPMax < 0)
            {
                HPMax++;
            }
        }
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;
            mana = StatModifier.Default;

            health.Flat = HPMax;
            if(health.Flat <= -Player.statLifeMax2)
            {
                health.Flat += 1;
            }
        }
        public override void Initialize()
        {
            HPMax = 0;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)TerrariaBut.MessageType.CardEffect);
            packet.Write((byte)Player.whoAmI);
            packet.Write(HPMax);
            packet.Send(toWho, fromWho);
        }
        public override void SaveData(TagCompound tag)
        {
            tag["HPMax"] = HPMax;
        }
        public override void LoadData(TagCompound tag)
        {
            HPMax = (int)tag["HPMax"];
        }
        public void ReceivePlayerSync(BinaryReader reader)
        {
            HPMax = reader.ReadInt32();
        }
        public override void CopyClientState(ModPlayer targetCopy)
        {
            TerrariaButPlayer clone = (TerrariaButPlayer)targetCopy;
            clone.HPMax = HPMax;
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            TerrariaButPlayer clone = (TerrariaButPlayer)clientPlayer;
            if (HPMax != clone.HPMax)
            {
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            OnHitEffect();
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            OnHitEffect();
        }
        private void OnHitEffect()
        {
            HPMax -= Main.rand.Next(1, 11);
            if (Main.rand.NextBool(100))
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Player.TeleportationPotion();
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.RequestTeleportationByServer);
            }
            if (Main.rand.NextBool(20))
                Player.DropSelectedItem();
            else if (Main.rand.NextBool(150))
            {
                for (int i = 0; i < Player.inventory.Length; i++)
                {
                    Player.DropItem(Player.GetSource_FromThis(), Player.Center, ref Player.inventory[i]);
                }
            }
        }
    }
}