using UnityEngine;

public class GameEventManager : MonoBehaviour
{

    public static GameEventManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }


}
