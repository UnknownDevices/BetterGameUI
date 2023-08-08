using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class DraggingOverHotbarSlotDoesntSelectItemLoader
    {
        public static void Load() {
            Terraria.IL_Main.GUIHotbarDrawInner += Main_GUIHotbarDrawInner_DraggingOverHotbarSlotDoesntSelectItem;
        }

        private static void Main_GUIHotbarDrawInner_DraggingOverHotbarSlotDoesntSelectItem(ILContext il) {
            var c = new ILCursor(il);

            //->:if (mouseLeft && !player[myPlayer].hbLocked && !blockMouse) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Main", "mouseLeft")
                && x.Next.MatchBrfalse(out _)
                && x.Next.Next.MatchLdsfld("Terraria.Main", "player")
                && x.Next.Next.Next.MatchLdsfld("Terraria.Main", "myPlayer")
                && x.Next.Next.Next.Next.MatchLdelemRef()
                && x.Next.Next.Next.Next.Next.MatchLdfld("Terraria.Player", "hbLocked")
                && x.Next.Next.Next.Next.Next.Next.MatchBrtrue(out _)
                && x.Next.Next.Next.Next.Next.Next.Next.MatchLdsfld("Terraria.Main", "blockMouse")
                && x.Next.Next.Next.Next.Next.Next.Next.Next.MatchBrtrue(out _)
            )) {
                throw new Exception.InstructionNotFound();
            }
            //--:if (mouseLeft && !player[myPlayer].hbLocked && !blockMouse) {
            //++:if (mouseLeftRelease && mouseLeft && !player[myPlayer].hbLocked && !blockMouse) {
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mouseLeftRelease", BindingFlags.Public | BindingFlags.Static));
            c.Emit(OpCodes.Brfalse, (ILLabel)c.Next.Next.Operand);
        }
    }
}