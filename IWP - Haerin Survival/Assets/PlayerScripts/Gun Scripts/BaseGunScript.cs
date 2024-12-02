using UnityEngine;

[CreateAssetMenu(fileName = "BaseGunScript", menuName = "Scriptable Objects/BaseGun")]
public class BaseGunScript : ScriptableObject
{
    [Header("Gun Variables")]
    public string _GunName;
    public int _CurrentAmmo;
    public int _MaxAmmo;
    public int _Damage;
    public int _ReloadTime;
    public float _FireRate;
    public float _LastShot;
    public float _NextFireTime;

    [Header("Gun Position")]
    public GameObject _SpawnGun;
    public Vector3 gunPosition = new Vector3();
    public Vector3 firePointPos = new Vector3();
    public Quaternion firePointRotation = new Quaternion();
    public Quaternion gunRotation = new Quaternion();
    public Vector3 FirePointOffset; // Relative position offset for FirePoint

}
