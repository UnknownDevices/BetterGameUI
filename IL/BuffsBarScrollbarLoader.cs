using BetterGameUI.UI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class BuffsBarScrollbarLoader
    {
        public static HotbarBuffList HotbarBuffList;
        public static EquipPageBuffList EquipPageBuffList;

        public static void Load() {
            HotbarBuffList = new HotbarBuffList();
            EquipPageBuffList = new EquipPageBuffList();

            Terraria.IL_Main.DrawInventory += Main_DrawInventory_BuffListScrollbar;
            Terraria.IL_Main.DrawInterface_27_Inventory += Main_DrawInterface_27_Inventory_BuffListScrollbar;
            Terraria.IL_Main.GUIBarsDrawInner += Main_GUIBarsDrawInner_BuffListScrollbar;
        }

        private static void Main_GUIBarsDrawInner_BuffListScrollbar(ILContext il) {
            var c = new ILCursor(il);

            //  :if (!ingameOptionsWindow && !playerInventory && !inFancyUI) {
            //->:    DrawInterface_Resources_Buffs();
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchCall("Terraria.Main", "DrawInterface_Resources_Buffs")
            )) {
                throw new Exception.InstructionNotFound();
            }

            //  :if (!ingameOptionsWindow && !playerInventory && !inFancyUI) {
            //--:    DrawInterface_Resources_Buffs();
            c.RemoveRange(2);

            //->:if (!ingameOptionsWindow && !playerInventory && !inFancyUI) {
            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Main", "ingameOptionsWindow")
                && x.Next.MatchBrtrue(out _)
                && x.Next.Next.MatchLdsfld("Terraria.Main", "playerInventory")
                && x.Next.Next.Next.MatchBrtrue(out _)
                && x.Next.Next.Next.Next.MatchLdsfld("Terraria.Main", "inFancyUI")
                && x.Next.Next.Next.Next.Next.MatchBrtrue(out _)
            )) {
                throw new Exception.InstructionNotFound();
            }

            //++:BetterGameUI.BuffListsEdits.HobarBuffList.Update();
            //  :if (!ingameOptionsWindow && !playerInventory && !inFancyUI) {
            c.Emit(OpCodes.Ldsfld, typeof(BetterGameUI.UISystem)
                .GetField("HotbarBuffList", BindingFlags.Public | BindingFlags.Static));
            c.Emit(OpCodes.Call, typeof(HotbarBuffList)
                .GetMethod("Update", BindingFlags.Public | BindingFlags.Instance));
        }

        private static void Main_DrawInterface_27_Inventory_BuffListScrollbar(ILContext il) {
            var c = new ILCursor(il);

            var updateEquipPageBuffList = () =>
            {
                if (Main.ignoreErrors) {
                    try {
                        EquipPageBuffList.Update();
                    }
                    catch (System.Exception e) {
                        TimeLogger.DrawException(e);
                    }
                }
                else {
                    EquipPageBuffList.Update();
                }
            };

            if (!c.TryGotoNext(MoveType.Before, x => x.MatchRet())) {
                throw new Exception.InstructionNotFound();
            }

            if (!c.TryGotoNext(MoveType.Before, x => x.MatchRet())) {
                throw new Exception.InstructionNotFound();
            }
            c.MoveAfterLabels();

            c.EmitDelegate(updateEquipPageBuffList);

            c.GotoPrev(MoveType.Before, x => x.MatchRet());
            c.MoveAfterLabels();

            c.EmitDelegate(updateEquipPageBuffList);
        }

        public static void Main_DrawInventory_BuffListScrollbar(ILContext il) {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall(typeof(Main).GetMethod(
                    "MouseTextHackZoom", BindingFlags.Public | BindingFlags.Instance,
                    new[] { typeof(string), typeof(int), typeof(byte), typeof(string) }))
            )) {
                throw new Exception.InstructionNotFound();
            }

            var label = c.MarkLabel();

            //  :num24 += 247;
            //->:num23 += 8;
            if (!c.TryGotoPrev(MoveType.After,
                x => x.MatchStloc(52)
                && x.Previous.MatchAdd()
                && x.Previous.Previous.MatchLdcI4(out _)
                && x.Previous.Previous.Previous.MatchLdloc(52)
                && x.Previous.Previous.Previous.Previous.MatchStloc(53)
                && x.Previous.Previous.Previous.Previous.Previous.MatchAdd()
                && x.Previous.Previous.Previous.Previous.Previous.Previous.MatchLdcI4(out _)
                && x.Previous.Previous.Previous.Previous.Previous.Previous.Previous.MatchLdloc(53)
            )) {
                throw new Exception.InstructionNotFound();
            }

            c.Emit(OpCodes.Ldloc, 52);
            c.Emit(OpCodes.Ldloc, 53);
            c.EmitDelegate((int num23, int num24) =>
            {
                EquipPageBuffList.Dimensions.X = num23 -
                    ((BuffList.IconWidth + BuffList.IconToIconPad) * (EquipPageBuffList.ColsCount - 1));
                EquipPageBuffList.Dimensions.Y = num24;
            });
            c.Emit(OpCodes.Br, label);
        }
    }
}