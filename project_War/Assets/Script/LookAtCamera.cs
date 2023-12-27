using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Human").transform.Find("head").transform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
