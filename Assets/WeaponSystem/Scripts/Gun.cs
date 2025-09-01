using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public string gunName;
    public Transform spawnPoint;
    public float fireRate = 0.5f;
    public int magazinSize;
    public float reloadTime;

    private float nextFire = 0.0f;
    private int tempMagazinSize;
    public int TempMagazinSize { get { return tempMagazinSize; } }

    private bool isReloaded;
    public Animation anim;

    public TypeOfPoolObjects bulletType;


    private void Awake()
    {
        tempMagazinSize = magazinSize;
    }

    void Update()
    {
        if (isReloaded)
            return;

        if (tempMagazinSize <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            MouseFire();
        }
    }


    public IEnumerator Reload()
    {
        isReloaded = true;
        UIController.instance.ShowMagazinSize("Reloading....");
        yield return new WaitForSeconds(reloadTime);
        tempMagazinSize = magazinSize;
        UIController.instance.ShowMagazinSize("magazin " + tempMagazinSize.ToString() + "/" + magazinSize.ToString());
        isReloaded = false;
    }

    private void MouseFire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Fire();
        }
    }

    public void Fire()
    {
        nextFire = Time.time + fireRate;
        GameObject missile = ObjectPooler.instance.GetPooledObject(bulletType);
        if (missile != null)
        {
            missile.transform.position = spawnPoint.position;
            missile.transform.rotation = spawnPoint.rotation;
            //Time.timeScale = 0f;
            missile.SetActive(true);
            missile.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * 500f);
        }
        anim.Play();
        tempMagazinSize--;
        UIController.instance.ShowMagazinSize("magazin " + tempMagazinSize.ToString() + "/" + magazinSize.ToString());
    }
}
