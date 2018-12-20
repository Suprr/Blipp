using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour {

    public Button levelOne;
    public Button levelTwo;
    public Button back;
    public AudioSource audiosource;
    public AudioClip menu;
    public AudioClip ret;


	// Use this for initialization
	void Start () {
        levelOne.onClick.AddListener(LevelOne); // Add listener to button
        levelTwo.onClick.AddListener(LevelTwo); // Add listener to button
        back.onClick.AddListener(Back);
        StartCoroutine(Menu(1));
    }
	
    IEnumerator Menu(int times)
    {
        for(int i=0; i<times; i++)
        {
            audiosource.PlayOneShot(menu, 0.5F);
            yield return new WaitForSeconds(menu.length);
        }
    }

    void LevelOne()
    {
        LevelData.LevelOne();
        SceneManager.LoadScene("Main"); // Load the main scene with the level data to play
    }

    void LevelTwo()
    {
        LevelData.LevelTwo();
        SceneManager.LoadScene("Main"); // Load the main scene with the level data to play
    }

    void Back() {
        StartCoroutine(Ret(1));
    }
    
    IEnumerator Ret(int times)
    {
        for(int i=0; i<times; i++)
        {
            audiosource.PlayOneShot(ret, 0.5F);
            yield return new WaitForSeconds(ret.length*1.5f);
        }
        SceneManager.LoadScene("MainMenu"); // Go back to Main Menu
    }
}
