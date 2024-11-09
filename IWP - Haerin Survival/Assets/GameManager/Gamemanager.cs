using UnityEngine;
using System;

public class Gamemanager : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript gunModel;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject Head;
    private GameObject spawnedGun;

    [Header("Event System")]
    public static Gamemanager _instance;

    private void Awake()
    {
        SpawnGun();
        _instance = this;
       
    }

    public event Action _iswalking;
    //public event Action _isReloding;
    private void Update()
    {
        RotateGun();
    }
    public void IsPlayerWalking()
    {
        // Invoke the event if there are any subscribers
        _iswalking?.Invoke();
    }

    public void SpawnGun()
    {
        spawnedGun = Instantiate(gunModel._SpawnGun);
        spawnedGun.transform.SetParent(headTransform);
        spawnedGun.transform.localPosition = gunModel.gunPosition;
        spawnedGun.transform.localRotation = gunModel.gunRotation;
    }

    public void SpawnGuns(BaseGunScript _gunModel, Transform _headTrans)
    {
        if (_gunModel._SpawnGun == null)
        {
            Debug.LogError("Gun prefab not assigned in BaseGunScript!");
            return; 
        }

        GameObject _gun = Instantiate(_gunModel._SpawnGun);
        _gun.transform.SetParent(_headTrans);
        _gun.transform.localPosition = _gunModel.gunPosition;
        _gun.transform.localRotation = _gunModel.gunRotation;
    }


    private void RotateGun()
    {
        // Get the camera's rotation
        Quaternion cameraRotation = _camera.transform.rotation;

        // Create a new rotation using the camera's X and Y values, with Z fixed at 90 degrees
        Quaternion gunRotation = Quaternion.Euler(cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z);

        // Set the gun's rotation
        Head.transform.rotation = gunRotation;
    }
}
