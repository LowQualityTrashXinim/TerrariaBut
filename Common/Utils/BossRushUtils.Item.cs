﻿using System;
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
        public enum MeleeStyle
        {
            CheckVanillaSwingWithModded,
            CheckOnlyModded,
            CheckOnlyModdedWithoutDefault
        }
    }
}