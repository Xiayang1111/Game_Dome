using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleLogic : MonoBehaviour
{
    public GameObject Ghost;
    public GameObject GhostBrithPlace;
    public GameObject GhostList;
    
    public float changSpeed = 2f;
   
    
    public int CallNum = 2;
    public bool isCall = false;

    float y1;
    float y2 = 0f;
    int num = 1;
    int GhostNum=0;
    // Start is called before the first frame update
    void Start()
    {
        y1 = this.gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCall==true) {
            Change();
            CallGhost();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "JohnLemon") {
            isCall = true;
        }
    }

    public void CallGhost()
    {
        if (GhostNum < CallNum)
        {
            GameObject newGhost = Instantiate(Ghost, GhostList.transform);
            newGhost.transform.position = GhostBrithPlace.transform.position;
            newGhost.transform.localEulerAngles = GhostBrithPlace.transform.localEulerAngles;
            GhostNum++;
        }
    }
        public void Change()
    {
        if (num==1) {
            if (y2 < 0.2) y2 += changSpeed * Time.deltaTime;
            else num = 0;
        }
        else
        {
            if (y2 > 0) y2 += -changSpeed * Time.deltaTime;
            else num = 1;
        }
        this.transform.localScale = new Vector3(this.transform.localScale.x, y1 + y2, this.transform.localScale.z);
    }

   
}
