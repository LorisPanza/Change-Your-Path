using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Player kvothe;
    public Robot robot;
    public SelecterMovement selecter;
    public GameObject pauseMenu, settingsMenu, quitMenu, newGameMenu;
    public GameObject menuFirstButton, menuSecondButton, newFirstButton, newClosedButton, settingsFirstButton, settingsClosedButton, quitFirstButton, quitClosedButton;
    public SaveManager saveManager1;
    public SaveManager2 saveManager2;
    public Button quitButton, newGameButton;
    public static bool gameIsPaused = false;

    private GameObject selected;
    public GameManager gameManager;

    void Update() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (selected != EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject != null) {
            selected = EventSystem.current.currentSelectedGameObject;
        }

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                   
                    Resume();
                }
                else
                {
                    
                    Pause();
                }
            }

            if (gameIsPaused)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
                    EventSystem.current.SetSelectedGameObject(selected);
                }
                    
            }
        } else {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
                EventSystem.current.SetSelectedGameObject(selected);
            }
        }

    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (!PlayerPrefs.HasKey("LastScene"))
            {
                menuFirstButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(menuSecondButton);
                selected = menuSecondButton;
                
                Navigation navigation = newGameButton.navigation;
                navigation.selectOnUp = quitButton;
                newGameButton.navigation = navigation;

                navigation = quitButton.navigation;
                navigation.selectOnDown = newGameButton;
                quitButton.navigation = navigation;
            } else {
                selected = menuFirstButton;
            }
        }
    }

    void Pause() {
        
        kvothe.GetComponent<Animator>().enabled = false;
        
        kvothe.enabled = false;
        if (SceneManager.GetActiveScene().name == "SpringScene") robot.enabled = false;
        selecter.enabled = false;
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        selected = menuFirstButton;
        gameIsPaused = true;
        
        gameManager.enabled = false;
    }

    public void Resume() {
        kvothe.GetComponent<Animator>().enabled = true;
        
        kvothe.enabled = true;
        if (SceneManager.GetActiveScene().name == "SpringScene") robot.enabled = true;
        selecter.enabled = true;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
        gameIsPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        gameManager.enabled=true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeGame() {
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PlayNewGame() {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            openNew();
        } else {
            NewGame();
        }
    }

    public void openNew()
    {
        newGameMenu.SetActive(true);
        pauseMenu.SetActive(false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(newFirstButton);

    }

    public void closeNew()
    {
        newGameMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(newClosedButton);
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
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            saveManager1.Save();
            
        }else if (SceneManager.GetActiveScene().name == "SpringScene")
        {
            saveManager2.Save();
        }
        
        
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit () {
        SceneManager.LoadScene("MainMenu");
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
