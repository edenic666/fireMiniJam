using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Transform upperFloor;
    public Transform lowerFloor;

    private GameObject player;

    private void Update()
    {
        // Test if Update is running
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z pressed");

            if (player != null && upperFloor != null)
            {
                player.transform.position = upperFloor.position;
                Debug.Log("Moved up");
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C pressed");

            if (player != null && lowerFloor != null)
            {
                player.transform.position = lowerFloor.position;
                Debug.Log("Moved down");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("Player entered door trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            Debug.Log("Player left door trigger");
        }
    }
}