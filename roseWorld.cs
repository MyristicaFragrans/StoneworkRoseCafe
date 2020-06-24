using StoneworkRoseCafe.Items;
using StoneworkRoseCafe.NPCs;
using StoneworkRoseCafe.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;


namespace StoneworkRoseCafe {
    class roseWorld  : ModWorld {
		public override void Initialize() {
		}
		public override TagCompound Save() {

			return new TagCompound {
				["myriil"] = Myriil.Save()
			};
		}
		public override void Load(TagCompound tag) {
			Myriil.Load(tag.GetCompound("myriil"));
		}
		public override void PreUpdate() {
			Myriil.getPayout();
		}
	}
}
