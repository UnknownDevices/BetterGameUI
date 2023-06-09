using BetterGameUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;
using Terraria.GameContent;

namespace BetterGameUI.IL
{
    public class LockingHotbarLocksAdditionalUIsLoader
    {
        public static void Load() {
            try {
                global::IL.Terraria.Main.DrawBuffIcon += Main_DrawBuffIcon_LockingHotbarLocksMoreUIs;
                global::IL.Terraria.Main.DrawHotbarLockIcon += Main_DrawHotbarLockIcon_LockingHotbarLocksAdditionalUIs;
                global::IL.Terraria.Main.GUIHotbarDrawInner += Main_GUIHotbarDrawInner_LockingHotbarLocksAdditionalUIs;
                global::IL.Terraria.GameContent.UI.Minimap.MinimapFrame.Update +=
                    MinimapFrame_Update_LockingHotbarLocksAdditionalUIs;
            }
            catch (System.Exception e) {
                throw new Exception.FailedToLoadFeature("Mods.BetterGameUI.Config.Label.Feature_LockingHotbarLocksAdditionalUIs", e);
            }
        }

        private static void Main_DrawBuffIcon_LockingHotbarLocksMoreUIs(ILContext il) {
            var c = new ILCursor(il);

            var currentMouseLeftBegunInBufffList = typeof(BuffList).GetField("currentMouseLeftBegunInBufffList", BindingFlags.Static | BindingFlags.NonPublic);

            var elseBlock = c.DefineLabel();
            //->: if (mouseRectangle.Contains(new Point(mouseX, mouseY)) {
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchBrfalse(out elseBlock)
                && x.Previous.MatchCall("Microsoft.Xna.Framework.Rectangle", "Contains")
                && x.Previous.Previous.MatchNewobj("Microsoft.Xna.Framework.Point")
                && x.Previous.Previous.Previous.MatchLdsfld("Terraria.Main", "mouseY")
                && x.Previous.Previous.Previous.Previous.MatchLdsfld("Terraria.Main", "mouseX"))) {
                throw new Exception.InstructionNotFound();
            }

            var insideIfBlock = c.MarkLabel();
            c.MoveBeforeLabels();

            //--: if (mouseRectangle.Contains(new Point(mouseX, mouseY))) {
            //++: if (mouseRectangle.Contains(new Point(mouseX, mouseY)) && (!Main.LocalPlayer.hbLocked || Main.playerInventory)) {
            c.Emit(OpCodes.Call, typeof(Main).GetMethod("get_LocalPlayer", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Ldfld, typeof(Terraria.Player).GetField("hbLocked", BindingFlags.Instance | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse, insideIfBlock);
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("playerInventory", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse, elseBlock);

            c.MoveAfterLabels();

            //  : if (mouseRectangle.Contains(new Point(mouseX, mouseY))) {
            //++:     if (Main.mouseLeft && Main.mouseLeftRelease) {
            //++:         CurrentMouseLeftBegunInBufffList = true;
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mouseLeft", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse_S, elseBlock); // Placeholder target, shortly after the cursor is returned to set the actual target
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mouseLeftRelease", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse_S, elseBlock); // Placeholder target, shortly after the cursor is returned to set the actual target
            c.Emit(OpCodes.Ldc_I4_1);
            c.Emit(OpCodes.Stsfld, currentMouseLeftBegunInBufffList);

            var afterCurrentMouseLeftBegunInBufffListAssignment = c.MarkLabel();

            //  : if (Main.mouseLeft && Main.mouseLeftRelease) {
            //  :     CurrentMouseLeftBegunInBufffList = true;
            //++: }
            c.Previous.Previous.Previous.Operand = afterCurrentMouseLeftBegunInBufffListAssignment;
            c.Previous.Previous.Previous.Previous.Previous.Operand = afterCurrentMouseLeftBegunInBufffListAssignment;
        }

        private static void Main_DrawHotbarLockIcon_LockingHotbarLocksAdditionalUIs(ILContext il) {
            var c = new ILCursor(il);

            // ->: MouseText(Lang.inter[5].Value, 0, 0);
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Lang", "inter") &&
                x.Next.MatchLdcI4(5) &&
                x.Next.Next.MatchLdelemRef() &&
                x.Next.Next.Next.MatchCallvirt("Terraria.Localization.LocalizedText", "get_Value"))) {
                throw new Exception.InstructionNotFound();
            }

