using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class FixForHotbarFlickerUponOpeningFancyUILoader
    {
        public static void Load() {
            Terraria.IL_Main.GUIHotbarDrawInner += Main_GUIHotbarDrawInner_FixForHotbarFlickerUponOpeningFancyUI;
        }

        private static void Main_GUIHotbarDrawInner_FixForHotbarFlickerUponOpeningFancyUI(ILContext il) {
            var c = new ILCursor(il);

            //->:if (playerInventory || player[myPlayer].ghost) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Main", "playerInventory")
                && x.Next.MatchBrtrue(out _)
                && x.Next.Next.MatchLdsfld("Terraria.Main", "player")
                && x.Next.Next.Next.MatchLdsfld("Terraria.Main", "myPlayer")
                && x.Next.Next.Next.Next.MatchLdelemRef()
                && x.Next.Next.Next.Next.Next.MatchLdfld("Terraria.Player", "ghost")
                && x.Next.Next.Next.Next.Next.Next.MatchBrfalse(out _)
            )) {
                throw new Exception.InstructionNotFound();
            }
            //--:if (playerInventory || player[myPlayer].ghost) {
            //++:if (inFancyUI || playerInventory || player[myPlayer].ghost) {
            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("inFancyUI", BindingFlags.Public | BindingFlags.Static));
            c.Emit(OpCodes.Brtrue_S, (ILLabel)c.Next.Next.Operand);
        }
    }
}