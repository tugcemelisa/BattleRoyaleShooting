using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragons : MonoBehaviour
{
    protected string name;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)) 
        {
            Voice();
        }
    }
    protected virtual void Voice()
    {

    }
    public string PlayWithMe() 
    {
        return name;
    }
}
