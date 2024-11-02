using UnityEngine;

[CreateAssetMenu(fileName = "BaseGunScript", menuName = "Scriptable Objects/BaseGun")]
public class BaseGunScript : ScriptableObject
{ 
    public string _GunName;
    public int _CurrentAmmo;
    public int _MaxAmmo;
    public int _Damage;
    public int _ReloadTime;
    public float _FireRate;

    public GameObject _GunModel;


}
