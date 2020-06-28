using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using StoneworkRoseCafe.NPCs;

namespace StoneworkRoseCafe.Items {
    public class letter : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Letter"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Endowed with an intricate wax seal.\nUse to open");
        }
        public override void SetDefaults() {
            item.width = 32;
            item.height = 32;
            item.maxStack = 1;
            item.value = 0;
            item.useStyle = ItemUseStyleID.HoldingOut; //spellbook-like
            item.useTime = 1;
            item.holdStyle = 1;
            item.rare = ItemRarityID.LightRed;
            item.scale = 0.45f;
        }
        public override Vector2? HoldoutOffset() {
            return new Vector2(Main.player[Main.myPlayer].direction==-1?0:-71, -20);
        }
        public override void UseStyle(Player player) {
            base.UseStyle(player);
            player.itemLocation = new Vector2(Main.player[Main.myPlayer].direction == -1 ? 0 : -71, -20);
        }
        public override bool UseItem(Player player) {
            playerMod myPlayer = player.GetModPlayer<playerMod>();
            if (!myPlayer.StoneworkUIOpen) {
                ModContent.GetInstance<StoneworkRoseCafe>().alterUI(true);
                if (Myriil.arrivesIn == -1) Myriil.arrivesIn = 3;
                myPlayer.StoneworkUIOpen = true;
                return true;
            }
            return false;
        }
    }
}
