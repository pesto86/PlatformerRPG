using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private PlayerManager player;
    [SerializeField] private DialogueScript dialogueScript;
    [SerializeField] private Dialogue initialDialogue;
    [SerializeField] private Dialogue successDialogue;
    private bool playerEncountered;
    private int dialogueIndex = 0;
    public bool questSuccess = false;

    void Update()
    {
        if (player != null)
        {
            HandleInteraction();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerManager>();
            PlayerInProximity();
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            dialogueIndex = 0;
            playerEncountered = false;
            player.NotInRangeOfInteraction();
            player = null;
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
            case "inRangeOfInteraction":

                if (Input.GetKeyDown(KeyCode.E) && !questSuccess)
                {
                    dialogueScript.SetDialogue(initialDialogue.lines[0]);
                    dialogueScript.SlideUp();
                    dialogueIndex++;
                    player.InDialogue();
                }

                if (Input.GetKeyDown(KeyCode.E) && questSuccess)
                {
                    dialogueScript.SetDialogue(successDialogue.lines[0]);
                    dialogueScript.SlideUp();
                    dialogueIndex++;
                    player.InDialogue();
                }

            break;

            case "inDialogue": 

                DialogueMode();

            break;
        }
    }

    private void DialogueMode()
    {
        if (!questSuccess)
        {
            if (Input.GetKeyDown(KeyCode.E) && dialogueIndex < initialDialogue.lines.Length) // 4 will be replaced by max length of dialogue
            {
                Debug.Log(dialogueIndex);
                dialogueScript.SetDialogue(initialDialogue.lines[dialogueIndex]);
                dialogueIndex++;
            }

            if (Input.GetKeyDown(KeyCode.E) && dialogueIndex >= initialDialogue.lines.Length) // 4 will be replaced by max length of dialogue
            {
                dialogueScript.SlideDown();
                dialogueIndex = 0;
                player.InRangeOfInteraction();
            }
        }

        if (questSuccess)
        {
            if (Input.GetKeyDown(KeyCode.E) && dialogueIndex <= successDialogue.lines.Length) // 4 will be replaced by max length of dialogue
            {
                dialogueScript.SetDialogue(successDialogue.lines[dialogueIndex]);
                dialogueIndex++;
            }

            if (Input.GetKeyDown(KeyCode.E) && dialogueIndex > successDialogue.lines.Length) // 4 will be replaced by max length of dialogue
            {
                dialogueScript.SlideDown();
                dialogueIndex = 0;
                player.InRangeOfInteraction();
            }
        }
    }
}
