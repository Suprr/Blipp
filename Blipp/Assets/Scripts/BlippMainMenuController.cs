using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlippMainMenuController : MonoBehaviour {

    public Button arcade;
    public Button levels;
    public AudioSource audiosource;
    public AudioClip blipp;

    // Use this for initialization
    void Start() {
        arcade.onClick.AddListener(Arcade); // Add listener to button
        levels.onClick.AddListener(Levels); // Add listener to button
        StartCoroutine(PlayAudio (3));
    }
 
    IEnumerator PlayAudio(int times)
    {
        for(int i=0; i<times; i++)
        {
            audiosource.PlayOneShot(blipp, 0.5F);
            yield return new WaitForSeconds(blipp.length-0.095f);
        }
    }
    
    void Update(){
    }


    void Arcade()
    {
        LevelData.ArcadeMode();
        SceneManager.LoadScene("Main"); // Reload the main scene with the same level data to play again
    }
    void Levels()
    {
        SceneManager.LoadScene("LevelMenu"); // Switch to Level Menu
    }
}
