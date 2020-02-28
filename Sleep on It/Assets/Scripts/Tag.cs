using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "HitBox")
        {
            this.gameObject.SetActive(false);

            Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
            userInfo["increment"] = 1;
            NotificationCenter.Instance.postNotification(new Notification("IncreaseScore", this, userInfo));
        }
    }
}