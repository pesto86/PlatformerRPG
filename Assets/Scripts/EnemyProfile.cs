using UnityEngine;
using UnityEngine.UI;

public class EnemyProfile : MonoBehaviour
{
    [Header("Stats")]
    public int attackDamage = 10;
    public int health = 20;
    public int maxHealth = 20;
    public ParticleSystem enemyBloodEffect;

    [Header("Loot")]
    [SerializeField] GameObject coin;
    private Vector2 lootPosition;
    int coinNumber;

    [Header("Health UI")]
    public Slider healthSlider;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private RectTransform enemyHealthUI;
    private Vector3 positionOffset = new(0, 1.5f, 0);
    [SerializeField] private Slider healthSliderInstance;

    [Header("State")]
    public bool playerInRange;
    private bool playerEncountered;
    private bool isDead = false;
    
    void Awake()
    {
        healthSliderInstance = Instantiate(healthSlider, enemyHealthUI);
        SetMaxHealth();
        healthSliderInstance.gameObject.SetActive(false);
        playerInRange = false;
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            coinNumber = Random.Range(1, 5);
            for (int i = 0; i < coinNumber; i++)
            {
                lootPosition = new Vector2(Random.Range(transform.position.x-2, transform.position.x + 2), transform.position.y);    
                Instantiate(coin, lootPosition, Quaternion.identity);
                Debug.Log("Loot dropped");
            }
            Destroy(gameObject);
            Destroy(healthSliderInstance);
        }

        switch (playerInRange)
        {
            case true when !playerEncountered:
                playerEncountered = true;
                healthSliderInstance.gameObject.SetActive(true);
                SetHealth();
                break;
            
            case false when playerEncountered:
                healthSliderInstance.gameObject.SetActive(false);
                break;
            
            case true when playerEncountered:
                healthSliderInstance.gameObject.SetActive(true);
                SetHealth();
                break;
        }


    }

    public void Damage()
    {
        enemyBloodEffect.Play();
    }

    public void SetMaxHealth()
    {
        healthSliderInstance.maxValue = maxHealth;
        healthSliderInstance.value = maxHealth;
    }

    public void SetHealth()
    {
        healthSliderInstance.value = health;
    }

    public void SetHealthBarPosition()
    {
        RectTransform rect = healthSliderInstance.GetComponent<RectTransform>();
        Vector3 worldPosition = enemyPosition.position + positionOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        rect.position = screenPosition;
    }

    void LateUpdate()
    {
        SetHealthBarPosition();
    }

}
