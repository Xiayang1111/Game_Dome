using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    NavMeshAgent nav;
    public GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray,out hit))//射线检测
            {
                Debug.Log("射线未与可导航面相交");
                return;
            }
            Debug.Log(hit.point);
            nav.destination = hit.point;
            if (nav.pathStatus==NavMeshPathStatus.PathInvalid)//该点为无效路径
            {
                Debug.Log("目标地点无效");
                return;
            }
        }
        
    }


}
