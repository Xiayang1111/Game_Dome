using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//点击使人物移动到指定地点
public class Moving : MonoBehaviour
{
    private float speed = 0.10f;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mVector = MouseMoveVector.mVector;
        if (mVector == Vector3.zero) { return; }
        Vector3 worldYDirection = Camera.main.transform.TransformDirection(Vector3.up);
        Vector3 yDirection = new Vector3(worldYDirection.x, 0, worldYDirection.z).normalized * mVector.y;
        Vector3 worldXDirection = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 xDirection = new Vector3(worldXDirection.x,0,worldXDirection.z).normalized*mVector.x;
        Vector3 distance = (yDirection + xDirection).normalized*speed;
        distance.y = -100f;
        cc.Move(distance);
    }

}
