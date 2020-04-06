using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    [SerializeField] Transform rotator;
    [SerializeField] Transform firePoint;

    [SerializeField] float coolDown = 1;
    private float currentFireTime = 1;
    [SerializeField] float fireForce = 500;
    [SerializeField] int damage = 1;


    [SerializeField] BulletController bullet;

    void Start()
    {
        if(target == null)
        {
            //target = GameObject.FindObjectOfType<PlayerController>().transform
        }
    }

    void Update()
    {
        //aim
        rotator.LookAt(target);
        //shooting

        if(currentFireTime >= 0)
        {
            currentFireTime -= Time.deltaTime;
        }
        else
        {
            currentFireTime = coolDown;
            Shoot();
        }
    }

    void Shoot()
    {
        BulletController bull = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bull.speed = fireForce;
        bull.damage = damage;
    }
}
