using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLogic : MonoBehaviour
{
    public float changeTime=1f;
    public float showTime = 5f;
    public GameObject John;
    public CanvasGroup group_win;
    public CanvasGroup group_fail;

    public AudioClip winClip;
    public AudioClip failClip;

    float time = 0f;

    public bool isExit_win = false;
    public bool isExit_fail = false;

    public AudioSource Source;

    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isExit_win==true)
        {
            Gradient(group_win);
        }else if (isExit_fail == true)
        {
           Gradient(group_fail);
        }
    }

    public void Gradient(CanvasGroup group)
    {
        if (num==0)
        {
            PlayMusic();
        }
        num++;
        time += Time.deltaTime;
        group.alpha += time;
        if (time>changeTime+showTime)
        {
            EndGame();
        }
    }

    public void PlayMusic()
    {
            if (isExit_win == true)
            {
                //music_game.Pause();
                Source.PlayOneShot(winClip);
            }
            else if (isExit_fail == true)
            {
                //music_game.Pause();
                Source.PlayOneShot(failClip);
            }
        
    }

    public void EndGame()
    {
        if (isExit_win==true)
        {
            Application.Quit();
        }
        
    }
}
