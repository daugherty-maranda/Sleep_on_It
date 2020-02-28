using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float yAngle = 90f, xAngle = 0f, zAngle = 0f;

    [SerializeField] private float range = 5f;

    GameObject player;

    private Transform door;

    //Locked and Unlocked doors
    private bool isLocked;

    //private void OnMouseDown()
    void openDoor()
    {
        //This is for the player
        player = GameObject.FindWithTag("Player");

        door = this.transform;

        if(Vector3.Distance(player.transform.position, door.position) <= range)
        {
            if(isLocked == false)
            {
                if (yAngle == 90f)
                {
                    gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
                    yAngle = 0f;
                }
                else
                {
                    gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                    yAngle = 90f;
                }
            }
            /*
            if (yAngle == 90f)
            {
                gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
                yAngle = 0f;
            }
            else
            {
                gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                yAngle = 90f;
            }*/
        }
        /*
        if (yAngle == 90f)
        {
            gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
            yAngle = 0f;
        }
        else
        {
            gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
            yAngle = 90f;
        }*/
         
    }

    private void OnTriggerEnter(Collider guardColli)
    {
        if (guardColli.gameObject.tag == "Guard")
        {
            if (yAngle == 90f)
            {
                gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
                yAngle = 0f;
                StartCoroutine(waitSeconds());
            }
            else
            {
                gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                yAngle = 90f;
                StartCoroutine(waitSeconds());
            }
        }

        IEnumerator waitSeconds()
        {
            yield return new WaitForSeconds(2);

            if (yAngle == 90f)
            {
                gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
                yAngle = 0f;
            }
            else
            {
                gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                yAngle = 90f;
            }
        }
    }
}
