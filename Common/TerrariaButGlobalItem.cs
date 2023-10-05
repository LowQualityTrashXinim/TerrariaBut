using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaBut.Common
{
    internal class TerrariaButGlobalItem : GlobalItem
    {
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (item.createTile != -1)
                return;
            if (item.damage > 0)
                item.damage += Main.rand.Next(-item.damage + 1, item.damage + 1);
            if (item.knockBack > 0)
                item.knockBack += Main.rand.NextFloat(-item.knockBack + 1, item.knockBack + 1);
            item.useTime += Main.rand.Next(-item.useTime + 1, item.useTime + 1);
            item.useAnimation += Main.rand.Next(-item.useAnimation + 1, item.useAnimation + 1);
            item.shoot = Main.rand.Next(ProjectileLoader.ProjectileCount);
            item.shootSpeed += item.shootSpeed == 0 ? Main.rand.NextFloat(0, 50) : Main.rand.NextFloat(-item.shootSpeed + 1, item.shootSpeed);
            item.scale += Main.rand.NextFloat(-item.scale + .1f, item.scale);
        }
    }
}