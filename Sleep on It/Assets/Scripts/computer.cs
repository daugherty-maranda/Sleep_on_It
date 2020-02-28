using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("camcam");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            Dictionary<string, System.Object> userInfo = new Dictionary<string, System.Object>();
            
        }
    }
}
