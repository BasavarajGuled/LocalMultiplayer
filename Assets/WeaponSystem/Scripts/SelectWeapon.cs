using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public Outline outLine;
    public int weaponNumber;
    public WeaponHolder holder;

    private Button selectButton;
    public Text weaponName;

    private void Start()
    {
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(delegate
        {
            Selected();
        });
    }

    public void Selected()
    {
        holder.selectedWeapon = weaponNumber;
        holder.SelectWeapon();
        outLine.enabled = true;
        for(int i = 0; i < UIController.instance.weapons.Count; i++)
        {
            if(UIController.instance.weapons[i].GetComponent<SelectWeapon>().weaponNumber != weaponNumber)
            {
                UIController.instance.weapons[i].GetComponent<SelectWeapon>().ShowSelected(false);
            }
        }
    }

    public void ShowSelected(bool isSelected)
    {
        outLine.enabled = isSelected;
    }

    public void SetText(string name)
    {
        weaponName.text = name;
    }
}
