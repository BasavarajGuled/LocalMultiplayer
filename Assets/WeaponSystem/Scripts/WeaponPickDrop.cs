using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickDrop : MonoBehaviour
{
    private CharacterController controller;
    public Transform weaponHolder;

    private GameObject currentGun;
    public GameObject CurrentGun { set { currentGun = value; } }

    private bool isPicked;
    private string otherName = string.Empty;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentGun != null)
        {
            Drop();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (!isPicked)
        {
            if (other.gameObject.tag.Equals("Gun"))
            {

                if (otherName != other.gameObject.name)
                {
                    isPicked = true;
                    Pickup(other);
                }
            }
        }
    }

    private void Pickup(ControllerColliderHit other)
    {
        otherName = other.gameObject.name;
        GameObject pickedGun = other.gameObject;
        pickedGun.transform.SetParent(weaponHolder);

        //other.gameObject.GetComponent<Gun>().enabled = true;
        if (weaponHolder.childCount < 3)
        {
            pickedGun.transform.localPosition = Vector3.zero;
            pickedGun.transform.localRotation = Quaternion.identity;
            pickedGun.GetComponent<Rigidbody>().isKinematic = true;
            pickedGun.GetComponent<BoxCollider>().isTrigger = true;
            isPicked = false;
            UIController.instance.dropGun.interactable = true;
            if (weaponHolder.childCount > 0 && weaponHolder.childCount < 2)
            {
                currentGun = pickedGun;
                currentGun.GetComponent<Gun>().enabled = true;
                weaponHolder.GetComponent<WeaponHolder>().enabled = true;
                weaponHolder.GetComponent<WeaponHolder>().SelectWeapon();
                SelectedWeapon(true, currentGun.gameObject.GetComponent<Gun>().gunName);
                UIController.instance.SetGunDisplay(true);
                UIController.instance.ShowGunName(currentGun.GetComponent<Gun>().gunName);
                UIController.instance.ShowMagazinSize("magazin " + currentGun.GetComponent<Gun>().TempMagazinSize.ToString() + "/" + currentGun.GetComponent<Gun>().magazinSize.ToString());
            }
            else if (weaponHolder.childCount > 1)
            {
                pickedGun.SetActive(false);
                other.gameObject.GetComponent<Gun>().enabled = false;
                SelectedWeapon(false, other.gameObject.GetComponent<Gun>().gunName);
            }

            if (weaponHolder.childCount < 1)
            {
                weaponHolder.GetComponent<WeaponHolder>().enabled = false;
            }
        }
        else
        {
            pickedGun.transform.SetParent(null);
        }

    }

    public void Drop()
    {

        currentGun.gameObject.GetComponent<Gun>().enabled = false;
        currentGun.GetComponent<Rigidbody>().isKinematic = false;
        currentGun.GetComponent<BoxCollider>().isTrigger = false;
        currentGun.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);
        currentGun.GetComponent<Rigidbody>().AddForce(transform.up * 100f);
        UIController.instance.RemoveWeaponButton(currentGun.transform.GetSiblingIndex());
        currentGun.transform.SetParent(null);
        isPicked = false;
        otherName = string.Empty;
        if (weaponHolder.childCount > 0)
        {
            UIController.instance.SetGunDisplay(true);
            weaponHolder.GetComponent<WeaponHolder>().selectedWeapon = 0;
            weaponHolder.GetComponent<WeaponHolder>().SelectWeapon();
            UIController.instance.weapons[0].GetComponent<SelectWeapon>().weaponNumber = weaponHolder.GetComponent<WeaponHolder>().selectedWeapon;
            UIController.instance.weapons[0].GetComponent<SelectWeapon>().ShowSelected(true);
            UIController.instance.dropGun.interactable = true;
        }
        else
        {
            currentGun = null;
            UIController.instance.SetGunDisplay(false);
            UIController.instance.dropGun.interactable = false;
        }
    }

    public void SelectedWeapon(bool isSelected, string name)
    {
        SelectWeapon weapon = Instantiate(UIController.instance.weaponButton, UIController.instance.weaponUIHolder);
        weapon.weaponNumber = weaponHolder.childCount - 1;
        weapon.holder = weaponHolder.GetComponent<WeaponHolder>();
        weapon.SetText(name);
        UIController.instance.weapons.Add(weapon.gameObject);
        weapon.ShowSelected(isSelected);
    }

}
