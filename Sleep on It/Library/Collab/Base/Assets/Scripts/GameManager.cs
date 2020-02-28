using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState { MENU, PLAYING };

    public GameState state;
    //public GameObject myCanvas;
    public List<Scene> scenes;
    public int sceneNumber;
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }
    public GameData gameData;
    private bool tutorialFlag;
    private UpdateHUD updateHUD;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            state = GameState.PLAYING;
            scenes = new List<Scene>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                scenes.Add(SceneManager.GetSceneByBuildIndex(i));
            }
            gameData = new GameData();

            DontDestroyOnLoad(this.gameObject);
            _instance = this;
            updateHUD = GetComponentInChildren<UpdateHUD>();
            tutorialFlag = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        NotificationCenter.Instance.addObserver("IncreaseScore", scoreNotification);
        NotificationCenter.Instance.addObserver("DecreaseLives", deathNotification);
        NotificationCenter.Instance.addObserver("NextLevel", levelNotification);
        NotificationCenter.Instance.addObserver("Reset", resetNotification);
        NotificationCenter.Instance.addObserver("TutorialComplete", tutorialNotification);
    }

    private void OnDisable()
    {
        NotificationCenter.Instance.removeObserver("IncreaseScore", scoreNotification);
        NotificationCenter.Instance.removeObserver("DecreaseLives", deathNotification);
        NotificationCenter.Instance.removeObserver("NextLevel", levelNotification);
        NotificationCenter.Instance.removeObserver("Reset", resetNotification);
        NotificationCenter.Instance.removeObserver("TutorialComplete", tutorialNotification);
    }

    // Update is called once per frame
    /* USED FOR PAUSE MENU
    void Update()
    {
        switch (state)
        {
            case GameState.MENU:
                if (Input.GetButtonDown("Cancel"))
                {
                    play();
                }
                break;
            case GameState.PLAYING:
                if (Input.GetButtonDown("Cancel"))
                {
                    pause();
                }
                break;
        }
    }
    */

    public void reset()
    {
        NotificationCenter.Instance.postNotification(new Notification("Reset", this));
    }

    public void scoreNotification(Notification notification)
    {
        Dictionary<String, System.Object> userInfo = notification.userInfo;
        int increment = (int)userInfo["increment"];
        increaseScore(increment);
    }

    public void deathNotification(Notification notification)
    {
        Dictionary<String, System.Object> userInfo = notification.userInfo;
        int deincrement = (int)userInfo["deincrement"];
        decreaseLives(deincrement);
    }

    public void levelNotification(Notification notification)
    {
        Dictionary<String, System.Object> userInfo = notification.userInfo;
        int increment = (int)userInfo["increment"];
        nextLevel(increment);
    }

    public void resetNotification(Notification notification)
    {
        Dictionary<String, System.Object> userInfo = notification.userInfo;
        int menu = (int)userInfo["menu"];
        reset(menu);
    }

    public void tutorialNotification(Notification notification)
    {
        Dictionary<String, System.Object> userInfo = notification.userInfo;
        bool tutorialComplete = (bool)userInfo["tutorialComplete"];
        tutorialFlagSet(tutorialComplete);
    }

    public void increaseScore(int increment)
    {
        gameData.Score += increment;
        updateHUD.update("score", gameData.Score);
        if(gameData.Score >= 10)
        {
            nextLevel(1);
        }
    }

    public void reset(int menu)
    {
        SceneManager.LoadScene(menu);
        gameData.Lives = 3;
        gameData.Score = 0;
        updateHUD.update("lives", gameData.Lives);
        updateHUD.update("score", gameData.Lives);
        tutorialFlag = false;
    }

    public void nextLevel(int increment)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + increment);
    }

    public void decreaseLives(int deincrement)
    {
        gameData.Lives -= deincrement;
        updateHUD.update("lives", gameData.Lives);
        if (gameData.Lives < 0)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
            userInfo["tutorialFlag"] = tutorialFlag;
            NotificationCenter.Instance.postNotification(new Notification("TutorialFlag", this, userInfo));
        }
    }

    public void tutorialFlagSet(bool tutorialComplete)
    {
       tutorialFlag = tutorialComplete;
    }

    /*
    public void pause()
    {
        myCanvas.SetActive(true);
        state = GameState.MENU;
    }

    public void play()
    {
        state = GameState.PLAYING;
        NotificationCenter.Instance.postNotification(new Notification("Start", this));
        myCanvas.SetActive(false);
    }
    */

    public void loadScene()
    {
        sceneNumber = (sceneNumber + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(sceneNumber);
    }

    public bool getTutorialFlag()
    {
        return tutorialFlag;
    }

    [Serializable]
    public class GameData
    {
        public int Score { get; set; }
        public int Lives { get; set; }
        public bool TutorialComplete { get; set; }

        public GameData()
        {
            Score = 0;
            Lives = 3;
        }
    }
}
