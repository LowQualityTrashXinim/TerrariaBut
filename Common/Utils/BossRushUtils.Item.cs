using BossRush.Common.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossRush
{
    public partial class BossRushUtils
    {
        /// <summary>
        /// Set your own DamageClass type
        /// </summary>
        public static void BossRushSetDefault(this Item item, int width, int height, int damage, float knockback, int useTime, int useAnimation, int useStyle, bool autoReuse)
        {
            item.width = width;
            item.height = height;
            item.damage = damage;
            item.knockBack = knockback;
            item.useTime = useTime;
            item.useAnimation = useAnimation;
            item.useStyle = useStyle;
            item.autoReuse = autoReuse;
        }
        public static void BossRushDefaultToConsume(this Item item, int width, int height)
        {
            item.width = width;
            item.height = height;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldUp;
            item.autoReuse = false;
            item.consumable = true;
        }
        /// <summary>
        /// Use this along with <see cref="BossRushSetDefault(Item, int, int, int, float, int, int, int, bool)"/>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="spearType"></param>
        /// <param name="shootSpeed"></param>
        public static void BossRushSetDefaultSpear(this Item item, int spearType, float shootSpeed)
        {
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shootSpeed = shootSpeed;
            item.shoot = spearType;
            item.DamageType = DamageClass.Melee;
        }
        public static void BossRushDefaultMeleeShootCustomProjectile(this Item item, int width, int height, int damage, float knockback, int useTime, int useAnimation, int useStyle, int shoot, float shootspeed, bool autoReuse)
        {
            BossRushSetDefault(item, width, height, damage, knockback, useTime, useAnimation, useStyle, autoReuse);
            item.shoot = shoot;
            item.shootSpeed = shootspeed;
            item.DamageType = DamageClass.Melee;
        }
        public static void BossRushDefaultMeleeCustomProjectile(this Item item, int width, int height, int damage, float knockback, int useTime, int useAnimation, int useStyle, int shoot, bool autoReuse)
        {
            BossRushSetDefault(item, width, height, damage, knockback, useTime, useAnimation, useStyle, autoReuse);
            item.shoot = shoot;
            item.shootSpeed = 1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.DamageType = DamageClass.Melee;
        }
        public static void BossRushDefaultRange(this Item item, int width, int height, int damage, float knockback, int useTime, int useAnimation, int useStyle, int shoot, float shootSpeed, bool autoReuse, int useAmmo = 0
        )
        {
            BossRushSetDefault(item, width, height, damage, knockback, useTime, useAnimation, useStyle, autoReuse);
            item.shoot = shoot;
            item.shootSpeed = shootSpeed;
            item.useAmmo = useAmmo;
            item.noMelee = true;
            item.DamageType = DamageClass.Ranged;
        }

        public static void BossRushDefaultMagic(this Item item, int width, int height, int damage, float knockback, int useTime, int useAnimation, int useStyle, int shoot, float shootSpeed, int manaCost, bool autoReuse
            )
        {
            BossRushSetDefault(item, width, height, damage, knockback, useTime, useAnimation, useStyle, autoReuse);
            item.shoot = shoot;
            item.shootSpeed = shootSpeed;
            item.mana = manaCost;
            item.noMelee = true;
            item.DamageType = DamageClass.Magic;
        }

        public static void BossRushDefaultMagic(Item item, int shoot, float shootSpeed, int manaCost)
        {
            item.shoot = shoot;
            item.shootSpeed = shootSpeed;
            item.mana = manaCost;
            item.noMelee = true;
        }
        public static void GetWeapon(out int ReturnWeapon, out int Amount, int rng = 0)
        {
            if (rng > 6 || rng <= 0)
            {
                rng = Main.rand.Next(1, 6);
            }
            ReturnWeapon = 0;
            Amount = 1;
            List<int> DropItemMelee = new List<int>();
            List<int> DropItemRange = new List<int>();
            List<int> DropItemMagic = new List<int>();
            List<int> DropItemSummon = new List<int>();
            List<int> DropItemMisc = new List<int>();
            List<int> list = new() { 0 };
            if (NPC.downedSlimeKing)
            {
                list.Add(1);
            }
            if (NPC.downedBoss1)
            {
                list.Add(2);
            }
            if (NPC.downedBoss2)
            {
                list.Add(3);
            }
            if (NPC.downedBoss3)
            {
                list.Add(4);
            }
            if (NPC.downedQueenBee)
            {
                list.Add(5);
            }
            if (NPC.downedDeerclops)
            {
                list.Add(6);
            }
            if (Main.hardMode)
            {
                list.Add(7);
            }
            if (NPC.downedQueenSlime)
            {
                list.Add(8);
            }
            if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
            {
                list.Add(9);
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                list.Add(10);
            }
            if (NPC.downedPlantBoss)
            {
                list.Add(11);
            }
            if (NPC.downedGolemBoss)
            {
                list.Add(12);
            }
            if (NPC.downedEmpressOfLight)
            {
                list.Add(13);
            }
            if (NPC.downedAncientCultist)
            {
                list.Add(14);
            }
            if (NPC.downedMoonlord)
            {
                list.Add(15);
            }
            if (rng < 6 && rng > 0)
            {
                AddLoot(list, DropItemMelee, DropItemRange, DropItemMagic, DropItemSummon, DropItemMisc);
            }
            ChooseWeapon(rng, ref ReturnWeapon, ref Amount, DropItemMelee, DropItemRange, DropItemMagic, DropItemSummon, DropItemMisc);
        }
        private static void AddLoot(List<int> FlagNumber, List<int> DropItemMelee, List<int> DropItemRange, List<int> DropItemMagic, List<int> DropItemSummon, List<int> DropItemMisc)
        {
            for (int i = 0; i < FlagNumber.Count; ++i)
            {
                switch (FlagNumber[i])
                {
                    case 0://PreBoss
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePreBoss);
                        DropItemRange.AddRange(TerrariaArrayID.RangePreBoss);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPreBoss);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonPreBoss);
                        DropItemMisc.AddRange(TerrariaArrayID.SpecialPreBoss);
                        break;
                    case 1://PreEoC
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePreEoC);
                        DropItemRange.AddRange(TerrariaArrayID.RangePreEoC);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPreEoC);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonerPreEoC);
                        DropItemMisc.AddRange(TerrariaArrayID.Special);
                        break;
                    case 2://EoC
                        DropItemMelee.Add(ItemID.Code1);
                        DropItemMagic.Add(ItemID.ZapinatorGray);
                        break;
                    case 3://Evil boss
                        DropItemMelee.AddRange(TerrariaArrayID.MeleeEvilBoss);
                        DropItemRange.Add(ItemID.MoltenFury);
                        DropItemRange.Add(ItemID.StarCannon);
                        DropItemRange.Add(ItemID.AleThrowingGlove);
                        DropItemRange.Add(ItemID.Harpoon);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicEvilBoss);
                        DropItemSummon.Add(ItemID.ImpStaff);
                        break;
                    case 4://Skeletron
                        DropItemMelee.AddRange(TerrariaArrayID.MeleeSkel);
                        DropItemRange.AddRange(TerrariaArrayID.RangeSkele);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicSkele);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonSkele);
                        break;
                    case 5://Queen bee
                        DropItemMelee.Add(ItemID.BeeKeeper);
                        DropItemRange.Add(ItemID.BeesKnees); DropItemRange.Add(ItemID.Blowgun);
                        DropItemMagic.Add(ItemID.BeeGun);
                        DropItemSummon.Add(ItemID.HornetStaff);
                        DropItemMisc.Add(ItemID.Beenade);
                        break;
                    case 6://Deerclop
                        DropItemRange.Add(ItemID.PewMaticHorn);
                        DropItemMagic.Add(ItemID.WeatherPain);
                        DropItemSummon.Add(ItemID.HoundiusShootius);
                        break;
                    case 7://Wall of flesh
                        DropItemMelee.AddRange(TerrariaArrayID.MeleeHM);
                        DropItemRange.AddRange(TerrariaArrayID.RangeHM);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicHM);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonHM);
                        break;
                    case 8://Queen slime
                        DropItemMelee.AddRange(TerrariaArrayID.MeleeQS);
                        DropItemSummon.Add(ItemID.Smolstar);
                        break;
                    case 9://First mech
                        DropItemMelee.AddRange(TerrariaArrayID.MeleeMech);
                        DropItemRange.Add(ItemID.SuperStarCannon);
                        DropItemRange.Add(ItemID.DD2PhoenixBow);
                        DropItemMagic.Add(ItemID.UnholyTrident);
                        break;
                    case 10://All three mech
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePostAllMechs);
                        DropItemRange.AddRange(TerrariaArrayID.RangePostAllMech);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPostAllMech);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonPostAllMech);
                        break;
                    case 11://Plantera
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePostPlant);
                        DropItemRange.AddRange(TerrariaArrayID.RangePostPlant);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPostPlant);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonPostPlant);
                        break;
                    case 12://Golem
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePostGolem);
                        DropItemRange.AddRange(TerrariaArrayID.RangePostGolem);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPostGolem);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonPostGolem);
                        break;
                    case 13://Pre lunatic (Duke fishron, EoL, ect)
                        DropItemMelee.AddRange(TerrariaArrayID.MeleePreLuna);
                        DropItemRange.AddRange(TerrariaArrayID.RangePreLuna);
                        DropItemMagic.AddRange(TerrariaArrayID.MagicPreLuna);
                        DropItemSummon.AddRange(TerrariaArrayID.SummonPreLuna);
                        break;
                    case 14://Lunatic Cultist
                        DropItemMelee.Add(ItemID.DayBreak);
                        DropItemMelee.Add(ItemID.SolarEruption);
                        DropItemRange.Add(ItemID.Phantasm);
                        DropItemRange.Add(ItemID.VortexBeater);
                        DropItemMagic.Add(ItemID.NebulaArcanum);
                        DropItemMagic.Add(ItemID.NebulaBlaze);
                        DropItemSummon.Add(ItemID.StardustCellStaff);
                        DropItemSummon.Add(ItemID.StardustDragonStaff);
                        break;
                    case 15://MoonLord
                        DropItemMelee.Add(ItemID.StarWrath);
                        DropItemMelee.Add(ItemID.Meowmere);
                        DropItemMelee.Add(ItemID.Terrarian);
                        DropItemRange.Add(ItemID.SDMG);
                        DropItemRange.Add(ItemID.Celeb2);
                        DropItemMagic.Add(ItemID.LunarFlareBook);
                        DropItemMagic.Add(ItemID.LastPrism);
                        DropItemSummon.Add(ItemID.RainbowCrystalStaff);
                        DropItemSummon.Add(ItemID.MoonlordTurretStaff);
                        break;
                }
            }
        }
        private static void ChooseWeapon(int rng, ref int weapon, ref int amount, List<int> DropItemMelee, List<int> DropItemRange, List<int> DropItemMagic, List<int> DropItemSummon, List<int> DropItemMisc)
        {
            switch (rng)
            {
                case 0:
                    weapon = ItemID.None;
                    break;
                case 1:
                    weapon = Main.rand.NextFromCollection(DropItemMelee);
                    break;
                case 2:
                    weapon = Main.rand.NextFromCollection(DropItemRange);
                    break;
                case 3:
                    weapon = Main.rand.NextFromCollection(DropItemMagic);
                    break;
                case 4:
                    weapon = Main.rand.NextFromCollection(DropItemSummon);
                    break;
                case 5:
                    if (DropItemMisc.Count < 1)
                    {
                        int rngM = Main.rand.Next(1, 5);
                        ChooseWeapon(rngM, ref weapon, ref amount, DropItemMelee, DropItemRange, DropItemMagic, DropItemSummon, DropItemMisc);
                        break;
                    }
                    amount += 199;
                    weapon = Main.rand.NextFromCollection(DropItemMisc);
                    break;
            }
        }
    }
}