using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;

namespace BetterGameUI.IL
{
    public class AccessorySlotsImprovedScrollbarLoader
    {
        public static void Load() {
            Terraria.IL_Main.DrawInventory += Main_DrawInventory_AccessorySlotsImprovedScrollbar;
        }

        public static void Main_DrawInventory_AccessorySlotsImprovedScrollbar(ILContext il) {
            var c = new ILCursor(il);

            // ->: LoaderManager.Get<AccessorySlotLoader>().DrawAccSlots(num20);
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall("Terraria.ModLoader.LoaderManager", "Get") &&
                x.Next.MatchLdloc(10) &&
                x.Next.Next.MatchCallvirt("Terraria.ModLoader.AccessorySlotLoader", "DrawAccSlots"))) {
                throw new Exception.InstructionNotFound();
            }

            // --: LoaderManager.Get<AccessorySlotLoader>().DrawAccSlots(num20);
            // ++: BetterGameUI.AccessorySlotLoaderEdits.DrawAccSlots(LoaderManager.Get<AccessorySlotLoader>(), num20);
            c.Next.Next.OpCode = OpCodes.Call;
            c.Next.Next.Operand = il.Import(typeof(BetterGameUI.UISystem).GetMethod("DrawAccSlots", BindingFlags.Static | BindingFlags.Public));
        }
    }
}