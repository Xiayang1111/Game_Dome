using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    //游戏结束界面的操作
    public GameObject gameOverPanel;
    //游戏开始界面的操作
    public GameObject gameStartPanel;

    //脚本刚被调用时使用
    private void OnEnable()
    {
        //恢复游戏速度正常进行
        Time.timeScale = 1;
        //注册游戏结束的委托
        EventHander.GetGameOverEvent += OnGetGameOverEvent;
        //注册游戏开始的委托
        EventHander.GetGameStartEvent += OnGetGameStartEvent;
    }

    private void OnGetGameStartEvent()
    {
        //关闭游戏开始界面
        gameStartPanel.SetActive(false);
        //如果游戏开始界面被显示(恢复游戏时间)
        if (!gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 1;
        }
    }

    private void OnGetGameOverEvent()
    {
        //显示游戏结束界面
        gameOverPanel.SetActive(true);
        //如果游戏结束界面被显示
        if (gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    private void OnDisable()
    {
        EventHander.GetGameOverEvent -= OnGetGameOverEvent;
        //注册游戏开始的委托
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
