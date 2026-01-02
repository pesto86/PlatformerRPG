using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryGrid;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public bool inventoryVisible = false;

    void Awake()
    {
        inventoryPanel.SetActive(false);
    } 


    public void OnInventoryButton()
    {
        switch (inventoryVisible)
        {
            case true:
                ClearInventory();
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

    public void OnItemButton()
    {
        
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
                itemIconInstance.GetComponentInChildren<TextMeshProUGUI>().text = $"x{itemCount}";
            }
            
            if (itemCount > 1 && !displayedItems.Contains(item))
            {
                GameObject itemIconInstance = Instantiate(itemIcon, inventoryGrid.transform);
                itemIconInstance.GetComponent<Image>().sprite = item.sprite;
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
