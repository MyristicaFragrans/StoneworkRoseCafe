using StoneworkRoseCafe.NPCs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StoneworkRoseCafe {
    class roseWorld  : ModWorld {

		public static List<string> beforeCafeArrival;

		public static void beforeCafe(string uuid) {
			if(!Myriil.hasEverSpawned && !beforeCafeArrival.Contains(uuid)) {
				beforeCafeArrival.Add(uuid);
            }
        }

		public override void Initialize() {
			Myriil.Initialize();
		}
		public override TagCompound Save() {

			return new TagCompound {
				["myriil"] = Myriil.Save(),
				["beforeCafeArrival"] = beforeCafeArrival
			};
		}
		public override void Load(TagCompound tag) {
			Myriil.Load(tag.GetCompound("myriil"));
			beforeCafeArrival = tag.Get<List<string>>("beforeCafeArrival");
		}
		public override void PreUpdate() {
			Myriil.getPayout();
			Myriil.update();
		}
	}
}
