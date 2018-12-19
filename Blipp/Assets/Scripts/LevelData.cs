using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static Class that stores Level Data that can be accessed by other scripts
// Can change the Level Data then reload the Main Scene to Load the Level
public class LevelData : MonoBehaviour {
    public static int[] bolts; // Color of each bolt, referenced by index of Color array
    public static int[] boltTimes; // Time each bolt will spawn relative to start
    public static int[][] boltPos; // Bolt position, each array is X and Y, each value is either -1, 0, 1, where 0 is the center
    public static int nBolts; // Number of bolts
    public static Color[][][] cubeColors; // Colors of each cube
    public static float fallSpeed; // Fall speed of bolts
    public static float spawnHeight; // Spawn height of bolts
    public static bool arcade; // True if arcade mode, false otherwise

    public static Color[] colors; // List of colors
    public Color[] initColors;

    // Initialize this script before starting the rest of the scene
    void Awake()
    {
        colors = new Color[initColors.Length]; // Copy over colors to static variable
        for (int i = 0;i < initColors.Length;i++)
        {
            colors[i] = initColors[i];
        }

        DontDestroyOnLoad(gameObject); // Do not reset this object

        // LevelOne(); // Load level 1
        ArcadeMode();
    }

    void ArcadeMode()
    {
        arcade = true;

        fallSpeed = 2.0f;
        spawnHeight = 20f;
    }

    // Load level 1 into static data structures
    // Level 1 is a simpler level because it only involves 3 colors but still requires the user to move fast
    // and occasionally deal with bolts on the other side of the cube
    void LevelOne()
    {
        arcade = false;

        fallSpeed = 2.0f;
        spawnHeight = 20f;

        nBolts = 14;
        bolts = new int[nBolts];
        boltTimes = new int[nBolts];
        boltPos = new int[nBolts][];
        cubeColors = new Color[3][][];

        bolts[0] = 0;
        bolts[1] = 0;
        bolts[2] = 0;
        bolts[3] = 1;
        bolts[4] = 1;
        bolts[5] = 2;
        bolts[6] = 2;
        bolts[7] = 0;
        bolts[8] = 1;
        bolts[9] = 2;
        bolts[10] = 0;
        bolts[11] = 1;
        bolts[12] = 2;
        bolts[13] = 0;

        boltTimes[0] = 0;
        boltTimes[1] = 8;
        boltTimes[2] = 15;
        boltTimes[3] = 22;
        boltTimes[4] = 27;
        boltTimes[5] = 33;
        boltTimes[6] = 38;
        boltTimes[7] = 44;
        boltTimes[8] = 50;
        boltTimes[9] = 56;
        boltTimes[10] = 62;
        boltTimes[11] = 69;
        boltTimes[12] = 75;
        boltTimes[13] = 80;

        boltPos[0] = new int[2];
        boltPos[0][0] = -1;
        boltPos[0][1] = -1;
        boltPos[1] = new int[2];
        boltPos[1][0] = 0;
        boltPos[1][1] = -1;
        boltPos[2] = new int[2];
        boltPos[2][0] = 1;
        boltPos[2][1] = -1;
        boltPos[3] = new int[2];
        boltPos[3][0] = -1;
        boltPos[3][1] = 0;
        boltPos[4] = new int[2];
        boltPos[4][0] = 1;
        boltPos[4][1] = 0;
        boltPos[5] = new int[2];
        boltPos[5][0] = -1;
        boltPos[5][1] = 1;
        boltPos[6] = new int[2];
        boltPos[6][0] = 1;
        boltPos[6][1] = 1;
        boltPos[7] = new int[2];
        boltPos[7][0] = -1;
        boltPos[7][1] = -1;
        boltPos[8] = new int[2];
        boltPos[8][0] = 0;
        boltPos[8][1] = -1;
        boltPos[9] = new int[2];
        boltPos[9][0] = 1;
        boltPos[9][1] = -1;
        boltPos[10] = new int[2];
        boltPos[10][0] = -1;
        boltPos[10][1] = 0;
        boltPos[11] = new int[2];
        boltPos[11][0] = 0;
        boltPos[11][1] = 1;
        boltPos[12] = new int[2];
        boltPos[12][0] = 1;
        boltPos[12][1] = 1;
        boltPos[13] = new int[2];
        boltPos[13][0] = -1;
        boltPos[13][1] = 1;

        cubeColors[0] = new Color[3][];
        cubeColors[0][0] = new Color[3];
        cubeColors[0][0][0] = colors[1];
        cubeColors[0][0][1] = colors[1];
        cubeColors[0][0][2] = colors[0];
        cubeColors[0][1] = new Color[3];
        cubeColors[0][1][0] = colors[2];
        cubeColors[0][1][1] = colors[1];
        cubeColors[0][1][2] = colors[2];
        cubeColors[0][2] = new Color[3];
        cubeColors[0][2][0] = colors[0];
        cubeColors[0][2][1] = colors[2];
        cubeColors[0][2][2] = colors[0];
        cubeColors[1] = new Color[3][];
        cubeColors[1][0] = new Color[3];
        cubeColors[1][0][0] = colors[1];
        cubeColors[1][0][1] = colors[0];
        cubeColors[1][0][2] = colors[2];
        cubeColors[1][1] = new Color[3];
        cubeColors[1][1][0] = colors[0];
        cubeColors[1][1][1] = colors[2];
        cubeColors[1][1][2] = colors[1];
        cubeColors[1][2] = new Color[3];
        cubeColors[1][2][0] = colors[1];
        cubeColors[1][2][1] = colors[0];
        cubeColors[1][2][2] = colors[2];
        cubeColors[2] = new Color[3][];
        cubeColors[2][0] = new Color[3];
        cubeColors[2][0][0] = colors[2];
        cubeColors[2][0][1] = colors[1];
        cubeColors[2][0][2] = colors[1];
        cubeColors[2][1] = new Color[3];
        cubeColors[2][1][0] = colors[0];
        cubeColors[2][1][1] = colors[2];
        cubeColors[2][1][2] = colors[2];
        cubeColors[2][2] = new Color[3];
        cubeColors[2][2][0] = colors[1];
        cubeColors[2][2][1] = colors[0];
        cubeColors[2][2][2] = colors[0];
    }
}
