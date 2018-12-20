using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("MainMenu"); // CHANGE THIS TO MAIN MENU LATER
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
