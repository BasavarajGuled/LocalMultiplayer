using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    
    public List<GameObject> pooledOjects;
    public List<GameObject> types;
    //public GameObject objectToPool;
    public int amountToPool;

    public bool shouldExpand;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledOjects = new List<GameObject>();
        for (int k = 0; k < types.Count; k++)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(types[k]);
                obj.SetActive(false);
                pooledOjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(TypeOfPoolObjects type)
    {
        for (int i = 0; i < pooledOjects.Count; i++)
        {
            if (!pooledOjects[i].activeInHierarchy && pooledOjects[i].GetComponent<Type>().type == type)
            {
                return pooledOjects[i];
            }
        }
        foreach (GameObject bulletType in types)
        {
            if (bulletType.GetComponent<Type>().type == type)
            {
                if (shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(bulletType);
                    obj.SetActive(false);
                    pooledOjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}

public enum TypeOfPoolObjects { ShotGun, HandGun, BarretGun }

