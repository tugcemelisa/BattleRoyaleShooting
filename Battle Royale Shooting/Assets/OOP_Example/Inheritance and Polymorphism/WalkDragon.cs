using UnityEngine;
public class WalkDragon : Dragons
{
    private void Start()
    {
        name = "Walk dragon";
    }
    protected override void Voice()
    {
        print("Fire Impact, Red Dragon!");
    }
}
