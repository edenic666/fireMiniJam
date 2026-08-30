using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public GameObject torch_1;
    private TorchBehavior torch;

    public GameObject chestClosed;
    public SpriteRenderer sprite;

    public GameObject playerChar;
    private PlayerBehavior player;

    public AudioClip chestOpened;

    private bool playerInside = false;

    private Animator anim;

    void Start()
    {
        torch = torch_1.GetComponent<TorchBehavior>();
        player = playerChar.GetComponent<PlayerBehavior>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (torch.activated)
        {
            anim.SetBool("PlayerOpen", true);
            chestClosed.SetActive(false);
        }

        if (playerInside && torch.activated && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Chest destroyed!");

            player.hasItem = true;
            SoundEffects.instance.PlaySFX(chestOpened,2);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered chest");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player left chest");
        }
    }
}