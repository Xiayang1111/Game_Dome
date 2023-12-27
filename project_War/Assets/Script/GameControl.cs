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
        //��Ϸ����
        if (humanControl.isdead||tanks.isEmptyTank)
        {
            GameOver();
            return;
        }
    }

    //��Ϸ��ʼ
    public void GameStart()
    {
        EventHander.CallGetGameStartEvent();
        AbleInput();
    }

    //��Ϸ����
    public void GameOver()
    {
        cameraControl.ToShowCursor();
        EventHander.CallGetGameOverEvent();
        DisableInput();
    }

    //������Ϸ
    public void Restart()
    {
        //���¼���֮ǰ��Ծ���ĳ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //ֹͣ��Ϸ
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//��Unity�������н�������
        #else
            Application.Quit();//�ڿ�ִ�г����н�������
        #endif
    }

    //ֹͣ����
    private void DisableInput()
    {
        cameraControl.ToShowCursor();//�������
        tankControl.enabled = false;
    }

    //��������
    private void AbleInput()
    {
        cameraControl.ToShowCursor();//�������
        tankControl.enabled = true;
    }
}
