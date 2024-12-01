using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    //Scripts 
    [SerializeField] Transform Origin;


    //Bools
    private bool isAiming;

    //Rotations
    private Vector3 currentRotations;
    private Vector3 targetRotations;

    //HipFire Recoil
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] protected float recoilZ;

    //Ads Recoil
    [SerializeField] private float aimrecoilX;
    [SerializeField] private float aimrecoilY;
    [SerializeField] protected float aimrecoilZ;

    //Setting
    [SerializeField] private float snappiness;
    [SerializeField] private float returnspeed;


    void Update()
    {
        //isAiming = GunSystem_Script.isADS;
        //isAiming = AimSettings.isAiming;

        targetRotations = Vector3.Lerp(targetRotations, Vector3.zero, returnspeed * Time.deltaTime);
        currentRotations = Vector3.Slerp(currentRotations, targetRotations, snappiness * Time.deltaTime);
        Origin.localRotation = Quaternion.Euler(currentRotations);
    }


    public void RecoilFire()
    {
        if (isAiming)
        {
            Debug.Log("IsAiming");
            targetRotations += new Vector3(aimrecoilX, Random.Range(-aimrecoilY, aimrecoilY), Random.Range(-aimrecoilZ, aimrecoilZ));
        }
        else
        {
            Debug.Log("IsNotAiming");
            targetRotations += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }

    }
}
