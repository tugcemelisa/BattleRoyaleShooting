using UnityEngine;

public class Inv_RotateObject : MonoBehaviour
{
    float rotationSpeed = 50f;
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
