using Microsoft.Xna.Framework;
using StoneworkRoseCafe.Mugs;
using StoneworkRoseCafe.Mugs.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace StoneworkRoseCafe
{
	public class StoneworkRoseCafe : Mod
	{
		public StoneworkRoseCafe() {
		}
		private static NewMug[] mugs = new NewMug[] {
			new NewMug("Stone Mug","StoneMug"),
			new NewMug("Glazed Stone Mug","GlazedStoneMug"),
			new NewMug("Glass Mug","GlassMug"),
			new NewMug("Blue-Spruce Mug","BlueSpruceStoneMug"),
			new NewMug("Heat Treated Stone Mug","HeatTreatedStoneMug"),
			new NewMug("Ceramic Mug","CeramicMug"),
			new NewMug("Porcelain Mug","PorcelainMug"),
			new NewMug("Moziac Mug","MoziacMug"),
			new NewMug("Specked Mug","SpeckledMug"),
			new NewMug("Crimstone Mug","CrimstoneMug"),
			new NewMug("Gold-Plated Mug","GoldPlatedMug"),
			new NewMug("Calacatta Marble Mug","MarbleMug"),
			new NewMug("Obsidian Mug","ObsidianMug"),
			new NewMug("China Mug","China"),
			new NewMug("Stainless Steel Mug","StainlessSteelMug"),
			new NewMug("Beast Mug","BeastMug"),
			new NewMug("Wooden Mug","WoodenMug"),
			new NewMug("Paper Mug","PaperMug"),
		};
		public static List<int> MugIDs = new List<int>();

        internal roseUI stoneworkUI;
		internal UserInterface UIinterface;

		public void alterUI(bool show) {
			if(show) {
				UIinterface.SetState(null); //just in case
				stoneworkUI.myPlayer = Main.player[Main.myPlayer];
				stoneworkUI.worldName = Main.worldName;
				stoneworkUI.Activate();
				UIinterface.SetState(stoneworkUI);
			} else {
				stoneworkUI.Deactivate();
				UIinterface.SetState(null);
			}
		}

		public override void Load() {
			foreach (var mymug in mugs) {
				Mug item = new Mug(mymug.displayName, "StoneworkRoseCafe/Mugs/Items/" + mymug.texturePath + "Item");
				MugTile tile = new MugTile(item, "StoneworkRoseCafe/Mugs/Tiles/" + mymug.texturePath + "Tile");
				AddTile(mymug.displayName, tile, "StoneworkRoseCafe/Mugs/Tiles/" + mymug.texturePath + "Tile");
				item.tile = tile.Type;
				MugIDs.Add(tile.Type); //add so we can get all the mugs later
				AddItem(mymug.displayName, item);
				tile.drop = item.item.type;
			}

			if (!Main.dedServ) {
				UIinterface = new UserInterface();

				stoneworkUI = new roseUI();
				//.Activate() is in alterUI(bool show)
			}
		}

		public override void Unload() {
			stoneworkUI = null;
		}

		private GameTime _lastUIUpdate;

        public override void UpdateUI(GameTime gameTime) {
			_lastUIUpdate = gameTime;
			if(UIinterface?.CurrentState != null) {
				UIinterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if(mouseTextIndex !=-1) {
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
						"StoneworkRose: UIinterface",
						delegate {
							if (_lastUIUpdate != null && UIinterface?.CurrentState != null) {
								UIinterface.Draw(Main.spriteBatch, _lastUIUpdate);
							}
							return true;
						}, InterfaceScaleType.UI
					));

            }
        }
    }
}