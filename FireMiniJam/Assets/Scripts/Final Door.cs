using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

//combine this behaviour into a single event manager script after gamejam when i have more time lol

public class FinalDoor : MonoBehaviour
{
    public string finalScene;
    public GameObject player;
    public bool playerInDoor=false;
    public AudioClip doorUnlock;
    public AudioClip puzzleSolve;

    [Header("Torch/Door Slots")]
    public GameObject doorLid;
    public GameObject torch_1, torch_2, torch_3;
    private TorchBehavior torch1, torch2, torch3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        torch1 = torch_1.GetComponent<TorchBehavior>();
        torch2 = torch_2.GetComponent<TorchBehavior>();
        torch3 = torch_3.GetComponent<TorchBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (torch1.activated && torch2.activated && torch3.activated) 
        {
            doorLid.SetActive(false);
           // SoundEffects.instance.PlaySFX(puzzleSolve, 2);
        }

        if (playerInDoor && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z pressed at FINAL DOOR");

            if (player != null && torch1.activated && torch2.activated && torch3.activated)
            {
                SoundEffects.instance.PlaySFX(doorUnlock,2);
                SceneManager.LoadScene(finalScene);
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("Player entered door trigger");
            playerInDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            Debug.Log("Player left door trigger");
            playerInDoor = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
}
