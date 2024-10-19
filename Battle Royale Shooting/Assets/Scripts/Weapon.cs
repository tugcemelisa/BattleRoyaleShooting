using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    [SerializeField] protected GameObject cam;

    protected bool auto = false;
    protected float cooldown = 0;

    private float timer = 0;

    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0)) Shoot();
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                OnShoot();
                timer = 0;
            }
        }
    }

    protected virtual void OnShoot()
    {

    }
    
}
