using UnityEngine;

public class UI_Craft : MonoBehaviour
{
    [SerializeField] private UI_ItemSlotParent inventoryParent;
    private Inventory_Player inventory;
    private UI_CraftPreview craftPreviewUI;
    private UI_CraftSlot[] craftSlots;
    private UI_CraftListButton[] craftListButtons;


    public void SetupCraftUI(Inventory_Storage storage)
    {
        inventory = storage.playerInventory;
        inventory.OnInventoryChange += UpdateUI;
        UpdateUI();

        craftPreviewUI = GetComponentInChildren<UI_CraftPreview>();
        craftPreviewUI.SetupCraftPreview(storage);
        SetUpCraftListButtons();
    }

    private void SetUpCraftListButtons()
    {
        craftSlots = GetComponentsInChildren<UI_CraftSlot>();

        foreach (var slot in craftSlots)
            slot.gameObject.SetActive(false);

        craftListButtons = GetComponentsInChildren<UI_CraftListButton>();

        foreach (var button in craftListButtons)
            button.SetCraftSlots(craftSlots);

    }
    
    private void UpdateUI() => inventoryParent.UpdateSlots(inventory.itemList);
    
}
