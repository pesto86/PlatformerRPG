using UnityEngine;

public class ShopkeeperManager : MonoBehaviour
{
    [SerializeField] private DialogueScript dialogueScript;
    [SerializeField] private Sprite shopkeeperSprite;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Shopkeeper"))
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            player.InDialogue();
            dialogueScript.SetDialogue("Good day adventurer, do you want to browse my wares? Press E to continue", shopkeeperSprite);
            dialogueScript.SlideUp();
        }   
    }

}
