using IL.Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using static Terraria.Main;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: scroll into minimap to zoom in and out
        // TODO: work on features and tweaks list for mod's description
        // TODO: allow scrolling full screen map while using an item
        // TODO: play sound when auto use item changes
        // TODO: signal auto select being active through some other, unique way
        // TODO: consider crafting item clicked on crafting window
        // TODO: consider displaying item's name on top of hotbar even while the inventory is up
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        // TODO: look into fasterUIs mod in steam
        // TODO: visually signal somehow when the hotbar is locked without needing to open the inventory
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen - 12/22/22: can't replicate bug
        public static event Action OnClientConfigChanged;

        
        // updated every frame by DrawInterface_Logic_0
        public static List<int> ActiveBuffsIndexes { get; set; }
        public static ClientConfig ClientConfig { get; set; }
        public static ServerConfig ServerConfig { get; set; }
        public override uint ExtraPlayerBuffSlots { get => (uint)ServerConfig.ExtraPlayerBuffSlots; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

        // TODO: unload
        public override void Load() {
            On.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            //IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color1;
            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
            }
        }
        // IL_00dc: ldc.i4.s 10
        //private void ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color1(MonoMod.Cil.ILContext il) {
        //    NewText(il.Instrs[90].OpCode);
        //}

        private void ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(On.Terraria.UI.ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor) {
            UI.UISystem.DrawItemSlot(spriteBatch, inv, context, slot, position, lightColor);
        }

        public override void Unload() {
            OnClientConfigChanged = null;
            ActiveBuffsIndexes = null;
            ClientConfig = null;
            ServerConfig = null;

            BetterGameUI.Assets.Unload();
        }
    }
}