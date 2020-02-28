using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] TutorialPopUps;
    private InputManager input;

    private int PopUpsIndex = 0;
    private bool isSnipped = false;
    private bool isComplete = false;

    void Awake()
    {
        input = GetComponent<InputManager>();
    }

    void OnEnable()
    {
        NotificationCenter.Instance.addObserver("IncreaseScore", tagSnipped);
        setTutorialFlag();
    }

    void OnDisable()
    {
        NotificationCenter.Instance.removeObserver("IncreaseScore", tagSnipped);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isComplete == false)
        {
            // Displays proper tutorial according to current index
            if (TutorialPopUps != null)
            {
                for (int i = 0; i < TutorialPopUps.Length; i++)
                {
                    if (i == PopUpsIndex)
                    {
                        TutorialPopUps[i].SetActive(true);
                    }
                    else
                    {
                        TutorialPopUps[i].SetActive(false);
                    }
                }
            }

            // Registers proper inputs for tutorial pop ups
            switch (PopUpsIndex)
            {
                case 0:
                    // Movement
                    if (input.horizontal > 0f || input.horizontal < 0f || input.vertical > 0f || input.vertical < 0f)
                    {
                        PopUpsIndex += 1;
                    }
                    break;

                case 1:
                    // Sprint
                    if (input.sprint)
                    {
                        PopUpsIndex += 1;
                    }
                    break;

                case 2:
                    // Jump
                    if (input.jump)
                    {
                        PopUpsIndex += 1;
                    }
                    break;

                case 3:
                    // Crouch
                    if (input.crouch)
                    {
                        PopUpsIndex += 1;
                    }
                    break;

                case 4:
                    // Snip Tag
                    if (isSnipped)
                    {
                        PopUpsIndex += 1;
                    }
                    break;
            }

            if (PopUpsIndex >= TutorialPopUps.Length)
            {
                Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
                userInfo["tutorialComplete"] = true;
                NotificationCenter.Instance.postNotification(new Notification("TutorialComplete", this, userInfo));

                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void setTutorialFlag()
    {
        isComplete = FindObjectOfType<GameManager>().getTutorialFlag();
    }

    // Tag snipped notif
    private void tagSnipped(Notification notification)
    {
        if(PopUpsIndex == 4) {
            isSnipped = true;
        }
    }
}
