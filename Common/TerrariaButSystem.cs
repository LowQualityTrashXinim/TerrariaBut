using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaBut.Common.Utils;

namespace TerrariaBut.Common
{
    internal class TerrariaButSystem : ModSystem
    {
    }
    public class TerrariaButTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Pots && Main.rand.NextBool(10))
            {
                switch (Main.rand.Next(10))
                {

                    case 0:
                        for (int a = 0; a < 8; a++)
                        {
                            Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), new Vector2(i, j), Vector2.One.Vector2DistributeEvenly(8, 360, i) * 10, ProjectileID.Boulder, 400, 1);
                        }
                        break;
                    case 1:
                        for (int a = 0; a < 4; a++)
                        {
                            Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), new Vector2(i, j), Vector2.One.Vector2DistributeEvenly(4, 360, i) * 10, ProjectileID.BouncyBoulder, 400, 1);
                        }
                        break;
                    case 2:
                        for (int a = 0; a < 16; a++)
                        {
                            Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), new Vector2(i, j), Vector2.One.Vector2DistributeEvenly(16, 360, i) * 10, ProjectileID.BouncyDynamite, 400, 1);
                        }
                        break;
                    case 3:
                        NPC.NewNPC(Entity.GetSource_NaturalSpawn(), i, j, NPCID.ExplosiveBunny);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                    case 4:
                        NPC.NewNPC(Entity.GetSource_NaturalSpawn(), i, j, NPCID.GoldenSlime);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                    case 5:
                        BossRushUtils.GetWeapon(out int Weapon, out int amount);
                        Item.NewItem(Entity.GetSource_NaturalSpawn(), i, j, 1, 1, new Item(Weapon, amount));
                        break;
                    case 6:
                        Item.NewItem(Entity.GetSource_NaturalSpawn(), i, j, 1, 1, new Item(ItemID.PlatinumCoin, 10));
                        break;
                    case 7:
                        Projectile.NewProjectile(Entity.GetSource_NaturalSpawn(), new Vector2(i, j), Vector2.UnitX * Main.rand.NextBool().BoolOne(), ProjectileID.LifeCrystalBoulder, 999, 1);
                        break;
                    case 8:
                        int ran = Main.rand.Next(7, 14);
                        for (int a = 0; a < ran; a++)
                        {
                            BossRushUtils.GetWeapon(out int WeaponA, out int amountA);
                            Item.NewItem(Entity.GetSource_NaturalSpawn(), i, j, 1, 1, new Item(WeaponA, amountA));
                        }
                        break;
                    case 9:
                        int text = BossRushUtils.CombatTextRevamp(new Rectangle(i, j, 1, 1), Color.Red, "Boo");
                        Main.combatText[text].scale += 2;
                        NPC.NewNPC(Entity.GetSource_NaturalSpawn(), i, j, NPCID.Ghost);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC);
                        break;
                }
                noItem = true;
                return;
            }
            noItem = Main.rand.NextBool();
            fail = Main.rand.NextBool();
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
        }
    }
}
