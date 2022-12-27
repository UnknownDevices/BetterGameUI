using Mono.Cecil.Cil;

namespace BetterGameUI
{
    public static class ItemSlotsChanges
    {
        public static void Load() {
            try {
                IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color +=
                    IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            }
            catch (System.Reflection.TargetInvocationException e) {
                throw new BetterGameUI.Exception.LoadingItemSlotsChanges(e);
            }
            catch (BetterGameUI.Exception.InstructionNotFound e) {
                throw new BetterGameUI.Exception.LoadingItemSlotsChanges(e);
            }
        }

        public static void IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(MonoMod.Cil.ILContext il) {
            // 1554: - if (context == 0 && slot < 10 && player.player.selectedItem == slot) {
            // 1554: + if (context == 0 && player.player.selectedItem == slot) {
            il.Instrs[89] = il.IL.Create(OpCodes.Nop);
            il.Instrs[90] = il.IL.Create(OpCodes.Nop);
            il.Instrs[91] = il.IL.Create(OpCodes.Nop);

            // - 1571: else if (context == 0 && slot < 10) {
            // + 1571: else if (context == 0) {
            // + 1572:     if (slot < 10) {
            // + 1573:         value = TextureAssets.InventoryBack9.Value;
            // + 1574:     }
            il.Instrs[214].Operand = il.Instrs[218];
        }

    }
}
