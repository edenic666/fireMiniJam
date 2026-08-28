using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public string[] lines; 
    public float textSpeed; 
    private int index;
    public GameObject dialogueBox;
    private bool dialogueActive = false;

    public bool IsDialogueActive() 
    {
        return dialogueActive;
    }

    void Start()
    { 
        // Don't automatically start here if the NPC // should only talk when interacted with.
      textComponent.text = string.Empty;
      dialogueBox.SetActive(false);
    }
    void Update() {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            if (textComponent.text == lines[index]) 
            { 
                NextLine(); 
            } 
            else 
            { 
                StopAllCoroutines(); 
                textComponent.text = lines[index]; 
            } 
        } 
    }

    // Call this every time the player talks to the NPC
    public void StartDialogue() 
    { // Make sure the dialogue object is active
      dialogueBox.SetActive(true); 
        // Reset everything so the dialogue starts from line 1
        index = 0; 
        textComponent.text = string.Empty; 
        StopAllCoroutines(); 
        StartCoroutine(TypeLine()); } 
    IEnumerator TypeLine() 
    { 
        foreach (char c in lines[index].ToCharArray()) 
        { 
            textComponent.text += c; 
            yield return new WaitForSeconds(textSpeed); 
        } 
    } 
    void NextLine() 
    { 
        if (index < lines.Length - 1) 
        { 
            index++; 
            textComponent.text = string.Empty; 
            StartCoroutine(TypeLine()); 
        } 
        else 
        { // Dialogue is finished. // Hide it until the NPC is spoken to again.
              EndDialogue();
        } 
    }

    public void EndDialogue() 
    {
        StopAllCoroutines();
        dialogueActive = false;
        textComponent.text=string.Empty;
        dialogueBox.SetActive(false);
    }
}
