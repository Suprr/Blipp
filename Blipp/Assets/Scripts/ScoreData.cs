using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static Class that stores Score Data that can be accessed by other scripts
// Can set score data in this class, then load scene LevelEnd and it will read from this class
public class ScoreData : MonoBehaviour {

    public static int score; // Final score
    public static int lives; // Final lives remaining
    public static int combo; // Best combo
    public static bool win; // True if win, false if lose

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject); // Do not reset this object
    }
}
