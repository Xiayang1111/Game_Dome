using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseContent(GameObject g)
    {
        g.SetActive(false);
    }

    public void OpenContent(GameObject g)
    {
        g.SetActive(true);
    }
}
