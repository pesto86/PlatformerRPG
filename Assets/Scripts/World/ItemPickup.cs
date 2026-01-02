using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Items item; // serialised box for the chosen item

    void OnValidate()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite != item.sprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
        }
        else
        {
            return;
        }
        
    }

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // if player collides with pickup
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>(); // get the playermanager component from the player
            playerManager.AddItem(item); // run the additem method on player manager - which then calls the add method on inventory
            if (item.type == Items.ItemType.Weapon)
            {
                playerManager.UpdateDamage(item.attackDamage);
            }
            Destroy(gameObject); // destroy the picked up item
            
        }

    }
}
