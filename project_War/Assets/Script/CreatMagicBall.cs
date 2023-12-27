using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMagicBall : MonoBehaviour
{
    public GameObject[] InstantObjects;
    public float InstantTime = 10f;
    public int MaxNum=5;

    float time=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>=InstantTime&&transform.childCount<MaxNum)
        {
            Instantiate(InstantObjects[Random.Range(0,InstantObjects.Length)],transform.position+new Vector3(Random.Range(0,30), Random.Range(0, 20), Random.Range(0, 30)),transform.rotation,transform);
            time = 0;
        }
    }


}
