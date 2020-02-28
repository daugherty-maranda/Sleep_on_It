using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour
{
    [SerializeField] private float range = 5f;

    GameObject player;

    private Transform grate;

    private void OnMouseDown()
    {
        //This is for the player
        player = GameObject.FindWithTag("Player");

        grate = this.transform;

        if (Vector3.Distance(player.transform.position, grate.position) <= range)
        {
            Destroy(gameObject);
        }
    }
}
