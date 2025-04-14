using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PlayerHealth")]
    public Image healthBar;
    public float currentHealth = 100f;

    public GameObject player;

    public delegate void HealthPoints(GameManager source, float oldHealth, float newHealth);
    public event HealthPoints OnHealthChanged;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        float oldHealth = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
        healthBar.fillAmount = currentHealth / 100;
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.fillAmount = currentHealth / 100;
    }
}
