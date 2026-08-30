using UnityEngine;

public class TorchBehavior : MonoBehaviour
{
    public bool activated;
    public SpriteRenderer sprite;

    public GameObject flame;

    public AudioClip torchLight;

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

    public void ActivateTorch()
    {
        // Don't activate it again if it's already lit
        if (activated)
            return;

        activated = true;

        // Turn on flame
        flame.SetActive(true);

        // Play sound ONCE
        SoundEffects.instance.PlaySFX(torchLight, 5f);
    }
}
