using BetterGameUI.UI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class DraggingOverBuffIconDoesntInterruptClickLoader
    {
        public static void Load() {
            Terraria.IL_Main.DrawBuffIcon += Main_DrawBuffIcon_DraggingOverBuffIconDoesntInterruptClick;
        }

        private static void Main_DrawBuffIcon_DraggingOverBuffIconDoesntInterruptClick(ILContext il) {
            var c = new ILCursor(il);

            var pushFalse = c.DefineLabel();
            var flagAssignment = c.DefineLabel();
            //->: bool flag = mouseRight && mouseRightRelease;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Main", "mouseRight")
                && x.Next.MatchBrfalse(out pushFalse)
                && x.Next.Next.MatchLdsfld("Terraria.Main", "mouseRightRelease")
                && x.Next.Next.Next.MatchBr(out flagAssignment)
                && x.Next.Next.Next.Next.MatchLdcI4(0)
                && x.Next.Next.Next.Next.Next.MatchStloc(20))) {
                throw new Exception.InstructionNotFound();
            }

            var conditional = c.MarkLabel();
            c.MoveBeforeLabels();

            //--: bool flag = Main.mouseRight && Main.mouseRightRelease;
            //++: bool flag = Main.mouseRight && Main.mouseRightRelease && (!Main.mouseLeft || CurrentMouseLeftBegunInBufffList);
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mouseLeft", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse_S, conditional);
            c.Emit(OpCodes.Ldsfld, typeof(BuffList).GetField("currentMouseLeftBegunInBufffList", BindingFlags.Static | BindingFlags.NonPublic));
            c.Emit(OpCodes.Brfalse_S, pushFalse);

            var ifNotUsingGamepad = c.DefineLabel();
            //->: if (PlayerInput.UsingGamepad) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchCall("Terraria.GameInput.PlayerInput", "get_UsingGamepad")
                && x.Next.MatchBrfalse(out ifNotUsingGamepad))) {
                throw new Exception.InstructionNotFound();
            }

            //  : if (PlayerInput.UsingGamepad) {
            //  :     {...}
            //->: } else {
            c.GotoLabel(ifNotUsingGamepad);

            //  : if (PlayerInput.UsingGamepad) {
            //  :     {...}
            //++: } else if (!Main.mouseLeft || BuffList.currentMouseLeftBegunInBufffList) {
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mouseLeft", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Brfalse_S, ifNotUsingGamepad); // Placeholder target, shortly after the cursor is returned to set the actual target
            c.Emit(OpCodes.Ldsfld, typeof(BuffList).GetField("currentMouseLeftBegunInBufffList", BindingFlags.Static | BindingFlags.NonPublic));
            c.Emit(OpCodes.Brfalse_S, ifNotUsingGamepad); // Placeholder target, shortly after the cursor is returned to set the actual target

            var insideIfNotUsingGamepad = c.MarkLabel();

            //  : } else if (!Main.mouseLeft || CurrentMouseLeftBegunInBufffList) {
            //->:     Main.player[Main.myPlayer].mouseInterface = true;
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStfld("Terraria.Player", "mouseInterface")
                && x.Previous.MatchLdcI4(1))) {
                throw new Exception.InstructionNotFound();
            }

            var afterMouseInterfaceAssignment = c.MarkLabel();

            //->: } else if (!Main.mouseLeft || BuffList.currentMouseLeftBegunInBufffList) {
            c.GotoLabel(insideIfNotUsingGamepad, MoveType.Before);

            //  : } else if (!Main.mouseLeft || BuffList.currentMouseLeftBegunInBufffList) {
            //  :     {...}
            //  :     Main.player[Main.myPlayer].mouseInterface = true;
            //++: }
            c.Previous.Operand = afterMouseInterfaceAssignment;
            c.Previous.Previous.Previous.Operand = insideIfNotUsingGamepad;
        }
    }
}