using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;

namespace BetterGameUI.IL
{
    public class XButtonsScrollRecipeListLoader
    {
        public static void Load() {
            try {
                global::IL.Terraria.Player.Update += Player_Update_XButtonsScrollRecipeList;
            }
            catch (System.Exception e) {
                throw new Exception.FailedToLoadFeature("Mods.BetterGameUI.Config.Label.Feature_XButtonsScrollRecipeList", e);
            }
        }

        private static void Player_Update_XButtonsScrollRecipeList(ILContext il) {
            var c = new ILCursor(il);

            //->: int num8 = GetMouseScrollDelta();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStloc(38)
                && x.Previous.MatchCall("Terraria.Player", "GetMouseScrollDelta"))) {
                throw new Exception.InstructionNotFound();
            }

            //  : int num8 = GetMouseScrollDelta();
            //++: num8 += BetterGameUI.Player.XButtons;
            c.Emit(OpCodes.Ldloc, 38);
            c.Emit(OpCodes.Call, typeof(BetterGameUI.Player).GetMethod("get_XButtonsWithCD", BindingFlags.Static | BindingFlags.Public));
            c.Emit(OpCodes.Add);
            c.Emit(OpCodes.Stloc, 38);
        }
    }
}