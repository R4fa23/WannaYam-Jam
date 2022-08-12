using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public enum Weapon { Gun, Sword, Lantern, Kart };
    public Weapon weapon;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject[] otherWeapons;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
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
            foreach (var item in otherWeapons)
            {
                item.SetActive(true);
            }

            playerStats.ChooseWeaponEventTrigger();

            gameObject.SetActive(false);
        }
    }
}
