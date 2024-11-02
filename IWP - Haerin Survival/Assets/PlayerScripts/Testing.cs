using UnityEngine;

public class Testing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Gamemanager._instance.IsPlayerWalking(); // This should be called when the player enters the trigger
    }

}
