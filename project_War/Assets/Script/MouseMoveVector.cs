using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveVector : MonoBehaviour
{
    public static Vector3 mVector=Vector3.zero;
    private Vector3 nowVector = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (nowVector==Vector3.left)
            {
                nowVector = Input.mousePosition;
            }
            else
            {
                mVector = Input.mousePosition - nowVector;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            nowVector = Vector3.left;
            mVector = Vector3.zero;
        }
    }
}
