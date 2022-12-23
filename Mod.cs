using IL.Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using static Terraria.Main;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: scrollbar alfa config
        // TODO: require to let go and hold again for weapon swap to happen
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
            if (Main.netMode != NetmodeID.Server) {
                IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color +=
                    IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
                BetterGameUI.Assets.Load();
            }
        }

        private void IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(MonoMod.Cil.ILContext il) {
            // 1554: - if (context == 0 && slot < 10 && player.selectedItem == slot) {
            // 1554: + if (context == 0 && player.selectedItem == slot) {
            il.Instrs[89] = il.IL.Create(OpCodes.Nop);
            il.Instrs[90] = il.IL.Create(OpCodes.Nop);
            il.Instrs[91] = il.IL.Create(OpCodes.Nop);


            // - 1571: else if (context == 0 && slot < 10) {
            // + 1571: else if (context == 0) {
            // + 1572:     if (slot < 10) {
            // + 1573:         value = TextureAssets.InventoryBack9.Value;
            // + 1574:     }
            il.Instrs[214] = il.IL.Create(OpCodes.Bge_S, il.Instrs[218]);
        }
        
        public override void Unload() {
            if (Main.netMode != NetmodeID.Server) {
                IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color -=
                IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            }

            OnClientConfigChanged = null;
            ActiveBuffsIndexes = null;
            ClientConfig = null;
            ServerConfig = null;

            BetterGameUI.Assets.Unload();
        }
    }
}