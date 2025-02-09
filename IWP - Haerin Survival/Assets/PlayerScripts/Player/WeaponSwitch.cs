using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    [SerializeField] Rifle _Rifle;
    [SerializeField] Rifle _Glock;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                _Rifle.UpdateAmmoUI();
                _Glock.UpdateAmmoUI();
                selectedWeapon = 0;
            }
            else
            {
                _Rifle.UpdateAmmoUI();
                _Glock.UpdateAmmoUI();
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)    
            {
                _Rifle.UpdateAmmoUI();
                _Glock.UpdateAmmoUI();
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                _Rifle.UpdateAmmoUI();
                _Glock.UpdateAmmoUI();
                selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
