using UnityEngine;
using UnityEngine.UI;

public class EnemyProfile : MonoBehaviour
{
    public int attackDamage = 10;
    public int health = 20;
    public int maxHealth = 20;
    public ParticleSystem enemyBloodEffect;
    private Vector2 lootPosition;
    [SerializeField] GameObject coin;
    int coinNumber;
    private bool isDead = false;
    public Slider healthSlider;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private RectTransform enemyHealthUI;
    private Vector3 positionOffset = new(0, 1.5f, 0);
    public bool playerInRange;
    
    void Awake()
    {
        healthSlider.gameObject.SetActive(false);
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
        }

        if (playerInRange)
        {
            healthSlider.gameObject.SetActive(true);
        }

        if (!playerInRange)
        {
            healthSlider.gameObject.SetActive(false);
        }


    }

    public void Damage()
    {
        enemyBloodEffect.Play();
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth()
    {
        healthSlider.value = health;
    }

    void LateUpdate()
    {
        Vector3 worldPosition = enemyPosition.position + positionOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        enemyHealthUI.position = screenPosition;
    }

}