            // --: MouseText(Lang.inter[5].Value, 0, 0);
            // ++: MouseText(Terraria.Localization.Language.GetTextValue("Mods.BetterGameUI.UI.Tooltip.UIUnlocked"), 0, 0);
            c.RemoveRange(4);
            c.Emit(OpCodes.Ldstr, "Mods.BetterGameUI.UI.Tooltip.UIUnlocked");
            c.Emit(OpCodes.Call, typeof(Terraria.Localization.Language)
                .GetMethod("GetTextValue", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string) }));

            // ->: MouseText(Lang.inter[6].Value, 0, 0);
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Lang", "inter") &&
                x.Next.MatchLdcI4(6) &&
                x.Next.Next.MatchLdelemRef() &&
                x.Next.Next.Next.MatchCallvirt("Terraria.Localization.LocalizedText", "get_Value"))) {
                throw new Exception.InstructionNotFound();
            }

            // --: MouseText(Lang.inter[6].Value, 0, 0);
            // ++: MouseText(Terraria.Localization.Language.GetTextValue("Mods.BetterGameUI.UI.Tooltip.UILocked"), 0, 0);
            c.RemoveRange(4);
            c.Emit(OpCodes.Ldstr, "Mods.BetterGameUI.UI.Tooltip.UILocked");
            c.Emit(OpCodes.Call, typeof(Terraria.Localization.Language)
                .GetMethod("GetTextValue", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string) }));
        }

        private static void Main_GUIHotbarDrawInner_LockingHotbarLocksAdditionalUIs(ILContext il) {
            var c = new ILCursor(il);

            //->: string text = Lang.inter[37].Value;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Lang", "inter")
                && x.Next.MatchLdcI4(37)
                && x.Next.Next.MatchLdelemRef()
                && x.Next.Next.Next.MatchCallvirt("Terraria.Localization.LocalizedText", "get_Value")
            )) {
                throw new Exception.InstructionNotFound();
            }
            c.MoveAfterLabels();

            //++: Texture2D texture = TextureAssets.HbLock[(!Main.player[Main.myPlayer].hbLocked) ? 1 : 0].Value;
            //++: spriteBatch.Draw(texture, new Vector2(0, 21), texture.Frame(2), Color.White,
            //  :   0f, default, 0.9f, SpriteEffects.None, 0f);
            c.EmitDelegate(() =>
            {
                Texture2D texture = TextureAssets.HbLock[(!Main.player[Main.myPlayer].hbLocked) ? 1 : 0].Value;
                Main.spriteBatch.Draw(texture, new Vector2(0, 21), texture.Frame(2), Color.White,
                    0f, default, 0.9f, SpriteEffects.None, 0f);
            });
        }

        private static void MinimapFrame_Update_LockingHotbarLocksAdditionalUIs(ILContext il) {
            var c = new ILCursor(il);

            // ->: Button buttonUnderMouse = GetButtonUnderMouse();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStloc(0) &&
                x.Previous != null &&
                x.Previous.MatchCall("Terraria.GameContent.UI.Minimap.MinimapFrame", "GetButtonUnderMouse"))) {
                throw new Exception.InstructionNotFound();
            }

            var afterButtonUnderMouseAssignment = c.MarkLabel();

            c.MoveBeforeLabels();

            // TODO: emit individual instructions
            //   : Button buttonUnderMouse = GetButtonUnderMouse();
            // ++: if (Terraria.Main.LocalPlayer.hbLocked && !Terraria.Main.playerInventory) {
            // ++:     buttonUnderMouse = null;
            // ++: }
            c.EmitDelegate(() =>
                Terraria.Main.LocalPlayer.hbLocked &&
                !Terraria.Main.playerInventory);
            c.Emit(OpCodes.Brfalse, afterButtonUnderMouseAssignment);
            c.Emit(OpCodes.Ldc_I4, 0);
            c.Emit(OpCodes.Stloc_0);
        }
    }
}