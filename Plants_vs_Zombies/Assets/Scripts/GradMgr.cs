using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradMgr : MonoBehaviour
{
    public static GradMgr Instance;
    //第一个格子
    private GameObject grad;

    public void CreateGrad()
    {
        grad = new GameObject();
        grad.transform.position = transform.position;
        grad.AddComponent<BoxCollider2D>();
        grad.GetComponent<BoxCollider2D>().size = new Vector2(70, 90);
        grad.GetComponent<BoxCollider2D>().isTrigger = true;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject newGrad = GameObject.Instantiate(grad);
                newGrad.transform.parent = transform;
                newGrad.transform.position = transform.position + new Vector3(j * 82.7f, i * 100f, 0);
                newGrad.name = i + "_" + j;
                newGrad.tag = "grad";

            }
        }
        Destroy(grad);
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CreateGrad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
