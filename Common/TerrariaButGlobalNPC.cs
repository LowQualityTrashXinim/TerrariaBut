using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        int PositiveLifeRegen = 0;
        int PositiveLifeRegenCount = 0;
        public static int AmountOfModCurrentlyEnable()
        {
            int HowManyModIsEnable = ModLoader.Mods.Length;
            return Math.Clamp(HowManyModIsEnable - 2, 0, 99999);
        }
        public override void SetDefaults(NPC entity)
        {
            float amount = 1 + AmountOfModCurrentlyEnable() * .05f;
            entity.lifeMax = (int)(amount * entity.lifeMax);
            entity.life = entity.lifeMax;
            entity.damage = (int)(amount * entity.damage);
            entity.defense = (int)(amount * entity.defense);

        }
        public override void PostAI(NPC npc)
        {
            if (++PositiveLifeRegenCount >= 60)
            {
                PositiveLifeRegenCount = 0;
                npc.life = Math.Clamp(npc.life + PositiveLifeRegen, 0, npc.lifeMax);
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            npc.lifeMax += Main.rand.Next(-npc.lifeMax + 1, npc.lifeMax);
            npc.life = npc.lifeMax;
            PositiveLifeRegen += Main.rand.Next((int)(npc.lifeMax * .25f));
            if (PositiveLifeRegen >= 20 && !Main.hardMode)
            {
                PositiveLifeRegen = 20;
            }
            if (PositiveLifeRegen >= 50 && Main.hardMode)
            {
                PositiveLifeRegen = 50;
            }
            if (npc.damage > 0)
                npc.damage += Main.rand.Next(-npc.damage + 1, npc.damage);
            npc.scale += Main.rand.NextFloat(-.75f, 1);
            npc.defense += npc.defense == 0 ? Main.rand.Next(0, npc.lifeMax + 1) : Math.Clamp(Main.rand.Next(-npc.defense, npc.defense), 0, npc.lifeMax);
        }
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }
                if (item.IsAir)
                {
                    continue;
                }
                item.value *= 5;
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.boss)
            {
                modifiers.FinalDamage.Flat += target.statLife * .15f;
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByItem(npc, player, item, hit, damageDone);
            SpawnDupeNPCFunni(npc);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
            SpawnDupeNPCFunni(npc);
        }
        private void SpawnDupeNPCFunni(NPC npc)
        {
            if (npc.boss)
                return;
            if (npc.life <= npc.lifeMax * .05f || npc.life <= 100)
                return;
            if (Main.rand.NextBool((int)(npc.life * .5f) + 10))
            {
                int npclocal = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, npc.type);
                Main.npc[npclocal].life = npc.life;
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC);
            }
        }
        public override void OnKill(NPC npc)
        {
        }
    }
}
