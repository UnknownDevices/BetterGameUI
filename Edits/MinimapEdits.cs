using IL.Terraria;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils.Cil;
using System;
using System.Reflection;

namespace BetterGameUI.Edits
{
    public static class MinimapEdits
    {
        public static void Load()
        {
            try
            {
                IL.Terraria.GameContent.UI.Minimap.MinimapFrame.Update += MinimapFrame_Update;
            }
            catch (TargetInvocationException e)
            {
                throw new Exception.LoadingMinimapEdits(e);
            }
            catch (System.Exception e)
            {
                throw new Exception.LoadingMinimapEdits(e);
            }
        }

        private static void MinimapFrame_Update(ILContext il)
        {
            var c = new ILCursor(il);

            // ->: Button buttonUnderMouse = GetButtonUnderMouse();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStloc(0) &&
                x.Previous != null &&
                x.Previous.MatchCall("Terraria.GameContent.UI.Minimap.MinimapFrame", "GetButtonUnderMouse")))
            {
                throw new Exception.InstructionNotFound();
            }

            var label = c.MarkLabel();

            c.GotoLabel(label, MoveType.Before);

            //   : Button buttonUnderMouse = GetButtonUnderMouse();
            // ++: if (Terraria.Main.LocalPlayer.hbLocked && !Terraria.Main.playerInventory) {
            // ++:     buttonUnderMouse = null;
            // ++: }
            // TODO: emit individual instruccions instead of delegate
            c.EmitDelegate(() =>
                Mod.ClientConfig.Minimap_LockWhenHotbarIsLocked &&
                Terraria.Main.LocalPlayer.hbLocked &&
                !Terraria.Main.playerInventory);
            c.Emit(OpCodes.Brfalse, label);
            c.Emit(OpCodes.Ldc_I4, 0);
            c.Emit(OpCodes.Stloc_0);
        }
    }
}
