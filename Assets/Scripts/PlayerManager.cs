using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem bloodEffect;
    public PlayerHealth playerHealth;
    public DialogueScript dialoguePanel;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    public int playerMoney = 0;
    
    public void Damage(int damage)
    {
        bloodEffect.Play();
        playerHealth.OnDamageTaken(damage);
    }

    void Awake()
    {
        UpdateMoneyDisplay(0);
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

}
