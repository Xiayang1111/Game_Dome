using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanks : MonoBehaviour
{
    private int tankNum;
    public bool isEmptyTank;
    // Start is called before the first frame update
    void Start()
    {
        tankNum = transform.childCount;
        if (tankNum > 0) isEmptyTank = false;
    }

    // Update is called once per frame
    void Update()
    {
        tankNum = transform.childCount;
        if (tankNum<=0)
        {
            isEmptyTank = true;
        }
    }
}
