using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanning : MonoBehaviour
{
    public GargoyleLogic Gargoyle;
    public EndLogic end;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (this.transform.parent.gameObject.name=="C"&&other.gameObject.name == "JohnLemon")
        {
            Gargoyle.isCall = true;
        }
        else if(this.transform.parent.gameObject.name == "C (1)" && other.gameObject.name == "JohnLemon")
        {
            NavLogic nav = this.transform.parent.parent.GetComponent<NavLogic>();
            nav.flog= true;
        }
    }
}
