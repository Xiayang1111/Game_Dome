using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private bool isHide=false;
    public HumanControl humanControl;

    private void Awake()
    {
        ToShowCursor();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            isHide = !isHide;
            if (isHide == false) ToHideCursor();
            else ToShowCursor();
        }
    }

    public void ToHideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        humanControl.enabled = true;
    }

    public void ToShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        humanControl.enabled = false;
        
    }
}
