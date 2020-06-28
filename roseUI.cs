using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneworkRoseCafe.NPCs;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StoneworkRoseCafe {
    class roseUI : UIState {
        public Player myPlayer;
        public string worldName;
        public override void OnInitialize() {
            Texture2D paper = GetTexture("StoneworkRoseCafe/UI/paper");
            UIImage panel = new UIImage(paper);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            panel.SetPadding(50);
            Append(panel);

            Player myPlayer = Main.player[Main.myPlayer];
            string letter = $"Dearest Mayor {myPlayer.name}, \nI have developed a driving interest in relocating my beloved cafe, the " +
                $"Stonework Rose, to what I have come to learn is the best village in {worldName}.\n\n " +
                $"As such, I would like to request a building, made from either Dynasty Wood, or " +
                $"the wood I know to be native to {worldName}, Boreal Wood. There is no need to write me back as I will be arriving in " +
                $"three day's time.\n\n As part of my arrival, I am willing to pay one platinum coin to each of the rulers over {worldName}. " +
                $"I look forward to seeing you!\n\n Yours Sincerely, {Myriil.myName}\n";
            //string letter = $"Hello, {myPlayer.name}!";
            UIText text = WriteParagraph(letter, paper.Width - 100);

            panel.Append(text);

            panel.OnClick += OnClickClose;
            panel.OnRightClick += OnClickClose;
            OnClick += OnClickClose; //assign to global as well
            OnRightClick += OnClickClose;
            playerMod player = myPlayer.GetModPlayer<playerMod>();
            player.StoneworkUIOpen = true;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

        }

        private UIText WriteParagraph(string text, int maxwidth) {
            UIText ui = new UIText("");
            ui.SetText(text);
            List<string> toAdd = new List<string>(text.Split(' '));
            List<List<string>> words = new List<List<string>>();

            //Rectangle boundry = ui.GetClippingRectangle(Main.spriteBatch);
            while (toAdd.Count != 0) {
                words.Add(new List<string>());
                bool cont = true;
                while (cont && toAdd.Count != 0) {
                    words[words.Count-1].Add(toAdd[0]);
                    ui.SetText(words[words.Count - 1].Aggregate((i, j) => i + ' ' + j));
                    ui.Recalculate();
                    if(ui.MinWidth.Pixels > maxwidth && words[words.Count - 1].Any()) {
                        words[words.Count - 1].RemoveAt(words[words.Count - 1].Count - 1);
                        cont = false;
                    } else {
                        toAdd.RemoveAt(0);
                    }
                }
            }
            //reconstruct UI from words
            string construct = "";
            for(int i = 0; i < words.Count; i++) {
                construct += words[i].Aggregate((j, k) => j + ' ' + k);
                construct += "\n";
            }
            ui.SetText(construct);
            return ui;
        }

        private void OnClickClose(UIMouseEvent evt, UIElement listeningElement) {
            ModContent.GetInstance<StoneworkRoseCafe>().alterUI(false);
            playerMod player = myPlayer.GetModPlayer<playerMod>();
            player.StoneworkUIOpen = false;
        }
    }
}
