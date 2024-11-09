using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript glock;
    [SerializeField] private Transform headTransform;
    [SerializeField] private GameObject Head;
    [SerializeField] private Gamemanager gamemanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // No need to assign spawnedGun here, we can directly use glock in the spawn method
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // Call SpawnGuns to spawn the glock in the head transform
            gamemanager.SpawnGuns(glock, headTransform);
        }
    }
}
