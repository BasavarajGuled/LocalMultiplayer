using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public int selectedWeapon = 0;
    public WeaponPickDrop weaponPickDrop;
    // Start is called before the first frame update
    void Start()
    {
        //SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        MouseWheelSelectWeapon();
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
               
                weapon.gameObject.SetActive(true);
                weapon.GetComponent<Gun>().enabled = true;
                weaponPickDrop.CurrentGun = weapon.gameObject;
                UIController.instance.SetGunDisplay(true);
                UIController.instance.ShowGunName(weapon.GetComponent<Gun>().gunName);
                UIController.instance.ShowMagazinSize("magazin " + weapon.GetComponent<Gun>().TempMagazinSize.ToString() + "/" + weapon.GetComponent<Gun>().magazinSize.ToString());
            }
            else
            {
                weapon.gameObject.SetActive(false);
                weapon.GetComponent<Gun>().enabled = false;
            }
            i++;
        }
    }

    private void MouseWheelSelectWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
                SelectWeapon();
            }
            else
            {
                selectedWeapon++;
                SelectWeapon();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
                SelectWeapon();
            }
            else
            {
                selectedWeapon--;
                SelectWeapon();
            }
        }
    }

}
