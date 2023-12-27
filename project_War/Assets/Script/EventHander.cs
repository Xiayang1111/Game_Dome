using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHander : MonoBehaviour
{
    public static event Action GetGameOverEvent;//获取游戏结束事件
    public static event Action GetGameStartEvent;//获取游戏开始事件

    public static void CallGetGameOverEvent()//通知获取事件
    {
        GetGameOverEvent?.Invoke();
    }

    public static void CallGetGameStartEvent()
    {
        GetGameStartEvent?.Invoke();
    }

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
