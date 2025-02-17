using System.Collections;
using System.Collections.Generic;
using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.VFX;



public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float CurrentHealth;
    [SerializeField] private ParticleSystem deathVfx;
    [SerializeField] private ParticleSystem hitVfx;
    

    // Health update events
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDied;
    //Private
    private StatusEffectManager statusEffectManager;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        statusEffectManager = GetComponent<StatusEffectManager>();
    }

    public void ApplyDamage(DamageData damageData, Vector3 attackerPos)
    {
        CurrentHealth -= damageData.damage;

        statusEffectManager.OnStatusTriggerBuildup(damageData.effectType,damageData.buildAmount);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        CheckHealth();
    }


    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);

            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }


    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetInitialHealth(int maxHealth)
    {
        this.MaxHealth = maxHealth;
        this.CurrentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetHealthAmount()
    {
        return CurrentHealth;
    }

    public float GetHealthAmountMax()
    {
        return MaxHealth;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)CurrentHealth / MaxHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth == 0;
    }

    public bool IsFullHealth()
    {
        return CurrentHealth == MaxHealth;
    }
}

[Serializable]
public class DamageData
{
    public int              damage;
    public StatusEffectType effectType;
    public float            buildAmount;
    
    public DamageData(int damage, StatusEffectType effectType, float buildAmount)
    {
        this.damage = damage;
        this.effectType = effectType;
        this.buildAmount = buildAmount;
    }
    
}