using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthDisplay;
    int defaultHealth = 100;
    int currentHealth;

    public void OnDamageTaken(int damage)
    {
        currentHealth -= damage;
        UpdateHealthDisplay();
    }

    void Start()
    {
        currentHealth = defaultHealth;
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        healthDisplay.text = "Health:" + currentHealth.ToString();
    }

    void Update()
    {
        
    }
}
