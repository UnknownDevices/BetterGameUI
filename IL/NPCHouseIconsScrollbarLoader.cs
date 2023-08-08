using BetterGameUI.Reflection;
using BetterGameUI.UI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;

namespace BetterGameUI.IL
{
    public class NPCHouseIconsScrollbarLoader
    {
        public static NPCHouseIconsScrollbar NPCHouseIconsScrollbar;
        public static int HeadsHeldByAnNPCCount = 0;

        public static void Load()
        {
            NPCHouseIconsScrollbar = new NPCHouseIconsScrollbar();
            Terraria.IL_Main.DrawNPCHousesInUI += Main_DrawNPCHousesInUI_NPCHouseIconsScrollbar;
        }

        public static void Main_DrawNPCHousesInUI_NPCHouseIconsScrollbar(ILContext il)
        {
            //    var c = new ILCursor(il);

            //    var mHInfo = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);

            //    ILLabel inForLoop = c.DefineLabel();
            //    if (!c.TryGotoNext(MoveType.Before,
            //        x => x.MatchLdloc(10)
            //        && x.Next.MatchLdcI4(1)
            //        && x.Next.Next.MatchAdd()
            //        && x.Next.Next.Next.MatchStloc(10)
            //        && x.Next.Next.Next.Next.MatchLdloc(10)
            //        && x.Next.Next.Next.Next.Next.MatchLdsfld("Terraria.GameContent.TextureAssets", "NpcHead")
            //        && x.Next.Next.Next.Next.Next.Next.MatchLdlen()
            //        && x.Next.Next.Next.Next.Next.Next.Next.MatchConvI4()
            //        && x.Next.Next.Next.Next.Next.Next.Next.Next.MatchBlt(out inForLoop)))
            //    {
            //        throw new Exception.InstructionNotFound();
            //    }

            //    ILLabel inForLoopConditional = c.MarkLabel();

            //    c.GotoNext(MoveType.After, x => x.MatchBlt(out inForLoop));

            //    ILLabel afterForLoop = c.MarkLabel();

            //    c.Goto(inForLoop.Target.Previous, MoveType.Before);

            //    c.Emit(OpCodes.Ldarg_0);
            //    c.Emit(OpCodes.Ldfld, typeof(Main).GetField("_npcIndexWhoHoldsHeadIndex", BindingFlags.NonPublic | BindingFlags.Instance));
            //    c.Emit(OpCodes.Ldsfld, mHInfo);
            //    c.EmitDelegate((int[] npcIndexWhoHoldsHeadIndex, int mH) =>
            //    {
            //        HeadsHeldByAnNPCCount = 0;
            //        NPCHouseIconsScrollbar.Update(npcIndexWhoHoldsHeadIndex, mH);
            //    });

            //    ILLabel afterIfConditionalToContinue = c.DefineLabel();
            //    if (!c.TryGotoNext(MoveType.Before,
            //        x => x.MatchLdloc(10)
            //        && x.Next.MatchBrfalse(out afterIfConditionalToContinue)
            //        && x.Next.Next.MatchLdarg(0)
            //        && x.Next.Next.Next.MatchLdfld("Terraria.Main", "_npcIndexWhoHoldsHeadIndex")
            //        && x.Next.Next.Next.Next.MatchLdloc(10)
            //        && x.Next.Next.Next.Next.Next.MatchLdelemI4()
            //        && x.Next.Next.Next.Next.Next.Next.MatchLdcI4(-1)
            //        && x.Next.Next.Next.Next.Next.Next.Next.MatchBeq(out _)))
            //    {
            //        throw new Exception.InstructionNotFound();
            //    }

            //    c.GotoLabel(afterIfConditionalToContinue);

            //    c.EmitDelegate(() =>
            //    {
            //        HeadsHeldByAnNPCCount++;
            //        return HeadsHeldByAnNPCCount <= NPCHouseIconsScrollbar.ScrolledNotches * Mod.Config.General_NPCHouseIconsColumns;
            //    });
            //    c.Emit(OpCodes.Brtrue, inForLoopConditional);
            //    c.EmitDelegate(() =>
            //        (NPCHouseIconsScrollbar.ScrolledNotches * Mod.Config.General_NPCHouseIconsColumns + NPCHouseIconsScrollbar.NotchesPerPage * Mod.Config.General_NPCHouseIconsColumns) < HeadsHeldByAnNPCCount);
            //    c.Emit(OpCodes.Brtrue, afterForLoop);

            //    if (!c.TryGotoNext(MoveType.After,
            //        x => x.MatchStloc(12)))
            //    {
            //        throw new Exception.InstructionNotFound();
            //    }

            //    c.Emit(OpCodes.Ldloc_0);
            //    c.EmitDelegate((int num) => Main.screenWidth - 64 - 28 - 48 * (num % Mod.Config.General_NPCHouseIconsColumns));
            //    c.Emit(OpCodes.Stloc, 12);

            //    if (!c.TryGotoNext(MoveType.After,
            //        x => x.MatchStloc(13)))
            //    {
            //        throw new Exception.InstructionNotFound();
            //    }

            //    c.Emit(OpCodes.Ldloc_0);
            //    c.Emit(OpCodes.Ldsfld, mHInfo);
            //    c.EmitDelegate((int num, int mH) => (int)((float)(174 + mH) + (num / Mod.Config.General_NPCHouseIconsColumns * 56 * Main.inventoryScale)));
            //    c.Emit(OpCodes.Stloc, 13);
        }
    }
}