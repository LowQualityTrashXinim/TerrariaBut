using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerrariaBut.Common
{
    public class TerrariaButConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;


        [Header("ExtraMode")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool EvenMoreAnnoying { get; set; }

        [Header("ItemSetting")]
        [DefaultValue(false)]
        public bool Disable_ItemBreak { get; set; }

        [DefaultValue(false)]
        public bool Disable_ItemMaxStack { get; set; }

        [Header("NPCSetting")]
        [DefaultValue(false)]
        public bool Disable_NPCLifeRandomize{ get; set; }

        [DefaultValue(false)]
        public bool Disable_ShopPricex5 { get; set; }

        [DefaultValue(false)]
        public bool Disable_NPCDuplicateOnHit{ get; set; }

        [DefaultValue(false)]
        public bool Disable_NPCCanRegenerate { get; set; }
        [Header("BossSetting")]
        [DefaultValue(false)]
        public bool Disable_BossDeal15PercentageDamage { get; set; }
        [Header("PlayerSetting")]
        [DefaultValue(false)]
        public bool Disable_PlayerMaxLifeReduction { get; set; }
        [DefaultValue(false)]
        public bool Disable_PlayerRandomlyTeleport { get; set; }
        [DefaultValue(false)]
        public bool Disable_PlayerDroppingItem { get; set; }
        [Header("TileSetting")]
        [DefaultValue(false)]
        public bool Disable_PotRandomize{ get; set; }
    }
}
