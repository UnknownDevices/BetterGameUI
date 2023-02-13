using BetterGameUI.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Mono.Cecil.Cil;

namespace BetterGameUI
{
    public class AccessorySlotsEdits
    {
        public static void Load() {
            try {
                IL.Terraria.Main.DrawInventory +=
                    IL_Main_DrawInventory;
            }
            catch (System.Reflection.TargetInvocationException e) {
                throw new BetterGameUI.Exception.LoadingAccessorySlotsEdits(e);
            }
            catch (BetterGameUI.Exception.InstructionNotFound e) {
                throw new BetterGameUI.Exception.LoadingAccessorySlotsEdits(e);
            }
        }

        public static void IL_Main_DrawInventory(ILContext il) {
            var c = new ILCursor(il);

            // ->: LoaderManager.Get<AccessorySlotLoader>().DrawAccSlots(num20);
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall("Terraria.ModLoader.LoaderManager", "Get") &&
                x.Next.MatchLdloc(10) &&
                x.Next.Next.MatchCallvirt("Terraria.ModLoader.AccessorySlotLoader", "DrawAccSlots"))) {
                throw new BetterGameUI.Exception.InstructionNotFound();
            }

            // --: LoaderManager.Get<AccessorySlotLoader>().DrawAccSlots(num20);
            // ++: BetterGameUI.AccessorySlotLoaderEdits.DrawAccSlots(LoaderManager.Get<AccessorySlotLoader>(), num20);
            c.Next.Next.OpCode = OpCodes.Call;
            c.Next.Next.Operand = typeof(AccessorySlotsEdits).GetMethod("DrawAccSlots");
        }

        public static void DrawAccSlots(AccessorySlotLoader self, int num20) {
            int skip = 0;
            AccessorySlotLoaderReflection.SetDrawVerticalAlignment(num20);
            Color color = Main.inventoryBack;

            for (int vanillaSlot = 3; vanillaSlot < Main.LocalPlayer.dye.Length; vanillaSlot++) {
                if (!self.Draw(skip, false, vanillaSlot, color)) {
                    skip++;
                }
            }

            for (int modSlot = 0; modSlot < AccessorySlotLoaderReflection.GetList(self).Count; modSlot++) {
                if (!self.Draw(skip, true, modSlot, color))
                    skip++;
            }

            // there are no slots to be drawn by us.
            if (skip == AccessorySlotLoader.MaxVanillaSlotCount + AccessorySlotLoaderReflection.GetList(self).Count) {
                ModAccessorySlotPlayerReflection.SetScrollbarSlotPosition(
                    AccessorySlotLoaderReflection.ModSlotPlayer(Main.LocalPlayer), 0);
                return;
            }

            int accessoryPerColumn = AccessorySlotLoaderReflection.GetAccessorySlotPerColumn(self);
            int slotsToRender = AccessorySlotLoaderReflection.GetList(self).Count + AccessorySlotLoader.MaxVanillaSlotCount - skip;
            int scrollIncrement = slotsToRender - accessoryPerColumn;

            if (scrollIncrement < 0) {
                accessoryPerColumn = slotsToRender;
                scrollIncrement = 0;
            }

            AccessorySlotLoaderReflection.SetDefenseIconPosition(new Vector2(Main.screenWidth - 64 - 28,
                AccessorySlotLoader.DrawVerticalAlignment + (accessoryPerColumn + 2) * 56 * Main.inventoryScale + 4));

            if (scrollIncrement > 0) {
                AccessorySlotLoaderReflection.DrawScrollSwitch(self);

                if (ModAccessorySlotPlayerReflection.GetScrollSlots(
                    AccessorySlotLoaderReflection.ModSlotPlayer(Main.LocalPlayer))) {
                    DrawScrollbar(accessoryPerColumn, slotsToRender, scrollIncrement);
                }
            }
            else {
                ModAccessorySlotPlayerReflection.SetScrollbarSlotPosition(
                    AccessorySlotLoaderReflection.ModSlotPlayer(Main.LocalPlayer), 0);
            }
        }

        public static void DrawScrollbar(int accessoryPerColumn, int slotsToRender, int scrollIncrement) {
            int xLoc = Main.screenWidth - 64 - 28;

            int chkMax = (int)((float)(AccessorySlotLoaderReflection.GetDrawVerticalAlignment()) +
                (float)(((accessoryPerColumn) + 3) * 56) * Main.inventoryScale) + 4;
            int chkMin = (int)((float)(AccessorySlotLoaderReflection.GetDrawVerticalAlignment()) + (float)((0 + 3) * 56) * Main.inventoryScale) + 4;

            UIScrollbar scrollbar = new UIScrollbar();

            Rectangle rectangle = new Rectangle(xLoc + 47 + 6, chkMin, 5, chkMax - chkMin);
            UIScrollbarReflection.DrawBar(scrollbar, Main.spriteBatch,
                Main.Assets.Request<Texture2D>("Images/UI/Scrollbar").Value, rectangle, Color.White);

            int barSize = (chkMax - chkMin) / (scrollIncrement + 1);
            rectangle = new Rectangle(xLoc + 47 + 5, chkMin +
                ModAccessorySlotPlayerReflection.GetScrollbarSlotPosition(AccessorySlotLoaderReflection.ModSlotPlayer
                (Main.LocalPlayer)) * barSize, 3, barSize);
            UIScrollbarReflection.DrawBar(scrollbar, Main.spriteBatch, Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner").Value, rectangle, Color.White);

            rectangle = new Rectangle(xLoc - 47 * 2, chkMin, 47 * 3, chkMax - chkMin);
            if (!(rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)) {
                return;
            }

            PlayerInput.LockVanillaMouseScroll("ModLoader/Acc");

            int scrollDelta = ModAccessorySlotPlayerReflection.GetScrollbarSlotPosition(AccessorySlotLoaderReflection.ModSlotPlayer
                (Main.LocalPlayer));
            scrollDelta += Mod.ClientConfig.AccessorySlots_InvertMouseScrollForScrollbar ?
                (int)PlayerInput.ScrollWheelDelta / 120 :
                -(int)PlayerInput.ScrollWheelDelta / 120;
            scrollDelta = Math.Min(scrollDelta, scrollIncrement);
            scrollDelta = Math.Max(scrollDelta, 0);

            ModAccessorySlotPlayerReflection.SetScrollbarSlotPosition(AccessorySlotLoaderReflection.ModSlotPlayer
                (Main.LocalPlayer), scrollDelta);
            PlayerInput.ScrollWheelDelta = 0;
        }
    }
}
