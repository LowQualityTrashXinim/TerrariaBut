using System;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalNPC : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            npc.lifeMax += Main.rand.Next(-npc.lifeMax + 1, npc.lifeMax);
            npc.life = npc.lifeMax;
            if (npc.damage > 0)
                npc.damage += Main.rand.Next(-npc.damage + 1, npc.damage);
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByItem(npc, player, item, hit, damageDone);
            SpawnDupeNPCFunni(npc);
        }
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            base.OnHitNPC(npc, target, hit);
            SpawnDupeNPCFunni(npc);
        }
        private void SpawnDupeNPCFunni(NPC npc)
        {
            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, npc.type);
            }
        }
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            TerrariaButPlayer modplayer = player.GetModPlayer<TerrariaButPlayer>();
            float Randomize = Main.rand.NextFloat(.01f, 0.1f);
            int RandomizeIntStats = Main.rand.Next(1, 11);
            switch (Main.rand.Next(0, 17))
            {
                case 0:
                    modplayer.MeleeDMG += Randomize;
                    break;
                case 1:
                    modplayer.RangeDMG += Randomize;
                    break;
                case 2:
                    modplayer.MagicDMG += Randomize;
                    break;
                case 3:
                    modplayer.SummonDMG += Randomize;
                    break;
                case 4:
                    modplayer.Movement += Randomize;
                    break;
                case 5:
                    modplayer.JumpBoost += Randomize;
                    break;
                case 6:
                    modplayer.HPMax += RandomizeIntStats;
                    break;
                case 7:
                    modplayer.HPRegen += Randomize;
                    break;
                case 8:
                    modplayer.ManaMax += RandomizeIntStats;
                    break;
                case 9:
                    modplayer.ManaRegen += Randomize;
                    break;
                case 10:
                    modplayer.DefenseBase += RandomizeIntStats / 2;
                    break;
                case 11:
                    modplayer.DamagePure += Randomize;
                    break;
                case 12:
                    modplayer.CritStrikeChance += RandomizeIntStats / 2;
                    break;
                case 13:
                    modplayer.CritDamage += Randomize;
                    break;
                case 14:
                    modplayer.DefenseEffectiveness += Randomize;
                    break;
                case 15:
                    modplayer.Thorn += Randomize;
                    break;
                case 16:
                    modplayer.MinionSlot += 1;
                    break;
                case 17:
                    modplayer.SentrySlot += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
