using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public HumanControl humanControl;
    public TankControl tankControl;
    public UIControl UIControl;
    public CameraControl cameraControl;
    public Tanks tanks;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //游戏结束
        if (humanControl.isdead||tanks.isEmptyTank)
        {
            GameOver();
            return;
        }
    }

    //游戏开始
    public void GameStart()
    {
        EventHander.CallGetGameStartEvent();
        AbleInput();
    }

    //游戏结束
    public void GameOver()
    {
        cameraControl.ToShowCursor();
        EventHander.CallGetGameOverEvent();
        DisableInput();
    }

    //重启游戏
    public void Restart()
    {
        //重新加载之前活跃过的场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //停止游戏
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//在Unity编译器中结束运行
        #else
            Application.Quit();//在可执行程序中结束运行
        #endif
    }

    //停止动作
    private void DisableInput()
    {
        cameraControl.ToShowCursor();//开启鼠标
        tankControl.enabled = false;
    }

    //开启动作
    private void AbleInput()
    {
        cameraControl.ToShowCursor();//开启鼠标
        tankControl.enabled = true;
    }
}
