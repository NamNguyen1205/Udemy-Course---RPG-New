using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MerchantSlot : UI_ItemSlot
{
    private Inventory_Merchant merchant;
    public enum MerchantSlotType { MerchantSlot, PlayerSlot }
    public MerchantSlotType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        bool rightButton = eventData.button == PointerEventData.InputButton.Right;
        bool leftButton = eventData.button == PointerEventData.InputButton.Left;

        if (slotType == MerchantSlotType.PlayerSlot)
        {
            if (rightButton)
            {
                // sell item on merchant class
                bool sellFullStack = Input.GetKey(KeyCode.LeftControl);
                merchant.TrySellItem(itemInSlot, sellFullStack);
            }
            else if (leftButton)
            {
                base.OnPointerDown(eventData);
            }
        }
        else if (slotType == MerchantSlotType.MerchantSlot)
        {
            if (leftButton)
                return;

            bool buyFullStack = Input.GetKey(KeyCode.LeftControl);
            merchant.TryBuyItem(itemInSlot, buyFullStack);
        }

        ui.itemToolTip.ShowToolTip(false, null);

    }
    
    override public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        if(slotType == MerchantSlotType.MerchantSlot)
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, buyPrice: true, showMerchantInfo: true);
        else
            ui.itemToolTip.ShowToolTip(true, rect,itemInSlot ,buyPrice: false, showMerchantInfo: true);
    }

    public void SetupMerchantUI(Inventory_Merchant merchant) => this.merchant = merchant;
}
