using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Player kvothe;
    public SelecterMovement selecter;
    public GameObject pauseMenu, settingsMenu, quitMenu;
    public GameObject menuFirstButton, settingsFirstButton, settingsClosedButton, quitFirstButton, quitClosedButton;
    public SaveManager saveManager;

    public static bool gameIsPaused = false;

    void Update() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Pause() {
        kvothe.enabled = false;
        selecter.enabled = false;
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        gameIsPaused = true;
    }

    public void Resume() {
        kvothe.enabled = true;
        selecter.enabled = true;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
        gameIsPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeGame() {
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void openSettings ()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);

    }

    public void closeSettings ()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(settingsClosedButton);
    }

    public void openQuit ()
    {
        quitMenu.SetActive(true);
        pauseMenu.SetActive(false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(quitFirstButton);

    }

    public void SaveAndQuit () {
        saveManager.SaveSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void closeQuit ()
    {
        quitMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(quitClosedButton);
    }
}