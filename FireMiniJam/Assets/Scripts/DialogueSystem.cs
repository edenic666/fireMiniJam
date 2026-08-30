using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueSystem : MonoBehaviour
{
    [Header("Assign Dialogue Box")]
    public TextMeshProUGUI textComponent;
    public GameObject dialogueBox;

    [Header("Dialogue Change Event")]
    public DialogueEvent dialogueChangeEvent;

    [Header("Lines Before")]
    public string[] linesBefore;

    [Header("Lines After")]
    public string[] linesAfter;

    [Header("Player")]
    public PlayerBehavior player;

    public float textSpeed;

    private string[] currentLines;

    private int index;
    public bool dialogueActive = false;
    private bool isTyping = false;

    public enum DialogueEvent 
    {
        None,
        HasItem,
        SolvedRiddle
    }
    public bool IsDialogueActive() 
    {
        return dialogueActive;
    }

    void Start()
    { 
      textComponent.text = string.Empty;
      dialogueBox.SetActive(false);
    }
    void Update() {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            if (isTyping) 
            {
                FinishLine();
            } else NextLine();
        } 
    }

    // Call this every time the player talks to the NPC
    public void StartDialogue() 
    {
        if (dialogueActive)
            return;

        if (EventHasHappened()) 
        {
            currentLines = linesAfter;
        }
        else
        {
            currentLines = linesBefore;
        }

        if (currentLines == null || currentLines.Length == 0) 
        {
            Debug.LogWarning("No dialogue lines assigned");
        }

        dialogueActive = true;
        index = 0;

        dialogueBox.SetActive(true);

        textComponent.text = string.Empty;

        StartCoroutine(TypeLine());
    }

    private bool EventHasHappened() 
    {
        switch (dialogueChangeEvent) 
        {
            case DialogueEvent.HasItem:
                return player.hasItem;

            case DialogueEvent.SolvedRiddle:
                return player.solvedRiddle;

            case DialogueEvent.None:
            default:
                return false;
        }
    }
    IEnumerator TypeLine() 
    { 
        isTyping= true;
        textComponent.text = string.Empty;
        foreach (char c in currentLines[index].ToCharArray()) 
        { 
            textComponent.text += c; 
            yield return new WaitForSeconds(textSpeed); 
        }
        isTyping = false;
    }
    private void FinishLine() 
    {
        StopAllCoroutines();

        textComponent.text = currentLines[index];

        isTyping = false;
    }
    void NextLine() 
    { 
        if (index < currentLines.Length - 1) 
        { 
            index++; 
            StartCoroutine(TypeLine()); 
        } 
        else 
        {
              EndDialogue();
        } 
    }

    public void EndDialogue() 
    {
        StopAllCoroutines();

        dialogueActive = false;
        isTyping=false;

        textComponent.text=string.Empty;

        dialogueBox.SetActive(false);
    }
}
