using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace BetterGameUI.Edits
{
    public static class ItemSlotsEdits
    {
        public static void Load()
        {
            try
            {
                IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color +=
                    IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                throw new Exception.FailedToLoadItemSlotsEdits(e);
            }
            catch (Exception.InstructionNotFound e)
            {
                throw new Exception.FailedToLoadItemSlotsEdits(e);
            }
        }

        public static void IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(ILContext il)
        {
            var c = new ILCursor(il);

            // ->: if (context == 0 && slot < [...] && player.selectedItem == slot) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(2) &&
                x.Next.MatchBrtrue(out _) &&
                x.Next.Next.MatchLdarg(3) &&
                x.Next.Next.Next.MatchLdcI4(out _) &&
                x.Next.Next.Next.Next.MatchBge(out _) &&
                x.Next.Next.Next.Next.Next.MatchLdloc(0) &&
                x.Next.Next.Next.Next.Next.Next.MatchLdfld("Terraria.Player", "selectedItem") &&
                x.Next.Next.Next.Next.Next.Next.Next.MatchLdarg(3) &&
                x.Next.Next.Next.Next.Next.Next.Next.Next.MatchBneUn(out _)))
            {
                throw new Exception.InstructionNotFound();
            }

            // --: if (context == 0 && slot < [...] && player.selectedItem == slot) {
            // ++: if (context == 0 && slot < 50 && player.selectedItem == slot) {
            c.Next.Next.Next.Next.Operand = 50;

            // ->: if (player.selectedItem == slot && highlightThingsForMouse) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdloc(0) &&
                x.Next.MatchLdfld("Terraria.Player", "selectedItem") &&
                x.Next.Next.MatchLdarg(3) &&
                x.Next.Next.Next.MatchCeq() &&
                x.Next.Next.Next.Next.MatchLdloc(10) &&
                x.Next.Next.Next.Next.Next.MatchAnd() &&
                x.Next.Next.Next.Next.Next.Next.MatchBrfalse(out _)))
            {
                throw new Exception.InstructionNotFound();
            }

            var label = c.MarkLabel();

            // ->: else if (context == 0 && slot < [...]) {
            if (!c.TryGotoPrev(MoveType.After,
                x => x.MatchLdarg(2) &&
                x.Next.MatchBrtrue(out _) &&
                x.Next.Next.MatchLdarg(3) &&
                x.Next.Next.Next.MatchLdcI4(out _) &&
                x.Next.Next.Next.Next.MatchBge(out _)))
            {
                throw new Exception.InstructionNotFound();
            }

            // --: else if (context == 0 && slot < {...}) {
            // ++: else if (context == 0) {
            // ++:     if (slot < {...}) {
            //   :         {...}
            // ++:     }
            //   :
            //   :     if (player.selectedItem == slot && highlightThingsForMouse) {
            c.Next.Next.Next.Next.Operand = label;
        }
    }
}
