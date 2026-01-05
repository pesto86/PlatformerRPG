using UnityEngine;
using TMPro;
using NUnit.Framework;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem bloodEffect;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public DialogueScript dialoguePanel;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    public int playerMoney = 0;
    public Inventory inventory;
    [SerializeField] private InventoryUIController inventoryController;
    
    public void Damage(int damage)
    {
        bloodEffect.Play();
        playerHealth.OnDamageTaken(damage);
    }

    void Awake()
    {
        UpdateMoneyDisplay(0);
        inventory = new Inventory();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SlideUp();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dialoguePanel.SlideDown();
        }


        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            Destroy(collision.gameObject);
            UpdateMoneyDisplay(1);
        }

    }

    void UpdateMoneyDisplay(int moneyValue)
    {
        playerMoney += moneyValue;
        coinDisplay.text = "$" + playerMoney;
    }

    public void AddItem(Items item) // the additem method which is called by pickup script
    {
        inventory.Add(item); // the add method on inventory which adds the item to this instance of inventory
        if (inventoryController.inventoryVisible)
        {
            inventoryController.ClearInventory();
            inventoryController.PopulateInventory();
        }
        
    }

    public void UpdateDamage(int damage)
    {
        playerAttack.UpdateAttackDamage(damage);
    }



}
