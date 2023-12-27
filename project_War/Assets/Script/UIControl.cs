using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    //��Ϸ��������Ĳ���
    public GameObject gameOverPanel;
    //��Ϸ��ʼ����Ĳ���
    public GameObject gameStartPanel;

    //�ű��ձ�����ʱʹ��
    private void OnEnable()
    {
        //�ָ���Ϸ�ٶ���������
        Time.timeScale = 1;
        //ע����Ϸ������ί��
        EventHander.GetGameOverEvent += OnGetGameOverEvent;
        //ע����Ϸ��ʼ��ί��
        EventHander.GetGameStartEvent += OnGetGameStartEvent;
    }

    private void OnGetGameStartEvent()
    {
        //�ر���Ϸ��ʼ����
        gameStartPanel.SetActive(false);
        //�����Ϸ��ʼ���汻��ʾ(�ָ���Ϸʱ��)
        if (!gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 1;
        }
    }

    private void OnGetGameOverEvent()
    {
        //��ʾ��Ϸ��������
        gameOverPanel.SetActive(true);
        //�����Ϸ�������汻��ʾ
        if (gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    private void OnDisable()
    {
        EventHander.GetGameOverEvent -= OnGetGameOverEvent;
        //ע����Ϸ��ʼ��ί��
        EventHander.GetGameStartEvent -= OnGetGameStartEvent;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
