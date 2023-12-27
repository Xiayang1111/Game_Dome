using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavLogic : MonoBehaviour
{
    NavMeshAgent NMA;
    GameObject player;

    public Transform[] NavPoints;
    int NavIndex=0;

    public float moveSpeed = 2f;

    public bool flog = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("JohnLemon");
        NMA=this.GetComponent<NavMeshAgent>();

        NMA = this.GetComponent<NavMeshAgent>();
        if(NavPoints.Length==0) NMA.SetDestination(player.transform.position);
        else NMA.SetDestination(NavPoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (NavPoints.Length != 0)
        {
            Patrol();
            Chase();
        }
        else NMA.SetDestination(player.transform.position);
        
    }

    public void Patrol()
    {
        if (flog==false) {
            if (NMA.remainingDistance <= NMA.stoppingDistance)
            {
                NavIndex = (NavIndex + 1) % NavPoints.Length;
                NMA.SetDestination(NavPoints[NavIndex].position);
            }
        }
    }

    public void Chase()
    {
        if (flog == true)
        {
            NMA.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="JohnLemon")
        {
            flog = true;
        }
    }
}
