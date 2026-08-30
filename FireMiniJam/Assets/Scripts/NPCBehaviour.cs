using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;

public class NPCBehaviour : MonoBehaviour
{
    private GameObject player;
    public DialogueSystem Dialogue;

    private bool playerInRange = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered NPC trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left NPC Trigger");
        }
    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X))
        {
            if (!Dialogue.IsDialogueActive())
            {
                Dialogue.StartDialogue();
            }
        }
    }


}
