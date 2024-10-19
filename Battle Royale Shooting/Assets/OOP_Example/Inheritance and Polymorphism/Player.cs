using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Dragons dragons = other.GetComponent<Dragons>();
        dragons.PlayWithMe();

    }
}
