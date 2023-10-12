using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalItem : GlobalItem
    {
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (item.createTile != -1)
                return;
            if (item.createWall != -1)
                return;
            if (item.damage > 0)
                item.damage += Main.rand.Next(-item.damage + 1, item.damage + 1);
            if (item.knockBack > 0)
                item.knockBack += Main.rand.NextFloat(-item.knockBack + 1, item.knockBack + 1);
            item.useTime += Main.rand.Next(-item.useTime + 1, item.useTime + 1);
            item.useAnimation += Main.rand.Next(-item.useAnimation + 1, item.useAnimation + 1);
            if (Main.rand.NextBool())
                item.shoot = Main.rand.Next(ProjectileLoader.ProjectileCount);
            item.shootSpeed += item.shootSpeed == 0 ? Main.rand.NextFloat(0, 50) : Main.rand.NextFloat(-item.shootSpeed + 1, item.shootSpeed);
            item.scale += Main.rand.NextFloat(-item.scale + .1f, item.scale);
            item.crit += Main.rand.Next(-100, 100);
            item.autoReuse = Main.rand.NextBool();
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (player.ItemAnimationJustStarted)
            {
                if (item.axe != 0 || item.pick != 0)
                {
                    if (Main.rand.NextBool(1000))
                    {
                        item.stack = 0;
                        return false;
                    }
                    return base.UseItem(item, player);
                }
                if (Main.rand.NextBool(200))
                {
                    item.stack = 0;
                    return false;
                }
            }
            return base.UseItem(item, player);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = item.shoot;
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("Damage", item.damage);
            tag.Add("Knockback", item.knockBack);
            tag.Add("useTime", item.useTime);
            tag.Add("useAnimation", item.useAnimation);
            tag.Add("shoot", item.shoot);
            tag.Add("shootspeed", item.shootSpeed);
            tag.Add("scale", item.scale);
            tag.Add("crit", item.crit);
            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.TryGet("Damage", out int damage))
                item.damage = damage;
            if (tag.TryGet("Knockback", out float knockBack))
                item.knockBack = knockBack;
            if (tag.TryGet("useTime", out int useTime))
                item.useTime = useTime;
            if (tag.TryGet("useAnimation", out int useAnimation))
                item.useAnimation = useAnimation;
            if (tag.TryGet("shoot", out int shoot))
                item.shoot = shoot;
            if (tag.TryGet("shootspeed", out float shootspeed))
                item.shootSpeed = shootspeed;
            if (tag.TryGet("scale", out float scale))
                item.scale = scale;
            if (tag.TryGet("crit", out int crit))
                item.crit = crit;
        }
    }
}