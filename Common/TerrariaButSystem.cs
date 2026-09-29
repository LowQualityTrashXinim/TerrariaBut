using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
            if (WorldGen.generatingWorld)
            {
                return;
            }
            if (BossRushUtils.GetConfig.Disable_PotRandomize)
            {
                return;
            }
            if (type == TileID.Pots && Main.rand.NextBool(10))
            {
                PotType typ1e = Main.rand.Next(PotSystem.potType);
                typ1e.OnPotDestroyed(i, j);
                noItem = true;
                return;
            }
            noItem = Main.rand.NextBool();
        }
    }
}
