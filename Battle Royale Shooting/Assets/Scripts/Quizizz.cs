using UnityEngine;
public class Quizizz : MonoBehaviour
{
    int a = 10;
    int b = 6;
    void Start()
    {
        b += a; 
        a += b; 
        print(a);
    }
}
