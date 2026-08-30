using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutSceneDialogue : MonoBehaviour
{
    [Header("Assign Dialogue Box")]
    public TextMeshProUGUI textComponent;
    public GameObject dialogueBox;

    [Header("Dialogue")]
    public string[] lines;
    public float textSpeed = 0.05f;

    [Header("Scene Transition")]
    public string nextSceneName;

    private int index;
    private bool isTyping = false;

    public bool dialogueActive = false;

    public Animator anim;

    private void Start()
    {
        textComponent.text = string.Empty;
        dialogueBox.SetActive(false);

        // Start the cutscene dialogue immediately
        StartDialogue();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                // Finish the current line
                FinishLine();
            }
            else
            {
                // Move to the next line
                NextLine();
            }
        }
    }

    public void StartDialogue()
    {
        if (dialogueActive)
            return;

        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("No dialogue lines assigned!");
            return;
        }

        dialogueActive = true;
        index = 0;

        dialogueBox.SetActive(true);

        textComponent.text = string.Empty;

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;

        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    private void FinishLine()
    {
        StopAllCoroutines();

        textComponent.text = lines[index];

        isTyping = false;
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
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
        isTyping = false;

        textComponent.text = string.Empty;

        dialogueBox.SetActive(false);

        // Go to the next scene
        NextScene();
    }
    IEnumerator WaitForTransition() 
    {

        Debug.Log("started coroutine");
        anim.SetBool("transitioning",true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("finished corooutine");
    }
    public void NextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("No next scene has been assigned!");
            return;
        }
        StartCoroutine(WaitForTransition());
        
    }
}
