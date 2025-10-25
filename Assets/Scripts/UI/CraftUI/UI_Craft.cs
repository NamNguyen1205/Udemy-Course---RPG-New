using UnityEngine;

public class UI_Craft : MonoBehaviour
{
    private UI_CraftSlot[] craftSlots;
    private UI_CraftListButton[] craftListButtons;

    private void Awake()
    {
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
}
