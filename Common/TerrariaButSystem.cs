using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BossRush;

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
                switch (Main.rand.Next(5))
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
