using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Text gunName;
    public Text magazinSize;

    public GameObject gunDisplay;
    public Button dropGun;

    public WeaponPickDrop pickDrop;

    public List<GameObject> weapons;
    public SelectWeapon weaponButton;
    public Transform weaponUIHolder;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        dropGun.interactable = false;
        dropGun.onClick.AddListener(delegate
        {
            print("clicked");
            Drop();
        });
    }

    public void ShowGunName(string name)
    {
        gunName.text = name;
    }

    public void ShowMagazinSize(string size)
    {
        magazinSize.text = size;
    }

    public void SetGunDisplay(bool isActive)
    {
        gunDisplay.SetActive(isActive);
    }

    public void Drop()
    {
        pickDrop.Drop();
    }

    public void Fire(Gun gun)
    {
        if (gun != null)
        {
            gun.Fire();
        }
    }

    public void RemoveWeaponButton(int weaponNumber)
    {
        Destroy(weapons[weaponNumber].gameObject);
        weapons.RemoveAt(weaponNumber);
    }
}
