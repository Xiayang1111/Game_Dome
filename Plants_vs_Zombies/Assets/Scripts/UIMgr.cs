using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance;

    //UI�е�����
    public Text sunNum;

    //����UI�е���������
    public void ChangSunNum(float num)
    {
        sunNum.text = num.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        sunNum = GameObject.Find("SunNum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
