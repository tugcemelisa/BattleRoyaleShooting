using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Pistol
{    
    void Start()
    {
        cooldown = 0.2f;
        auto = true;
    }
}
