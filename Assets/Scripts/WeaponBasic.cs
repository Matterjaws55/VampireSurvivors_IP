using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBasic : MonoBehaviour
{
    //Spawns weapon at a random point within X units of the player
    //When player walks over weapon they pick it up, destroying the weapon on the ground
    //Weapon enables and disables a sphere around the player each second, destroying enemies

    //Upgrades: Spawn after weapon is picked up within x units of the player
    //Picking up an upgrade increases the sphere size and shortens its duration


    [Header("Spawn Settings")]
    public GameObject weaponHandler;
    public GameObject weaponPrefab; 
    public GameObject upgradePrefab; 
    public float spawnRadius = 5f; 
    public float upgradeSpawnRadius = 10f; 

    private GameObject spawnedWeapon; 

    void Start()
    {
        SpawnWeapon();
    }

    void SpawnWeapon()
    {
        Vector3 spawnPosition = GetRandomPosition(spawnRadius);
        spawnedWeapon = Instantiate(weaponPrefab, spawnPosition, Quaternion.identity);

        PickUp pickup = spawnedWeapon.GetComponent<PickUp>();
        if (pickup == null)
        {
            pickup = spawnedWeapon.AddComponent<PickUp>();
        }
        pickup.OnPickedUp += HandleWeaponPickedUp;        
    }

    void HandleWeaponPickedUp()
    {
        if (spawnedWeapon != null)
        {
            spawnedWeapon.GetComponent<PickUp>().OnPickedUp -= HandleWeaponPickedUp;
            SpawnUpgrade();

            weaponHandler.SetActive(true);
        }
    }

    void SpawnUpgrade()
    {
        Vector3 upgradePosition = GetRandomPosition(upgradeSpawnRadius);
        GameObject newUpgrade = Instantiate(upgradePrefab, upgradePosition, Quaternion.identity);

        upgradeSpawnRadius += 2f; 

        PickUp pickup = newUpgrade.GetComponent<PickUp>();
        if (pickup == null)
        {
            pickup = newUpgrade.AddComponent<PickUp>();
        }
        pickup.OnPickedUp += HandleUpgradePickedUp;
    }

    void HandleUpgradePickedUp()
    {
        SpawnUpgrade();
    }

    Vector3 GetRandomPosition(float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;
    }

    void OnDisable()
    {
        spawnedWeapon = null;
    }
}