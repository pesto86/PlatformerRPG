using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryGrid;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemName;

    [Header("State")]
    public bool inventoryVisible = false;
    private InventorySlot currentSlot;
    private bool itemVisible = false;

    void Awake()
    {
        inventoryPanel.SetActive(false);
        itemPanel.SetActive(false);
    } 


    public void OnInventoryButton()
    {
        switch (inventoryVisible)
        {
            case true:
                ClearInventory();
                HideItemPanel();
                inventoryPanel.SetActive(false);
                inventoryVisible = false;
                break;
            
            case false:
                PopulateInventory();
                inventoryPanel.SetActive(true);
                inventoryVisible = true;
                break;
        }

        
    }

    
    public void ShowItemPanel()
    {
        itemPanel.SetActive(true);
        itemVisible = true;
    }

    public void HideItemPanel()
    {
        itemPanel.SetActive(false);
        itemVisible = false;
    }

    public void PopulateItemInfo(string itemNam, string itemDesc)
    {
        itemName.text = itemNam;
        itemDescription.text = itemDesc;
    }

    public void ClearItemInfo()
    {
        itemName.text = "";
        itemDescription.text = "";
    }

    public void OnSlotClicked(InventorySlot slot)
    {
        if (itemVisible == false)
        {
            ShowItemPanel();
            PopulateItemInfo(slot.item.itemName, slot.item.itemDescription);
            currentSlot = slot;
        }

        else if (itemVisible == true && slot == currentSlot)
        {
            ClearItemInfo();
            HideItemPanel();
            currentSlot = null;
        }

        else if (itemVisible == true && slot != currentSlot)
        {
            PopulateItemInfo(slot.item.itemName, slot.item.itemDescription);
            currentSlot = slot;
        }
    }

    public void PopulateInventory()
    {
        List<Items> displayedItems = new List<Items>();
        foreach (Items item in playerManager.inventory.InventoryContents)
        {
            int itemCount = playerManager.inventory.CountItems(item);
            if (itemCount == 1)
            {
                GameObject itemIconInstance = Instantiate(itemIcon, inventoryGrid.transform);
                itemIconInstance.GetComponent<Image>().sprite = item.sprite;
                itemIconInstance.GetComponent<InventorySlot>().item = item;
                itemIconInstance.GetComponentInChildren<TextMeshProUGUI>().text = $"x{itemCount}";
            }
            
            if (itemCount > 1 && !displayedItems.Contains(item))
            {
                GameObject itemIconInstance = Instantiate(itemIcon, inventoryGrid.transform);
                itemIconInstance.GetComponent<Image>().sprite = item.sprite;
                itemIconInstance.GetComponent<InventorySlot>().item = item;
                displayedItems.Add(item);
                itemIconInstance.GetComponentInChildren<TextMeshProUGUI>().text = $"x{itemCount}";
                Debug.Log($"There are {itemCount} {item.itemName}s");
            }

        }
    }

    public void ClearInventory()
    {
        foreach (Transform child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
