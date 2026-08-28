using UnityEngine;

public class TorchBehavior : MonoBehaviour
{
    public bool activated;
    public SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (player != null)
            {
                player.torch = this;
                Debug.Log("Player is near " + gameObject.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (player != null && player.torch == this)
            {
                player.torch = null;
                Debug.Log("Player left " + gameObject.name);
            }
        }
    }

    private void Update()
    {
        if (activated)
        {
            sprite.color = Color.red;
        }
    }
}
