using UnityEngine;
public class OtherClass : MonoBehaviour
{
    [SerializeField] MainClass mainClass;
    void Start()
    {
        print("!Green Dragon! " + mainClass.GetName());
        print("!Green Dragon! " + mainClass.dragonAbility);
        print("!Green Dragon! " + mainClass.GetClass());
    }
}
