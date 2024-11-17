using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrariaBut.Common;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using TerrariaBut.Common.Utils;

namespace TerrariaBut.Common
{
    public class PotSystem : ModSystem
    {
        public static List<PotType> potType = new();
        public static short Register(PotType type)
        {
            ModTypeLookup<PotType>.Register(type);
            short sh = short.Parse($"{potType.Count - 1}");
            return sh;
        }
    }
    public abstract class PotType : ModType
    {
        short Type = 0;
        protected override void Register()
        {
            Type = PotSystem.Register(this);
        }
        public virtual void OnPotDestroyed(int i, int j) { }
    }
}
public class BoulderParty : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        for (int a = 0; a < 8; a++)
        {
            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(8, 360, i) * 10, ProjectileID.Boulder, 400, 1, Main.myPlayer);
        }
    }
}
public class BouncyBoulderParty : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        for (int a = 0; a < 4; a++)
        {
            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(4, 360, i) * 10, ProjectileID.BouncyBoulder, 400, 1, Main.myPlayer);
        }
    }
}
public class BouncyDynamiteParty : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        for (int a = 0; a < 16; a++)
        {
            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(16, 360, i) * 10, ProjectileID.BouncyDynamite, 400, 1, Main.myPlayer);
        }
    }
}
public class Spawn_ExplosiveBunny : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        NPC.NewNPC(source, i * 16, j * 16, NPCID.ExplosiveBunny);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class Spawn_GoldenSlime : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        NPC.NewNPC(source, i * 16, j * 16, NPCID.GoldenSlime);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class Gift_Wealth : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 1, 1, new Item(ItemID.PlatinumCoin, Main.rand.Next(1, 11)));
    }
}
public class LifeCrystal : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.UnitX * Main.rand.NextBool().BoolOne(), ProjectileID.LifeCrystalBoulder, 999, 1, Main.myPlayer);
    }
}
public class Spawn_Ghost : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        int text = BossRushUtils.CombatTextRevamp(new Rectangle(i, j, 1, 1), Color.Red, "Boo");
        Main.combatText[text].scale += 2;
        NPC.NewNPC(source, i * 16, j * 16, NPCID.Ghost);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class Spawn_Wraith : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        NPC.NewNPC(source, i * 16, j * 16, NPCID.Wraith);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class Spawn_Tim : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        NPC.NewNPC(source, i * 16, j * 16, NPCID.Tim);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class Spawn_RuneWizard : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        NPC.NewNPC(source, i * 16, j * 16, NPCID.RuneWizard);
        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.SyncNPC);
    }
}
public class BouncyBombParty : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        for (int a = 0; a < 30; a++)
        {
            Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(30, 360, a) * 10, ProjectileID.BouncyBomb, 120, 1, Main.myPlayer);

        }
    }
}
public class GetNuked : PotType
{
    public override void OnPotDestroyed(int i, int j)
    {
        IEntitySource source = new EntitySource_TileBreak(i, j);
        for (int a = 0; a < 30; a++)
        {
            int proj = Projectile.NewProjectile(source, new Vector2(i, j) * 16, Vector2.One.Vector2DistributeEvenly(30, 360, a) * 10, ProjectileID.MiniNukeRocketII, 320, 1, Main.myPlayer);
            Main.projectile[proj].hostile = true;
            Main.projectile[proj].friendly = false;
        }
    }
}
