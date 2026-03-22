using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TerrariaBut.Common
{
    internal class EvenMoreAnnoyingMode_NPC : GlobalNPC
    {
        public bool Check() => ModContent.GetInstance<TerrariaButConfig>().EvenMoreAnnoying;
        public override void SetDefaults(NPC entity)
        {
            if (Check())
            {
                entity.lavaImmune = true;
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if(Check())
            {
                if(Main.rand.NextBool(50))
                {
                    npc.lifeMax *= 10;
                    npc.life = npc.lifeMax;
                }
                if(npc.boss)
                {
                    npc.lifeMax *= 3;
                    npc.life = npc.lifeMax;
                }
            }
        }
        public override bool InstancePerEntity => true;
        int NPC_SpawnCounter = 0;
        public override void PostAI(NPC npc)
        {
            if (!Check() || !npc.boss || ++NPC_SpawnCounter <= 600)
            {
                return;
            }
            NPC_SpawnCounter = 0;
            int amount = Main.rand.Next(1, 6);
            int type = 0;
            switch (npc.type)
            {
                case NPCID.KingSlime:
                    type = NPCID.SlimeSpiked;
                    break;
                case NPCID.EyeofCthulhu:
                    type = NPCID.ServantofCthulhu;
                    break;
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsTail:
                    type = NPCID.EaterofSouls;
                    break;
                case NPCID.BrainofCthulhu:
                    type = NPCID.Crimera;
                    break;
                case NPCID.SkeletronHead:
                    type = NPCID.DarkCaster;
                    break;
                case NPCID.SkeletronHand:
                    type = NPCID.ArmoredSkeleton;
                    break;
                case NPCID.Deerclops:
                    type = NPCID.SnowFlinx;
                    break;
                case NPCID.QueenBee:
                    type = NPCID.Hornet;
                    break;
                case NPCID.WallofFlesh:
                    type = NPCID.TheHungryII;
                    break;
                case NPCID.WallofFleshEye:
                    type = NPCID.TheHungryII;
                    break;
                case NPCID.QueenSlimeBoss:
                    type = NPCID.QueenSlimeMinionPurple;
                    break;
                case NPCID.TheDestroyer:
                case NPCID.TheDestroyerTail:
                case NPCID.SkeletronPrime:
                    type = NPCID.Probe;
                    break;
                case NPCID.Retinazer:
                case NPCID.Spazmatism:
                    type = NPCID.WanderingEye;
                    break;
                case NPCID.Plantera:
                    type = NPCID.GiantMossHornet;
                    break;
                case NPCID.Golem:
                case NPCID.GolemHeadFree:
                case NPCID.GolemHead:
                    type = NPCID.FlyingSnake;
                    break;
                case NPCID.MoonLordCore:
                    type = NPCID.AncientCultistSquidhead;
                    break;
            }
            if (type == 0)
            {
                NPC_SpawnCounter = -999999;
                return;
            }
            for (int i = 0; i < amount; i++)
            {
                NPC npc_sub = NPC.NewNPCDirect(npc.GetSource_FromAI(), npc.Center, type);
                npc_sub.velocity = Main.rand.NextVector2CircularEdge(10, 10);
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (!Check())
            {
                return;
            }
                if (Main.rand.NextFloat() <= .15f)
            {
                modifiers.SourceDamage *= 4;
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (!Check())
            {
                return;
            }
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.BrokenArmor, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Cursed, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Bleeding, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Burning, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Weak, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Ichor, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Poisoned, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Slow, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.ManaSickness, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.PotionSickness, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Obstructed, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Blackout, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Confused, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Darkness, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Electrified, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Stoned, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.WitheredArmor, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.WitheredWeapon, 60 * Main.rand.Next(5, 16));
            if (Main.rand.NextBool(30))
                target.AddBuff(BuffID.Suffocation, 60 * Main.rand.Next(5, 16));
        }
    }

    public class EvenMoreAnnoyingMode_Player : ModPlayer
    {
        public bool Check() => ModContent.GetInstance<TerrariaButConfig>().EvenMoreAnnoying;
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (Check())
            {
                healValue /= 2;
            }
        }
    }
}
