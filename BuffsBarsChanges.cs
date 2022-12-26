using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI
{
    public static class BuffsBarsChanges
    {
        public static void Load() {
            try {
                IL.Terraria.Main.DrawInventory += IL_Main_DrawInventory;
            }
            catch (System.Reflection.TargetInvocationException e) {
                throw new BetterGameUI.Exception.LoadBuffsBarsChanges(e);
            }
            catch (BetterGameUI.Exception.FailToFindInstruction e) {
                throw new BetterGameUI.Exception.LoadBuffsBarsChanges(e);
            }
        }

        public static void IL_Main_DrawInventory(ILContext il) {
            var c = new ILCursor(il);

            var mouseTextHackZoomInfo = typeof(Main).GetMethod("MouseTextHackZoom",
                    BindingFlags.Public | BindingFlags.Instance,
                    new[] { typeof(string), typeof(int), typeof(byte), typeof(string) });

            if (!c.TryGotoNext(MoveType.After, x => x.MatchCall(mouseTextHackZoomInfo))) {
                throw new BetterGameUI.Exception.FailToFindInstruction();
            }

            var afterVanillaBuffsBarDrawLabel = c.MarkLabel();

            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdloc(55) &&
                x.Next.MatchLdcI4(247) &&
                x.Next.Next.MatchAdd())) {
                throw new BetterGameUI.Exception.FailToFindInstruction();
            }

            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static));
            c.Emit(OpCodes.Call, typeof(UI.UISystem).GetMethod("Draw_InventoryBuffsBar"));
            c.Emit(OpCodes.Br, afterVanillaBuffsBarDrawLabel);
        }
    }
}
