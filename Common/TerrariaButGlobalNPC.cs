using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        float resMelee, resRange, resMagic, resSummon = 1;
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
                PositiveLifeRegen = 0;
                npc.life = Math.Clamp(npc.life + PositiveLifeRegen, 0, npc.lifeMax);
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            resMelee = Main.rand.NextFloat(0, 1f);
            resRange = Main.rand.NextFloat(0, 1f);
            resMagic = Main.rand.NextFloat(0, 1f);
            resSummon = Main.rand.NextFloat(0, 1f);
            npc.lifeMax += Main.rand.Next(-npc.lifeMax + 1, npc.lifeMax);
            npc.life = npc.lifeMax;
            PositiveLifeRegen += Main.rand.Next((int)(npc.lifeMax * .25f));
            if (npc.damage > 0)
                npc.damage += Main.rand.Next(-npc.damage + 1, npc.damage);
            npc.scale += Main.rand.NextFloat(-.75f, 1);
            npc.defense += npc.defense == 0 ? Main.rand.Next(0, npc.lifeMax + 1) : Math.Clamp(Main.rand.Next(-npc.defense, npc.defense), 0, npc.lifeMax);
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            ResDamage(ref modifiers);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            ResDamage(ref modifiers);
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
        private void ResDamage(ref NPC.HitModifiers modifiers)
        {
            if (modifiers.DamageType == DamageClass.Melee)
                modifiers.FinalDamage *= resMelee;
            if (modifiers.DamageType == DamageClass.Ranged)
                modifiers.FinalDamage *= resRange;
            if (modifiers.DamageType == DamageClass.Magic)
                modifiers.FinalDamage *= resMagic;
            if (modifiers.DamageType == DamageClass.Summon || modifiers.DamageType == DamageClass.SummonMeleeSpeed)
                modifiers.FinalDamage *= resSummon;

        }
        private void SpawnDupeNPCFunni(NPC npc)
        {
            if (npc.life <= npc.lifeMax * .05f || npc.life <= 100)
                return;
            if (Main.rand.NextBool((int)(npc.life * .1f)))
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
