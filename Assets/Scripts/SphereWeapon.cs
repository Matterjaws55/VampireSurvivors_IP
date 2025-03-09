using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWeapon : MonoBehaviour
{
    public float activeDuration = 1f; 
    public float inactiveDuration = 1f; 
    public float sizeIncreasePerUpgrade = 0.2f; 
    public float durationIncreasePerUpgrade = 0.1f; 
    private int upgradeCount = 0; 
    private Vector3 initialScale;


    private Collider weaponCollider;
    private Renderer weaponRenderer;

    void Start()
    {
        initialScale = transform.localScale; 
        weaponCollider = GetComponent<Collider>();
        weaponRenderer = GetComponent<Renderer>();

        UpgradePickUp.OnUpgradePickedUp += UpgradeWeapon;

        ToggleWeapon();
    }

    void ToggleWeapon()
    {
        bool isActive = weaponCollider.enabled;

        if (isActive)
        {
            DisableWeapon();
            Invoke(nameof(ToggleWeapon), inactiveDuration);
        }
        else
        {
            EnableWeapon();
            Invoke(nameof(ToggleWeapon), activeDuration);
        }
    }

    void EnableWeapon()
    {
        weaponCollider.enabled = true;
        weaponRenderer.enabled = true;
    }

    void DisableWeapon()
    {
        weaponCollider.enabled = false;
        weaponRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    public void UpgradeWeapon()
    {
        upgradeCount++;
        activeDuration += durationIncreasePerUpgrade; 
        transform.localScale = initialScale * (1 + sizeIncreasePerUpgrade * upgradeCount);

        Debug.Log($"Weapon upgraded! New active duration: {activeDuration}, New size: {transform.localScale}");
    }
}