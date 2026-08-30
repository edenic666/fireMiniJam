using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public PuzzleObjects puzzle_1, puzzle_2, puzzle_3;
    public PlayerBehavior player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle_1.PuzzleResult == "Heart" && puzzle_2.PuzzleResult=="Spade" && puzzle_3.PuzzleResult=="Diamond") 
        {
            player.solvedRiddle = true;
        }
    }
}
