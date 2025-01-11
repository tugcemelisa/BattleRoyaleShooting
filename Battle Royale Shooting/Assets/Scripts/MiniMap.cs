using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviourPunCallbacks
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float minValue = 10f;
    [SerializeField] private float maxValue = 60f;
    private float currentValue;
    void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
        currentValue = 20f;
    }

    private void Update()
    {
        
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        // print(scrollDelta);

        
        if (scrollDelta > 0)
        {
            currentValue += scrollSpeed;
        }
        else if (scrollDelta < 0)
        {
            currentValue -= scrollSpeed;
        }

       
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
        gameObject.GetComponent<Camera>().orthographicSize = currentValue;
    }
}