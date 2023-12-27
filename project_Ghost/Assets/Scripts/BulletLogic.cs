using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public GameObject explosionEffect;
    //public GameObject[] monstList;
    float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = maxDistance / speed;
        if(speed!=0) Invoke("destroy",lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        this.transform.Translate(0,0,speed*Time.deltaTime,Space.Self);
    }

    public void destroy()
    {
        Object.Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.name =="Ghost"||other.name=="Gargoyle")
        {

            GameObject effect = Instantiate(explosionEffect, null);
            effect.transform.position = other.transform.position;
            effect.transform.localEulerAngles = other.transform.localEulerAngles;

            Object.Destroy(other.gameObject);
            Object.Destroy(this.gameObject);
        }
        
    }
}
