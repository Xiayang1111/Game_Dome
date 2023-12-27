using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHander : MonoBehaviour
{
    public static event Action GetGameOverEvent;//��ȡ��Ϸ�����¼�
    public static event Action GetGameStartEvent;//��ȡ��Ϸ��ʼ�¼�

    public static void CallGetGameOverEvent()//֪ͨ��ȡ�¼�
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
