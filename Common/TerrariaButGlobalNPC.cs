using System;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;

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
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            TerrariaButPlayer modplayer = player.GetModPlayer<TerrariaButPlayer>();
            switch (Main.rand.Next(0, 17))
            {
                case 0:
                    modplayer.MeleeDMG += .01f;
                    break;
                case 1:
                    modplayer.RangeDMG += .01f;
                    break;
                case 2:
                    modplayer.MagicDMG += .01f;
                    break;
                case 3:
                    modplayer.SummonDMG += .01f;
                    break;
                case 4:
                    modplayer.Movement += .01f;
                    break;
                case 5:
                    modplayer.JumpBoost += .01f;
                    break;
                case 6:
                    modplayer.HPMax += 1;
                    break;
                case 7:
                    modplayer.HPRegen += .01f;
                    break;
                case 8:
                    modplayer.ManaMax += 1;
                    break;
                case 9:
                    modplayer.ManaRegen += .01f;
                    break;
                case 10:
                    modplayer.DefenseBase += 1;
                    break;
                case 11:
                    modplayer.DamagePure += .01f;
                    break;
                case 12:
                    modplayer.CritStrikeChance += 1;
                    break;
                case 13:
                    modplayer.CritDamage += .01f;
                    break;
                case 14:
                    modplayer.DefenseEffectiveness += .01f;
                    break;
                case 15:
                    modplayer.Thorn += .01f;
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
