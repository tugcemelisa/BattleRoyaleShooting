using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    [SerializeField] protected GameObject cam;
    protected int ammoCurrent;
    protected int ammoMax;
    protected int ammoBackPack;
    [SerializeField] TMP_Text ammoText;
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
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        AmmoTextUpdate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ammoCurrent != ammoMax || ammoBackPack != 0)
            {
                Invoke("Reload", 1);
            }
        }       
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                if(ammoCurrent > 0) 
                { 
                    OnShoot();
                    timer = 0;
                    ammoCurrent = ammoCurrent - 1;
                }
            }           
        }
    }
    protected virtual void OnShoot()
    {
    }
    private void AmmoTextUpdate()
    {
        ammoText.text = ammoCurrent + " / " + ammoBackPack;
    }

    private void Reload()
    {
        int ammoNeed = ammoMax - ammoCurrent; 
        if (ammoBackPack >= ammoNeed) 
        {
            ammoBackPack -= ammoNeed;
            ammoCurrent += ammoNeed;
        }
        else 
        {
            ammoCurrent += ammoBackPack;
            ammoBackPack = 0;
        }
    }
}
