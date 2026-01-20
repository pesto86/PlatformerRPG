using UnityEngine;

public class ShopkeeperManager : MonoBehaviour
{
    [SerializeField] private DialogueScript dialogueScript;
    [SerializeField] private Sprite shopkeeperSprite;
    [SerializeField] private ShopkeeperDialogue shopkeeperDialogue;
    [SerializeField] private ShopkeeperDialogue shopkeeperDialogueNegative;
    private bool playerInRange;
    private bool playerEncountered;
    private PlayerManager player;
    private int dialogueIndex;

    void Awake()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerManager>();
            playerInRange = true;
            PlayerInProximity();
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            dialogueIndex = 0;
            playerInRange = false;
            playerEncountered = false;
            player.NotInRangeOfInteraction();
            player = null;
        }
    }

    void Update()
    {
        if (player != null)
        {
            HandleInteraction();
        }
        
    }

    private void PlayerInProximity()
    {
            if (player.playerState != "inDialogue" && playerEncountered != true)
            {
                player.InRangeOfInteraction();
                playerEncountered = true;
                
            }
    }

    private void HandleInteraction()
    {
        switch (player.playerState)
            {
                case "inDialogue":                    

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        switch (dialogueIndex)
                            {
                            
                                case 0:
                                    dialogueScript.SetDialogue(shopkeeperDialogueNegative.lines[0]);
                                    dialogueIndex++;
                                    break;
                                
                                case 1:
                                    dialogueScript.SlideDown();
                                    dialogueIndex = 0;
                                    player.InRangeOfInteraction();
                                    break;
                            }
                    }
                    
                    break;
                
                case "inRangeOfInteraction":

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialogueScript.SetDialogue(shopkeeperDialogue.lines[0]);
                        dialogueScript.SlideUp();
                        player.InDialogue();
                    }
                    break;
                
                case null:
                break;
            
            }
    }

}