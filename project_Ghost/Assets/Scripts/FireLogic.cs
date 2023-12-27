using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLogic : MonoBehaviour
{
    public float fireSpeed;
    public float bulletsDistance;
    public GameObject bullet;
    public GameObject bulletBirth;
    public GameObject bulletList;

    float fireTime;
    // Start is called before the first frame update
    void Start()
    {
        fireTime = bulletsDistance / fireSpeed;
        if (fireSpeed>0) {
            InvokeRepeating("fire", 0.1f, fireTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet,bulletList.transform);
            newBullet.transform.position = bulletBirth.transform.position;
            newBullet.transform.eulerAngles = bulletBirth.transform.eulerAngles;
        }
        
    }
}
