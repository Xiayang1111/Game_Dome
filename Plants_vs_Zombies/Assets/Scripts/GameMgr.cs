using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;
    private float sunNum;

    //���������
    public float GetSunNum()
    {
        return sunNum;
    }

    //�޸�������
    public void ChangeSunNum(float num)
    {
        if (sunNum+num<0) return;
        sunNum += num;//����������
        UIMgr.Instance.ChangSunNum(sunNum);//UI������
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
