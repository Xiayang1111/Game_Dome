using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoNavigate : MonoBehaviour
{
    public float moveRange=20f;
    public GameObject Plane;
    public float maxDistance=6f;
    public float goToHumanTime = 10f;
    public RaycastHit[] hits=new RaycastHit[5];//不应该显示

    private NavMeshAgent agent;
    private Vector3 randomPoint;

    float time=0;
    public bool isHuman = false;//不应该显示
    private int index;//射线下标
    float goTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Navigate();
    }

    // Update is called once per frame
    void Update()
    {
        //检测到主角自动导航
        if (isHuman == true)
        {
            goTime += Time.deltaTime * 1;
            if (index != 0) NavigateToHuman(hits[index]);
            if (goTime>=goToHumanTime)
            {
                goTime = 0;
                isHuman = false;
            }
        }
        //未检测到主角自动导航
        else
        {
            index=checkedHuman();
            if (isHuman==true) return;
            time += Time.deltaTime * 1;
            if (time >= 10)
            {
                Navigate();
                time = 0;
            }
        }
        
    }

    private void NavigateToHuman(RaycastHit hit)
    {
        agent.SetDestination(hit.transform.position);
    }

    private int checkedHuman()
    {
        Physics.Raycast(transform.position, transform.forward, out hits[1], maxDistance);
        Physics.Raycast(transform.position, -transform.forward, out hits[2], maxDistance);
        Physics.Raycast(transform.position, transform.right, out hits[3], maxDistance);
        Physics.Raycast(transform.position, -transform.right, out hits[4], maxDistance);
        for(int i=1;i<=4;i++) {
            if (hits[i].collider && hits[i].transform.name == "Human")
            {
                isHuman = true;
                return i;
            }
        }
        return 0;
    }

    private void Navigate()
    {
        randomPoint=Plane.transform.position+Random.insideUnitSphere*moveRange;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint,out hit, 20, 1);//检测可到达性，hit返回20米范围内随机一个可达位置或者是randomPiont
        agent.SetDestination(hit.position);
        //Debug.Log(hit.position);
    }


}
