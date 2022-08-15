using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSelector : MonoBehaviour
{
    public enum Weapon { Gun, Sword, Lantern, Kart };
    public Weapon weapon;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] WeaponSelector[] otherWeaponsSelectors;
    public GameObject[] weaponMesh;
    public GameObject weaponGroup;
    float index;
    public UnityEvent openDoor;
    private void Update()
    {
        switch (weapon)
        {
            case Weapon.Gun:
                index = 0;
                break;
            case Weapon.Sword:
                index = 1;
                break;
            case Weapon.Lantern:
                index = 2;
                break;
            case Weapon.Kart:
                index = 3;
                break;
            default:
                break;
        }

        for (int i = 0; i < weaponMesh.Length; i++)
        {
            if (i == index) weaponMesh[i].SetActive(true);
            else weaponMesh[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            openDoor.Invoke();
            switch (weapon)
            {
                case Weapon.Gun:
                    playerStats.weapon = PlayerStats.Weapon.Gun;
                    break;
                case Weapon.Sword:
                    playerStats.weapon = PlayerStats.Weapon.Sword;
                    break;
                case Weapon.Lantern:
                    playerStats.weapon = PlayerStats.Weapon.Lantern;
                    break;
                case Weapon.Kart:
                    playerStats.weapon = PlayerStats.Weapon.Kart;
                    break;
                default:
                    break;
            }
            weaponGroup.SetActive(false);
            foreach (var item in otherWeaponsSelectors)
            {
                item.weaponGroup.SetActive(true);
            }
            playerStats.ChooseWeaponEventTrigger();
        }
    }
}
