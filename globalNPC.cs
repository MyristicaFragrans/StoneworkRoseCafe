using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StoneworkRoseCafe.Items;
using StoneworkRoseCafe.NPCs;

namespace StoneworkRoseCafe {
    class globalNPC : GlobalNPC {
		public static bool hasMail = false;
		public override void GetChat(NPC npc, ref string chat) {
			List<string> speaking = new List<string>() { chat }; /* Band-aid fix for overwriting some NPC's chat */

			if (npc.type == NPCID.DD2Bartender) {
				int cafeowner = NPC.FindFirstNPC(NPCType<NPCs.Myriil>());
				if(cafeowner>0) {
					speaking.Add(Main.npc[cafeowner].GivenName + " and I are not competition, contrary to popular belief!");
					speaking.Add("I sometimes go to " + Main.npc[cafeowner].GivenName + " for a beverage that won't make me black out.");
				}
			}
			if(npc.type == NPCID.TravellingMerchant && hasMail && !Myriil.hasEverSpawned) {
				speaking.Add($"Hey, I got some mail for... {Main.player[Main.myPlayer].name}? I have it in my shop bag.");
            }

			//return random index
			if(speaking.Count>0) {
				Random random = new Random();
				chat = speaking[random.Next(speaking.Count)];
			}
		}

        public override void SetupTravelShop(int[] shop, ref int nextSlot) {
			if (WorldGen.genRand.Next(1) == 0) {
				shop[nextSlot++] = ItemType<letter>();
				hasMail = true;
			}
			else hasMail = false;
		}
    }
}
