using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace BetterGameUI.IL
{
    public class InvertedMouseScrollForRecipeListLoader
    {
        public static void Load() {
            try {
                global::IL.Terraria.Player.Update += Player_Update_InvertedMouseScrollForRecipeList;
            }
            catch (System.Exception e) {
                throw new Exception.FailedToLoadFeature("Mods.BetterGameUI.Config.Label.Feature_InvertedMouseScrollForRecipeList", e);
            }
        }

        private static void Player_Update_InvertedMouseScrollForRecipeList(ILContext il) {
            var c = new ILCursor(il);

            //->: int num8 = GetMouseScrollDelta();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchStloc(38)
                && x.Previous.MatchCall("Terraria.Player", "GetMouseScrollDelta"))) {
                throw new Exception.InstructionNotFound();
            }

            //  : int num8 = GetMouseScrollDelta();
            //++: num8 *= -1;
            c.Emit(OpCodes.Ldloc, 38);
            c.Emit(OpCodes.Neg);
            c.Emit(OpCodes.Stloc, 38);
        }
    }
}