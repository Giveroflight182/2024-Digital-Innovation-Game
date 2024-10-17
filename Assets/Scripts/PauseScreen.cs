using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused){
                ResumeGame();
            }
            
            else{
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;

    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;

    }
}
