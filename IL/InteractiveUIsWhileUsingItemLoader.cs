using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class InteractiveUIsWhileUsingItemLoader
    {
        public static void Load() {
            Terraria.IL_Main.GUIHotbarDrawInner += Main_GUIHotbarDrawInner_InteractiveUIsWhileUsingItem;
            Terraria.IL_Player.Update += Player_Update_InteractiveUIsWhileUsingItem;
            Terraria.IL_Player.ScrollHotbar += Player_ScrollHotbar_InteractiveUIsWhileUsingItem;
        }

        private static void Main_GUIHotbarDrawInner_InteractiveUIsWhileUsingItem(ILContext il) {
            var c = new ILCursor(il);

            // Replace every read of 'Main.player[Main.myPlayer].selectedItem' for
            // 'BetterGameUI.Player.PreselectedItem' up until we get to the instructions that assign
            // 'Main.player[Main.myPlayer].selectedItem' to a local variable
            var preselectedItemField = typeof(Player)
                .GetField("preselectedItem", BindingFlags.NonPublic | BindingFlags.Static);
            while (c.TryGotoNext(MoveType.Before,
                x => x.MatchLdsfld("Terraria.Main", "player")
                && x.Next.MatchLdsfld("Terraria.Main", "myPlayer")
                && x.Next.Next.MatchLdelemRef()
                && x.Next.Next.Next.MatchLdfld("Terraria.Player", "selectedItem")
                && !x.Next.Next.Next.Next.MatchStloc(2)
            ))
            {
                c.RemoveRange(4);
                c.Emit(OpCodes.Ldsfld, preselectedItemField);
            }
        }

        private static void Player_Update_InteractiveUIsWhileUsingItem(ILContext il) {
            var c = new ILCursor(il);

            //  : if (itemAnimation == 0 && ItemTimeIsZero && reuseDelay == 0) {
            //->:     dropItemCheck();
            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall("Terraria.Player", "dropItemCheck"))) {
                throw new Exception.InstructionNotFound();
            }

            var afterDropItemCheckCall = c.MarkLabel();

            //  : if (itemAnimation == 0 && ItemTimeIsZero && reuseDelay == 0) {
            //  :     dropItemCheck();
            //++: }
            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchBrtrue(out _)
                && x.Previous.MatchLdfld("Terraria.Player", "reuseDelay")
                && x.Previous.Previous.MatchLdarg(0))) {
                throw new Exception.InstructionNotFound();
            }

            c.Next.Operand = afterDropItemCheckCall;

            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchBrfalse(out _)
                && x.Previous.MatchCall("Terraria.Player", "get_ItemTimeIsZero")
                && x.Previous.Previous.MatchLdarg(0))) {
                throw new Exception.InstructionNotFound();
            }

            c.Next.Operand = afterDropItemCheckCall;

            var beforeElseBlock = c.DefineLabel();

            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchBrtrue(out beforeElseBlock)
                && x.Previous.MatchLdfld("Terraria.Player", "itemAnimation")
                && x.Previous.Previous.MatchLdarg(0))) {
                throw new Exception.InstructionNotFound();
            }

            c.Next.Operand = afterDropItemCheckCall;

            var preselectedItemField = typeof(Player)
                .GetField("preselectedItem", BindingFlags.NonPublic | BindingFlags.Static);

            //->: int num6 = selectedItem;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdfld("Terraria.Player", "selectedItem")
                && x.Next.Next.MatchStloc(32))) {
                throw new Exception.InstructionNotFound();
            }

            //--: int num6 = selectedItem;
            //++: int num6 = BetterGameUI.Player.preselectedItem;
            c.Next.Next.Operand = preselectedItemField;

            //->: if (!Main.drawingPlayerChat && selectedItem != 58 && !Main.editSign && !Main.editChest) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdfld("Terraria.Player", "selectedItem")
                && x.Next.Next.MatchLdcI4(58)
                && x.Next.Next.Next.MatchBeq(out _))) {
                throw new Exception.InstructionNotFound();
            }

            //--: if (!Main.drawingPlayerChat && selectedItem != 58 && !Main.editSign && !Main.editChest) {
            //++: if (!Main.drawingPlayerChat && !Main.editSign && !Main.editChest) {
            c.RemoveRange(4);

            for (int i = 0; i < 10; i++) {
                //->: selectedItem = 0..9;
                if (!c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdarg(0)
                    && x.Next.MatchLdcI4(i)
                    && x.Next.Next.MatchStfld("Terraria.Player", "selectedItem"))) {
                    throw new Exception.InstructionNotFound();
                }

                //--: selectedItem = 0..9;
                //++: BetterGameUI.Player.preselectedItem = 0..9;
                c.Next.Next.Next.Operand = preselectedItemField;
            }

            //  : if (selectedItem != nonTorch) {
            //->:     SoundEngine.PlaySound(12);
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdfld("Terraria.Player", "selectedItem")
                && x.Next.Next.MatchLdarg(0)
                && x.Next.Next.Next.MatchLdfld("Terraria.Player", "nonTorch")
                && x.Next.Next.Next.Next.MatchBeq(out _)
                && x.Next.Next.Next.Next.Next.MatchLdcI4(12)
                && x.Next.Next.Next.Next.Next.Next.MatchLdcI4(-1)
                && x.Next.Next.Next.Next.Next.Next.Next.MatchLdcI4(-1)
                && x.Next.Next.Next.Next.Next.Next.Next.Next.MatchLdcI4(1)
                && x.Next.Next.Next.Next.Next.Next.Next.Next.Next.MatchLdcR4(1)
                && x.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.MatchLdcR4(0)
                && x.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.MatchCall("Terraria.Audio.SoundEngine", "PlaySound")
                && x.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.MatchPop())) {
                throw new Exception.InstructionNotFound();
            }

            //  : if (selectedItem != nonTorch) {
            //--:     SoundEngine.PlaySound(12);
            c.RemoveRange(13);

            //->: nonTorch = selectedItem;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdarg(0)
                && x.Next.Next.MatchLdfld("Terraria.Player", "selectedItem")
                && x.Next.Next.Next.MatchStfld("Terraria.Player", "nonTorch"))) {
                throw new Exception.InstructionNotFound();
            }

            //--: nonTorch = selectedItem;
            //++: nonTorch = BetterGameUI.Player.preselectedItem;
            c.RemoveRange(4);

            //->: selectedItem = num6;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdloc(32)
                && x.Next.Next.MatchStfld("Terraria.Player", "selectedItem"))) {
                throw new Exception.InstructionNotFound();
            }

            //--: selectedItem = num6;
            c.RemoveRange(3);

            //->: flag7 = false;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdcI4(0)
                && x.Next.MatchStloc(33))) {
                throw new Exception.InstructionNotFound();
            }

            //--: flag7 = false;
            c.RemoveRange(2);

            //->: if (num6 != selectedItem) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdloc(32)
                && x.Next.MatchLdarg(0)
                && x.Next.Next.MatchLdfld("Terraria.Player", "selectedItem"))) {
                throw new Exception.InstructionNotFound();
            }

            //--: if (num6 != selectedItem) {
            //++: if (num6 != BetterGameUI.Player.preselectedItem) {
            c.Goto(c.Next.Next.Next);
            c.Next.Operand = preselectedItemField;
             
            // ->: if (stoned != lastStoned) {
            //   :     if (whoAmI == Main.myPlayer && stoned) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdfld("Terraria.Player", "stoned")
                && x.Next.Next.MatchLdarg(0)
                && x.Next.Next.Next.MatchLdfld("Terraria.Player", "lastStoned")
                && x.Next.Next.Next.Next.MatchBeq(out _)
            )) {
                throw new Exception.InstructionNotFound();
            }

            c.MoveAfterLabels();
            var beforeStonedCheck = c.MarkLabel();

            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate((Terraria.Player player) =>
            {
                if (player.controlUseItem) {
                    if (player.ItemAnimationJustStarted) {
                        Player.HasControlUseItemStoppedSinceItemAnimationStarted = false;
                    }
                }
                else {
                    Player.HasControlUseItemStoppedSinceItemAnimationStarted = true;
                }

                if (player.controlTorch) {
                    player.nonTorch = Player.PreselectedItem;
                }

                if (player.selectedItem != 58) {
                    if (player.itemAnimation == 0 && player.ItemTimeIsZero && player.reuseDelay == 0) {
                        player.selectedItem = Player.PreselectedItem;
                    }
                    else if (player.ItemAnimationEndingOrEnded && Player.HasControlUseItemStoppedSinceItemAnimationStarted &&
                        !player.controlTorch) {
                        player.selectedItem = Player.PreselectedItem;
                        player.reuseDelay = 0;
                    }
                }
            });

            //  :     PlayerInput.MouseInModdedUI.Clear();
            //->: }
            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchBr(out _)
                && x.Previous.MatchCallvirt("System.Collections.Generic.List`1<System.String>", "Clear")
                && x.Previous.Previous.MatchLdsfld("Terraria.GameInput.PlayerInput", "MouseInModdedUI"))) {
                throw new Exception.InstructionNotFound();
            }

            //  :         PlayerInput.MouseInModdedUI.Clear();
            //  :     }
            //--: }
            //--: else {
            c.Next.Operand = beforeStonedCheck;

            //->: else if (!flag8) {
            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdloc(34)
                && x.Next.MatchBrtrue(out _))) {
                throw new Exception.InstructionNotFound();
            }

            //  :     else if (!flag8) {
            //  :         {...}
            //  :     }
            //--: }
            //--: else {
            c.Next.Next.Operand = beforeStonedCheck;
        }

        private static void Player_ScrollHotbar_InteractiveUIsWhileUsingItem(ILContext il) {
            var c = new ILCursor(il);

            // Replace every read and write of 'Terraria.Player.selectedItem' for 'BetterGameUI.Player.PreselectedItem'
            var preselectedItemField = typeof(Player)
                .GetField("preselectedItem", BindingFlags.NonPublic | BindingFlags.Static);
            while (c.TryGotoNext(MoveType.Before,
                x => x.MatchLdfld("Terraria.Player", "selectedItem")
                || x.MatchStfld("Terraria.Player", "selectedItem")
            )) {
                c.Next.Operand = preselectedItemField;
            }

            c.Goto(il.Instrs[0]);

            //->:nonTorch = -1;
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.MatchLdcI4(-1)
                && x.Next.Next.MatchStfld("Terraria.Player", "nonTorch")
            )) {
                throw new Exception.InstructionNotFound();
            }
            //--:nonTorch = -1;
            c.MoveAfterLabels();
            c.RemoveRange(3);

            //->:if (itemAnimation == 0 && selectedItem != 58) {
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0)
                && x.Next.OpCode == OpCodes.Ldfld
                && x.Next.Operand is FieldReference
                && ((FieldReference)x.Next.Operand).Is(
                    typeof(Terraria.Player).GetField("itemAnimation", BindingFlags.Public | BindingFlags.Instance))
                && x.Next.Next.MatchBrtrue(out _)
            )) {
                throw new Exception.InstructionNotFound();
            }
            //--:if (itemAnimation == 0 && selectedItem != 58) {
            //++:if (selectedItem != 58) {
            c.MoveAfterLabels();
            c.RemoveRange(3);
        }
    }
}