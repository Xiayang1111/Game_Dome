using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;
    private float sunNum;

    //获得阳光数
    public float GetSunNum()
    {
        return sunNum;
    }

    //修改阳光数
    public void ChangeSunNum(float num)
    {
        if (sunNum+num<0) return;
        sunNum += num;//数据区更新
        UIMgr.Instance.ChangSunNum(sunNum);//UI区更新
    }

    // Start is called before the first frame update
    void Start()
    {
        sunNum = 450;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
