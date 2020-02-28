using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    public enum State
    {
        CHASE,
        INVESTIGATE
    }

    public State state;
    private bool alive;

    //Investigating
    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;

    //Sight
    public float heightMultiplier;
    public float sightDist = 10;

    // Start is called before the first frame update
    void Start()
    {
        heightMultiplier = 1.36f;

        StartCoroutine("FSM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.INVESTIGATE:
                    Investigate();
                    break;
            }
            yield return null;
        }
    }

    void Investigate()
    {
        timer += Time.deltaTime;
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.yellow);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.yellow);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.yellow);

        if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                state = EnemySight.State.CHASE;
                //target = hit.collider.gameObject;
            }
        }
    }
}
