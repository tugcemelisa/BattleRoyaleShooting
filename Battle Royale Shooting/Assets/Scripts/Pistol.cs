using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : Weapon
{
    void Start()
    {
        cooldown = 0;
        auto = false;
        ammoCurrent = 10;
        ammoMax= 10;
        ammoBackPack = 30;
    }

    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);        
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            if(hit.collider.CompareTag("enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
            }
            Destroy(gameBullet, 1);
        }
    }
}
