using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI
{
    public static class BuffsBarsChanges
    {
        public static void Load() {
            IL.Terraria.Main.DrawInventory += IL_Main_DrawInventory;
        }

        public static void IL_Main_DrawInventory(ILContext il) {
            var c = new ILCursor(il);

            var mouseTextHackZoomInfo = typeof(Main).GetMethod("MouseTextHackZoom",
                    BindingFlags.Public | BindingFlags.Instance,
                    new[] { typeof(string), typeof(int), typeof(byte), typeof(string) });

            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall(mouseTextHackZoomInfo))) {
                return;
            }

            var afterVanillaBuffsBarDrawLabel = c.MarkLabel();

            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdloc(55) &&
                x.Next.MatchLdcI4(247) &&
                x.Next.Next.MatchAdd())) {
                return;
            }

            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static));
            c.Emit(OpCodes.Call, typeof(UI.UISystem).GetMethod("Draw_InventoryBuffsBar"));
            c.Emit(OpCodes.Br, afterVanillaBuffsBarDrawLabel);
        }
    }
}
