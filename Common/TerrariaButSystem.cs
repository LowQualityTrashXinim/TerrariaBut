using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaBut.Common.Utils;
using Terraria.DataStructures;

namespace TerrariaBut.Common
{
    internal class TerrariaButSystem : ModSystem
    {
    }
    public class TerrariaButTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if(WorldGen.generatingWorld)
            {
                return;
            }
            if (type == TileID.Pots && Main.rand.NextBool(10))
            {
                IEntitySource source = new EntitySource_TileBreak(i, j);
                switch (Main.rand.Next(10))
                {
                    case 0:
                        for (int a = 0; a < 8; a++)
                        {
                            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(8, 360, i) * 10, ProjectileID.Boulder, 400, 1, Main.myPlayer);
                        }
                        break;
                    case 1:
                        for (int a = 0; a < 4; a++)
                        {
                            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(4, 360, i) * 10, ProjectileID.BouncyBoulder, 400, 1, Main.myPlayer);
                        }
                        break;
                    case 2:
                        for (int a = 0; a < 16; a++)
                        {
                            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(16, 360, i) * 10, ProjectileID.BouncyDynamite, 400, 1, Main.myPlayer);
                        }
                        break;
                    case 3:
                        NPC.NewNPC(source, i * 16, j * 16, NPCID.ExplosiveBunny);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                    case 4:
                        NPC.NewNPC(source, i * 16, j * 16, NPCID.GoldenSlime);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                    case 5:
                        BossRushUtils.GetWeapon(out int Weapon, out int amount);
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 1, 1, new Item(Weapon, amount));
                        break;
                    case 6:
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 1, 1, new Item(ItemID.PlatinumCoin, 10));
                        break;
                    case 7:
                        Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.UnitX * Main.rand.NextBool().BoolOne(), ProjectileID.LifeCrystalBoulder, 999, 1, Main.myPlayer);
                        break;
                    case 8:
                        int ran = Main.rand.Next(7, 14);
                        for (int a = 0; a < ran; a++)
                        {
                            BossRushUtils.GetWeapon(out int WeaponA, out int amountA);
                            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 1, 1, new Item(WeaponA, amountA));
                        }
                        break;
                    case 9:
                        int text = BossRushUtils.CombatTextRevamp(new Rectangle(i, j, 1, 1), Color.Red, "Boo");
                        Main.combatText[text].scale += 2;
                        NPC.NewNPC(source, i * 16, j * 16, NPCID.Ghost);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                }
                return;
            }
            noItem = Main.rand.NextBool();
        }
    }
}
