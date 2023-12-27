using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject instanceBullet;
    public float attackTime = 2f;
    public bool isAttack=false;
    public GameObject BulletContraller;
    public AudioClip shootClip;
    private AudioSource source;
    public GameObject attackTarget;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            time += Time.deltaTime;
            if (time>=attackTime) {
                Attacking();
                time = 0;
            }
        }
    }

    public void Attacking()
    {
        //��ʼ��һ���ӵ����Ž��ӵ���������
        GameObject newBullet= Instantiate(instanceBullet, transform.position, transform.rotation, BulletContraller.transform);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        //��ǹ��Ч
        source.PlayOneShot(shootClip);
        //�����ӵ�Ŀ��
        bullet.target = attackTarget;
        //�������
        isAttack = false;
    }

    
}
