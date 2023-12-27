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
            if (!Physics.Raycast(ray,out hit))//���߼��
            {
                Debug.Log("����δ��ɵ������ཻ");
                return;
            }
            Debug.Log(hit.point);
            nav.destination = hit.point;
            if (nav.pathStatus==NavMeshPathStatus.PathInvalid)//�õ�Ϊ��Ч·��
            {
                Debug.Log("Ŀ��ص���Ч");
                return;
            }
        }
        
    }


}
