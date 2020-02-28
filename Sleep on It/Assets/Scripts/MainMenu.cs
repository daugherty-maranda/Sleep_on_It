using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Loads next scene when "Play" is clicked.
    public void PlayGame()
    {
        Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
        userInfo["increment"] = 1;
        NotificationCenter.Instance.postNotification(new Notification("NextLevel", this, userInfo));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
        userInfo["menu"] = 0;
        NotificationCenter.Instance.postNotification(new Notification("Reset", this, userInfo));
    }
}