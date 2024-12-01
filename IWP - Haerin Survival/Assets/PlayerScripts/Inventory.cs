using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Transform _primary;
    private Transform _secondary;
    private Transform _melee;
    private Transform[] _weapons;

    enum WeaponSlot
    {
        PRIMARY = 0,
        SECONDARY,
        MELEE
    }

    private void Awake()
    {
        _weapons = new[]
        {
            _primary,
            _secondary,
            _melee,
        };
    }

    private void ShowWeapon(WeaponSlot index)
    {
        //Disable all weapons
        foreach (var wep in _weapons)
        {
            wep.gameObject.SetActive(false);
        }
        //Re-Enable the weapon we want
        _weapons[(int)index].gameObject.SetActive(true);
    }
}
