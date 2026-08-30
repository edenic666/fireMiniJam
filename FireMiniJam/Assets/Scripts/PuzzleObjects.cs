using UnityEngine;

public class PuzzleObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public PuzzleEvent puzzleChange;
    public SpriteRenderer sprite;

    private bool playerInRange = false;

    public GameObject player;
    public PlayerBehavior playerBehavior;

    public string PuzzleResult;

    public AudioClip click;

    public Sprite[] imageIndex;
    public enum PuzzleEvent
    {
        None,
        Heart,
        Spade,
        Diamond,
        Clover
    }

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerInRange==true&&playerBehavior.solvedRiddle==!true) 
        {
            PuzzleChanger();
            SoundEffects.instance.PlaySFX(click);
        }
    }

    public void PuzzleChanger() 
    {
        puzzleChange++;

        if (puzzleChange > PuzzleEvent.Clover) 
        {
            puzzleChange = PuzzleEvent.Heart;
        }
        switch (puzzleChange)
        {
            case PuzzleEvent.Heart:
                sprite.sprite = imageIndex[0];              
                PuzzleResult = "Heart";
                break;
            case PuzzleEvent.Spade:
                sprite.sprite = imageIndex[1];
                PuzzleResult = "Spade";
                break;
            case PuzzleEvent.Diamond:
                sprite.sprite = imageIndex[2];
                PuzzleResult = "Diamond";
                break;
            case PuzzleEvent.Clover:
                sprite.sprite = imageIndex[3];
                PuzzleResult = "Clover";
                break;
        }
    }
}
