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
        MeleeDMG,
        RangeDMG,
        MagicDMG,
        SummonDMG,
        MovementSpeed,
        JumpBoost,
        MaxHP,
        RegenHP,
        MaxMana,
        RegenMana,
        Defense,
        DamageUniverse,
        CritChance,
        CritDamage,
        DefenseEffectiveness,
        MaxMinion,
        MaxSentry,
        Thorn
    }
    internal class TerrariaButPlayer : ModPlayer
    {
        public const int maxStatCanBeAchieved = 99999;
        public float MeleeDMG = 0;
        public float RangeDMG = 0;
        public float MagicDMG = 0;
        public float SummonDMG = 0;
        public float Movement = 0;
        public float JumpBoost = 0;
        public int HPMax = 0;
        public float HPRegen = 0;
        public int ManaMax = 0;
        public float ManaRegen = 0;
        public int DefenseBase = 0;
        public float DamagePure = 0;
        public int CritStrikeChance = 0;
        public float Thorn = 0;
        public float CritDamage = 1;
        public float DefenseEffectiveness = 1;
        public int MinionSlot = 0;
        public int SentrySlot = 0;

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            MeleeDMG = Main.rand.NextFloat(-3, 3);
            RangeDMG = Main.rand.NextFloat(-3, 3);
            MagicDMG = Main.rand.NextFloat(-3, 3);
            SummonDMG = Main.rand.NextFloat(-3, 3);
            Movement = Main.rand.NextFloat(-3, 3);
            JumpBoost = Main.rand.NextFloat(-3, 3);

            HPMax = Main.rand.Next(-Player.statLifeMax + 1, Player.statLifeMax2);
            HPRegen = Main.rand.NextFloat(0, 100);
            ManaMax = Main.rand.Next(-Player.statManaMax + 1, Player.statManaMax2);
            ManaRegen = Main.rand.NextFloat(0, 100);

            DefenseBase = Main.rand.Next(-100, 101);
            DamagePure = Main.rand.NextFloat(-3, 3);
            CritStrikeChance = Main.rand.Next(-3, 101);
            Thorn = Main.rand.NextFloat(-3, 3);
            CritDamage = Main.rand.NextFloat(-3, 3);
            DefenseEffectiveness = Main.rand.NextFloat(-3, 3);

            MinionSlot = Main.rand.Next(-10, 10);
            SentrySlot = Main.rand.Next(-10, 10);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (item.DamageType == DamageClass.Melee)
            {
                damage.Base = Math.Clamp(MeleeDMG + damage.Base, 0, maxStatCanBeAchieved);
            }
            if (item.DamageType == DamageClass.Ranged)
            {
                damage.Base = Math.Clamp(RangeDMG + damage.Base, 0, maxStatCanBeAchieved);
            }
            if (item.DamageType == DamageClass.Magic)
            {
                damage.Base = Math.Clamp(MagicDMG + damage.Base, 0, maxStatCanBeAchieved);
            }
            if (item.DamageType == DamageClass.Summon)
            {
                damage.Base = Math.Clamp(SummonDMG + damage.Base, 0, maxStatCanBeAchieved);
            }
            damage.Base = Math.Clamp(DamagePure + damage.Base, 0, maxStatCanBeAchieved);
        }
        public override void ModifyWeaponCrit(Item item, ref float crit)
        {
            crit = Math.Clamp(CritStrikeChance + crit, 0, maxStatCanBeAchieved);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage.Flat = Math.Clamp(CritDamage, -modifiers.CritDamage.Base + 1, 999999) * modifiers.CritDamage.Base;
        }
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;
            mana = StatModifier.Default;

            health.Base = Math.Clamp(HPMax + health.Base, -Player.statLifeMax + 1, maxStatCanBeAchieved);
            mana.Base = Math.Clamp(ManaMax + mana.Base, -Player.statManaMax + 1, maxStatCanBeAchieved);
        }
        public override void ResetEffects()
        {
            Player.statDefense += Math.Clamp(DefenseBase, -(maxStatCanBeAchieved + Player.statDefense), maxStatCanBeAchieved);
            Player.moveSpeed = Math.Clamp(Movement + Player.moveSpeed, 0, maxStatCanBeAchieved);
            Player.jumpSpeedBoost = Math.Clamp(JumpBoost + Player.jumpSpeedBoost, 0, maxStatCanBeAchieved);
            Player.lifeRegen = (int)Math.Clamp(HPRegen * Player.lifeRegen, 0, maxStatCanBeAchieved);
            Player.manaRegen = (int)Math.Clamp(ManaRegen * Player.manaRegen, 0, maxStatCanBeAchieved);
            Player.DefenseEffectiveness *= Math.Clamp(DefenseEffectiveness, 0, maxStatCanBeAchieved);
            Player.maxMinions = Math.Clamp(MinionSlot + Player.maxMinions, 0, maxStatCanBeAchieved);
            Player.maxTurrets = Math.Clamp(SentrySlot + Player.maxTurrets, 0, maxStatCanBeAchieved);
            Player.thorns += Thorn;
        }
        public override void Initialize()
        {
            MeleeDMG = 0;
            RangeDMG = 0;
            MagicDMG = 0;
            SummonDMG = 0;
            Movement = 0;
            JumpBoost = 0;
            HPMax = 0;
            HPRegen = 0;
            ManaMax = 0;
            ManaRegen = 0;
            DefenseBase = 0;
            DamagePure = 0;
            CritStrikeChance = 0;
            Thorn = 0;
            CritDamage = 1;
            DefenseEffectiveness = 1;
            MinionSlot = 0;
            SentrySlot = 0;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)TerrariaBut.MessageType.CardEffect);
            packet.Write((byte)Player.whoAmI);
            packet.Write(MeleeDMG);
            packet.Write(RangeDMG);
            packet.Write(MagicDMG);
            packet.Write(SummonDMG);
            packet.Write(Movement);
            packet.Write(JumpBoost);
            packet.Write(HPMax);
            packet.Write(HPRegen);
            packet.Write(ManaMax);
            packet.Write(ManaRegen);
            packet.Write(DefenseBase);
            packet.Write(DamagePure);
            packet.Write(CritStrikeChance);
            packet.Write(CritDamage);
            packet.Write(DefenseEffectiveness);
            packet.Write(MinionSlot);
            packet.Write(SentrySlot);
            packet.Write(Thorn);
            packet.Send(toWho, fromWho);
        }
        public override void SaveData(TagCompound tag)
        {
            tag["MeleeDMG"] = MeleeDMG;
            tag["RangeDMG"] = RangeDMG;
            tag["MagicDMG"] = MagicDMG;
            tag["SummonDMG"] = SummonDMG;
            tag["Movement"] = Movement;
            tag["JumpBoost"] = JumpBoost;
            tag["HPMax"] = HPMax;
            tag["HPRegen"] = HPRegen;
            tag["ManaMax"] = ManaMax;
            tag["ManaRegen"] = ManaRegen;
            tag["DefenseBase"] = DefenseBase;
            tag["DamagePure"] = DamagePure;
            tag["CritStrikeChance"] = CritStrikeChance;
            tag["CritDamage"] = CritDamage;
            tag["DefenseEffectiveness"] = DefenseEffectiveness;
            tag["MinionSlot"] = MinionSlot;
            tag["SentrySlot"] = SentrySlot;
            tag["Thorn"] = Thorn;
        }
        public override void LoadData(TagCompound tag)
        {
            MeleeDMG = (float)tag["MeleeDMG"];
            RangeDMG = (float)tag["RangeDMG"];
            MagicDMG = (float)tag["MagicDMG"];
            SummonDMG = (float)tag["SummonDMG"];
            Movement = (float)tag["Movement"];
            JumpBoost = (float)tag["JumpBoost"];
            HPMax = (int)tag["HPMax"];
            HPRegen = (float)tag["HPRegen"];
            ManaMax = (int)tag["ManaMax"];
            ManaRegen = (float)tag["ManaRegen"];
            DefenseBase = (int)tag["DefenseBase"];
            DamagePure = (float)tag["DamagePure"];
            CritStrikeChance = (int)tag["CritStrikeChance"];
            CritDamage = (float)tag["CritDamage"];
            DefenseEffectiveness = (float)tag["DefenseEffectiveness"];
            MinionSlot = (int)tag["MinionSlot"];
            SentrySlot = (int)tag["SentrySlot"];
            Thorn = (float)tag["Thorn"];
        }
        public void ReceivePlayerSync(BinaryReader reader)
        {
            MeleeDMG = reader.ReadSingle();
            RangeDMG = reader.ReadSingle();
            MagicDMG = reader.ReadSingle();
            SummonDMG = reader.ReadSingle();
            Movement = reader.ReadSingle();
            JumpBoost = reader.ReadSingle();
            HPMax = reader.ReadInt32();
            HPRegen = reader.ReadSingle();
            ManaMax = reader.ReadInt32();
            ManaRegen = reader.ReadSingle();
            DefenseBase = reader.ReadInt32();
            DamagePure = reader.ReadSingle();
            CritStrikeChance = reader.ReadInt32();
            CritDamage = reader.ReadSingle();
            DefenseEffectiveness = reader.ReadSingle();
            MinionSlot = reader.ReadInt32();
            SentrySlot = reader.ReadInt32();
            Thorn = reader.ReadSingle();
        }
        public override void CopyClientState(ModPlayer targetCopy)
        {
            TerrariaButPlayer clone = (TerrariaButPlayer)targetCopy;
            clone.MeleeDMG = MeleeDMG;
            clone.RangeDMG = RangeDMG;
            clone.MagicDMG = MagicDMG;
            clone.SummonDMG = SummonDMG;
            clone.Movement = Movement;
            clone.JumpBoost = JumpBoost;
            clone.HPMax = HPMax;
            clone.HPRegen = HPRegen;
            clone.ManaMax = ManaMax;
            clone.ManaRegen = ManaRegen;
            clone.DefenseBase = DefenseBase;
            clone.DamagePure = DamagePure;
            clone.CritStrikeChance = CritStrikeChance;
            clone.CritDamage = CritDamage;
            clone.DefenseEffectiveness = DefenseEffectiveness;
            clone.MinionSlot = MinionSlot;
            clone.SentrySlot = SentrySlot;
            clone.Thorn = Thorn;
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            TerrariaButPlayer clone = (TerrariaButPlayer)clientPlayer;
            if (MeleeDMG != clone.MeleeDMG
                || RangeDMG != clone.RangeDMG
                || MagicDMG != clone.MagicDMG
                || SummonDMG != clone.SummonDMG
                || Movement != clone.Movement
                || JumpBoost != clone.JumpBoost
                || HPMax != clone.HPMax
                || HPRegen != clone.HPRegen
                || ManaMax != clone.ManaMax
                || ManaRegen != clone.ManaRegen
                || DefenseBase != clone.DefenseBase
                || DamagePure != clone.DamagePure
                || CritStrikeChance != clone.CritStrikeChance
                || CritDamage != clone.CritDamage
                || DefenseEffectiveness != clone.DefenseEffectiveness
                || MinionSlot != clone.MinionSlot
                || SentrySlot != clone.SentrySlot
                || Thorn != clone.Thorn)
            {
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
            }
        }
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            if (Main.rand.NextBool(200))
            {
                BossRushUtils.GetWeapon(out int Weapon, out int amount);
                Player.QuickSpawnItem(Player.GetSource_FromThis(), Weapon, amount);
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