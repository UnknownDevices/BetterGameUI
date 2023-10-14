using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;

namespace BetterGameUI.IL
{
    public class MouseWheelButtonsScrollRecipeListLoader
    {
        public static void Load() {
            Terraria.IL_Player.Update += Player_Update_MouseWheelButtonsScrollRecipeList;
        }

        private static void Player_Update_MouseWheelButtonsScrollRecipeList(ILContext il) {
            var c = new ILCursor(il);

            //->: int num8 = GetMouseScrollDelta();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStloc(41)
                && x.Previous.MatchCall("Terraria.Player", "GetMouseScrollDelta"))) {
                throw new Exception.InstructionNotFound();
            }

            //  : int num8 = GetMouseScrollDelta();
            //++: num8 += BetterGameUI.Player.XButtons;
            c.Emit(OpCodes.Ldloc, 41);
            c.Emit(OpCodes.Call, typeof(BetterGameUI.Player).GetMethod("get_XButtonsWithCD", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Add);
            c.Emit(OpCodes.Stloc, 41);
        }
    }
}