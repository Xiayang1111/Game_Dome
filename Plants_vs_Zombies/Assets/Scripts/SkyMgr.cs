using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMgr : MonoBehaviour
{
    //创建一个公开的SkyMgr便于访问
    public static SkyMgr instance;

    //掉落范围
    private float minX =-410f;
    private float minY =-240f;
    private float maxX =250f;
    private float maxY =170f;

    //太阳预制体
    private GameObject sunPre;

    //创造太阳
    public void CreateSun()
    {
        GameObject sunObj = GameObject.Instantiate(sunPre);//自定义玩家生成物体
        Sun sun = sunObj.GetComponent<Sun>();//从物体中获取物体脚本
        float x = Random.Range(minX,maxX);
        float y = Random.Range(minY,maxY);

        sun.InitSun(x,transform.position.y,y);
        sun.transform.parent = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //获取太阳预制体
        sunPre = Resources.Load<GameObject>("Prefabs/Sun");
        //每隔一段时间生成一个太阳
        InvokeRepeating("CreateSun",2,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
