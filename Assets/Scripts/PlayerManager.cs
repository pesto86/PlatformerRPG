using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    public ParticleSystem bloodEffect;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public DialogueScript dialoguePanel;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    [SerializeField] private TextMeshProUGUI promptDisplay;
    [SerializeField] private TextMeshProUGUI promptInstance;
    [SerializeField] private InventoryUIController inventoryController;
    [SerializeField] private RectTransform promptPosition;
    [SerializeField] private Transform playerPosition;
    private Vector3 positionOffset = new(0, 1.5f, 0);

    [Header("Player Data")]
    public int playerMoney = 0;
    public Inventory inventory;

    [Header("State")]
    public string playerState;
    
    public void Damage(int damage)
    {
        bloodEffect.Play();
        playerHealth.OnDamageTaken(damage);
    }

    void Awake()
    {
        UpdateMoneyDisplay(0);
        inventory = new Inventory();

        promptInstance = Instantiate(promptDisplay, promptPosition);
        promptInstance.gameObject.SetActive(false); 
    }

    void LateUpdate()
    {
        
    }

    void Update()
    {
        SetPromptPosition();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dialoguePanel.SlideDown();
        }

        switch (playerState)
        {
            case "inRangeOfInteraction":
                promptInstance.gameObject.SetActive(true); 
                
                break;

            case "inDialogue":

                promptInstance.gameObject.SetActive(false); 
                
                break;

            case "!inRangeOfInteraction":

                promptInstance.gameObject.SetActive(false); 

                break;
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

    public void UpdateMoneyDisplay(int moneyValue)
    {
        playerMoney += moneyValue;
        coinDisplay.text = "$" + playerMoney;
    }

    public void DisplayPrompt()
    {
        promptDisplay.text = "Press E"; // change this to setActive
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

    public void InDialogue()
    {
        playerState = "inDialogue";
    }

    public void InRangeOfInteraction()
    {
        playerState = "inRangeOfInteraction";
    }

    public void NotInRangeOfInteraction()
    {
        playerState = "!inRangeOfInteraction";
        dialoguePanel.ClearDialogue();
        dialoguePanel.SlideDown();
    }

    public void SetPromptPosition()
    {
        RectTransform rect = promptInstance.GetComponent<RectTransform>();
        Vector3 worldPosition = playerPosition.position + positionOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        rect.position = screenPosition;
    }

}
