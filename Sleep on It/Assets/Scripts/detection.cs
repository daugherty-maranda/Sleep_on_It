using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection : MonoBehaviour
{
    private bool isTrigger = false;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
            userInfo["deincrement"] = 1;
            NotificationCenter.Instance.postNotification(new Notification("DecreaseLives", this, userInfo));
            Debug.Log("Hit");
            isTrigger = true;
        }
    }
}

