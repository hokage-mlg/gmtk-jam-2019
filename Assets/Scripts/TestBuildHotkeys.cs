using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBuildHotkeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            var currentIndex = SceneManager.GetActiveScene().buildIndex;
            string printText = $"This scene: {currentIndex}, next scene: {currentIndex + 1}\n" +
                $"Scene name: {SceneManager.GetSceneByBuildIndex(currentIndex).name}   " +
                $"Next Scene name: {SceneManager.GetSceneByBuildIndex(currentIndex + 1).name}";
            print(printText);
            // We are not at last scene
            if (currentIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentIndex + 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            var currentIndex = SceneManager.GetActiveScene().buildIndex;
            string printText = $"This scene: {currentIndex}, previous scene: {currentIndex - 1}\n" +
                $"Scene name: {SceneManager.GetSceneByBuildIndex(currentIndex).name}   " +
                $"Previous Scene name: {SceneManager.GetSceneByBuildIndex(currentIndex - 1).name}";
            print(printText);
            // We are not at first scene
            if (currentIndex > 0)
            {
                SceneManager.LoadScene(currentIndex - 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            // GetComponent<CharacterLife>().enabled = false; no update/collission check inside, enable does not stop it
        }
    }
}
