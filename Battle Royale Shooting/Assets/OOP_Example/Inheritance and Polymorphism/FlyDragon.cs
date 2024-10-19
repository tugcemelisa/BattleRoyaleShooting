using UnityEngine;

public class FlyDragon : Dragons
{
    private void Start()
    {
        name = "Fly dragon";
    }
    protected override void Voice()
    {
        print("Grrrrr, Green Dragon!");
    }
}
