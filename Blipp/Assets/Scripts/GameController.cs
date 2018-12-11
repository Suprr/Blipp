using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject bolt;
    public int nBolts;
    public int timeBetweenBolts;

	// Use this for initialization
	void Start () {
		for (int i = 0;i < nBolts;i++)
        {
            Invoke("SpawnBolt", timeBetweenBolts * i); // Call SpawnBolt nBolts times, with a delay of timeBetweenBolts between each call
        }
	}

    // Spawn a new Bolt
    void SpawnBolt()
    {
        GameObject newBolt = Instantiate(bolt); // Copy the bolt
        newBolt.transform.SetParent(bolt.transform.parent);
        int r1 = Random.Range(-1, 2); 
        int r2 = Random.Range(-1, 2);
        int r = Random.Range(0, 4); // Choose a new random color
        newBolt.GetComponent<BoltController>().SetColor(r); // Pass the randomly chosen values to the bolt
        newBolt.GetComponent<BoltController>().CreateCircleSprite(r1, r2);
        newBolt.transform.position = new Vector3(newBolt.transform.position.x + r1 * 2, newBolt.transform.position.y, newBolt.transform.position.z + r2 * 2); // Randomize the position
        // Color is randomized in BoltController Start()
        newBolt.SetActive(true); // Set the bolt to active
    }
}
