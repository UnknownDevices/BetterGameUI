using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;

namespace BetterGameUI.Edits
{
    public static class ToolbarEdits
    {
        public static void Load() {
            try {
                IL.Terraria.Main.DrawHotbarLockIcon += Main_DrawHotbarLockIcon;
            }
            // TODO: proper error message
            catch (System.Reflection.TargetInvocationException e) {
                throw new Exception.LoadingToolbarEdits(e);
            }
            catch (Exception.InstructionNotFound e) {
                throw new Exception.LoadingToolbarEdits(e);
            }
        }

        public static void Main_DrawHotbarLockIcon(ILContext il) {
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
            // ++: MouseText(Terraria.Localization.Language.GetTextValue("Mods.BetterGameUI.UI.Tooltips.UIUnlocked"), 0, 0);
            c.RemoveRange(4);
            c.Emit(OpCodes.Ldstr, "Mods.BetterGameUI.UI.Tooltips.UIUnlocked");
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
            // ++: MouseText(Terraria.Localization.Language.GetTextValue("Mods.BetterGameUI.UI.Tooltips.UILocked"), 0, 0);
            c.RemoveRange(4);
            c.Emit(OpCodes.Ldstr, "Mods.BetterGameUI.UI.Tooltips.UILocked");
            c.Emit(OpCodes.Call, typeof(Terraria.Localization.Language)
                .GetMethod("GetTextValue", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string) }));
        }
    }
}
