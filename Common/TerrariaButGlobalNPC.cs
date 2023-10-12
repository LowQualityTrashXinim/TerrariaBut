using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        float resMelee, resRange, resMagic, resSummon = 1;
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            resMelee = Main.rand.NextFloat(0, 1.25f);
            resRange = Main.rand.NextFloat(0, 1.25f);
            resMagic = Main.rand.NextFloat(0, 1.25f);
            resSummon = Main.rand.NextFloat(0, 1.25f);
            npc.lifeMax += Main.rand.Next(-npc.lifeMax + 1, npc.lifeMax);
            npc.life = npc.lifeMax;
            npc.lifeRegen += Main.rand.Next((int)(npc.lifeMax * .25f));
            if (npc.damage > 0)
                npc.damage += Main.rand.Next(-npc.damage + 1, npc.damage);
            npc.scale += Main.rand.NextFloat(-.75f, 1);
            npc.defense += npc.defense == 0 ? Main.rand.Next(1, 20) : Main.rand.Next(-npc.defense, npc.defense);
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByItem(npc, player, item, hit, damageDone);
            SpawnDupeNPCFunni(npc);
            ResDamage(ref hit);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
            SpawnDupeNPCFunni(npc);
            ResDamage(ref hit);
        }
        private void ResDamage(ref NPC.HitInfo hit)
        {
            if (hit.DamageType == DamageClass.Melee)
                hit.Damage = (int)(hit.Damage * resMelee);
            if (hit.DamageType == DamageClass.Ranged)
                hit.Damage = (int)(hit.Damage * resRange);
            if (hit.DamageType == DamageClass.Magic)
                hit.Damage = (int)(hit.Damage * resMagic);
            if (hit.DamageType == DamageClass.Summon || hit.DamageType == DamageClass.SummonMeleeSpeed)
                hit.Damage = (int)(hit.Damage * resSummon);

        }
        private void SpawnDupeNPCFunni(NPC npc)
        {
            if (npc.life <= npc.lifeMax * .05f)
                return;
            if (Main.rand.NextBool((int)(npc.life * .1f)))
            {
                int npclocal = NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, npc.type);
                Main.npc[npclocal].life = npc.life;
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC);
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
