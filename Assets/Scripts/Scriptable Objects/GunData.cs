using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]

public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Stats")]
    public float damage;
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float maxDistance;

    [Header("Reloading")]
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
