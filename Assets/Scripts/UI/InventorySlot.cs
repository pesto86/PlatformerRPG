using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Items item;
    private InventoryUIController controller;


    void Awake()
    {
        controller = gameObject.GetComponentInParent<InventoryUIController>();
    }

    public void OnItemButton()
    {
        controller.OnSlotClicked(this);
    }

    }

/*
switch (itemVisible)
        {
            case true:
                controller.HideItemPanel();
                itemVisible = false;
                break;
            
            case false:
                controller.ShowItemPanel();
                controller.PopulateItemInfo(item.itemName, item.itemDescription);
                itemVisible = true;
                break;
*/
