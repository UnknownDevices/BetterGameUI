using BetterGameUI.UI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils.Cil;
using System.Reflection;
using Terraria;

namespace BetterGameUI.Edits
{
    public static class BuffListsEdits
    {
        public static HotbarBuffList HotbarBuffList;
        public static EquipPageBuffList EquipPageBuffList;

        public static void Load()
        {
            try
            {
                HotbarBuffList = new HotbarBuffList();
                EquipPageBuffList = new EquipPageBuffList();

                IL.Terraria.Main.DrawInventory += IL_Main_DrawInventory;
                IL.Terraria.Main.DrawInterface_27_Inventory += IL_Main_DrawInterface_27_Inventory;
                IL.Terraria.Main.GUIBarsDrawInner += IL_Main_GUIBarsDrawInner;
            }
            catch (TargetInvocationException e)
            {
                throw new Exception.FailedToLoadBuffListsEdits(e);
            }
            catch (System.Exception e)
            {
                throw new Exception.FailedToLoadBuffListsEdits(e);
            }
        }

        private static void IL_Main_GUIBarsDrawInner(ILContext il) {
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
            c.Emit(OpCodes.Ldsfld, typeof(BuffListsEdits)
                .GetField("HotbarBuffList", BindingFlags.Public | BindingFlags.Static));
            c.Emit(OpCodes.Call, typeof(HotbarBuffList)
                .GetMethod("Update", BindingFlags.Public | BindingFlags.Instance));
        }

        private static void IL_Main_DrawInterface_27_Inventory(ILContext il) {
            var c = new ILCursor(il);
            var updateEquipPageBuffList = () => {
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

        public static void IL_Main_DrawInventory(ILContext il)
        {
            var c = new ILCursor(il);

            // TODO: remove range of instructions instead
            if (!c.TryGotoNext(MoveType.After, 
                x => x.MatchCall(typeof(Main).GetMethod(
                    "MouseTextHackZoom", BindingFlags.Public | BindingFlags.Instance,
                    new[] { typeof(string), typeof(int), typeof(byte), typeof(string) }))
            )) {
                throw new Exception.InstructionNotFound();
            }
            ILLabel label = c.MarkLabel();

            //  :num24 += 247;
            //->:num23 += 8;
            if (!c.TryGotoPrev(MoveType.After,
                x => x.MatchStloc(54)
                && x.Previous.MatchAdd()
                && x.Previous.Previous.MatchLdcI4(out _)
                && x.Previous.Previous.Previous.MatchLdloc(54)
                && x.Previous.Previous.Previous.Previous.MatchStloc(55)
                && x.Previous.Previous.Previous.Previous.Previous.MatchAdd()
                && x.Previous.Previous.Previous.Previous.Previous.Previous.MatchLdcI4(out _)
                && x.Previous.Previous.Previous.Previous.Previous.Previous.Previous.MatchLdloc(55)
            )) {
                throw new Exception.InstructionNotFound();
            }

            c.Emit(OpCodes.Ldloc, 54);
            c.Emit(OpCodes.Ldloc, 55);
            c.EmitDelegate((int num23, int num24) => {
                EquipPageBuffList.Dimensions.X = num23 -
                    ((BuffList.IconWidth + BuffList.IconToIconPad) * (EquipPageBuffList.ColsCount - 1));
                EquipPageBuffList.Dimensions.Y = num24;
            });
            c.Emit(OpCodes.Br, label);
        }
    }
}
