using UnityEngine;

public class BarrierBehaviour : MonoBehaviour
{
    [Header("Barrier Change Event")]
    public BarrierEvent barrierChangeEvent;
    [Header("Player")]
    public PlayerBehavior player;
    public enum BarrierEvent
    {
        None,
        HasItem,
        SolvedRiddle
    }
    private bool EventHasHappened()
    {
        switch (barrierChangeEvent)
        {
            case BarrierEvent.HasItem:
                return player.hasItem;

            case BarrierEvent.SolvedRiddle:
                return player.solvedRiddle;

            case BarrierEvent.None:
            default:
                return false;
        }
    }

    private void Update()
    {
        if (EventHasHappened()) 
        {
            Destroy(gameObject);
        }
    }

}
