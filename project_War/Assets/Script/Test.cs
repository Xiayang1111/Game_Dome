using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private bool isGround;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("StaticSpace");
        Debug.Log("�ò�Ϊ��"+layerMask);
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 2.3f, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * 2.3f);
        Debug.Log("�Ƿ��ڵ���" + isGround);
    }
}
