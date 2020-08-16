using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthControllerD : MonoBehaviour
{
    public float Hp = 100.0F;
    public float MaxHp = 100.0F;
    public float Armor = 0f;
    public float MaxArmor = 100.0f;
    public float ArmorAbsorption = 0.9f;
    
    [Space]
    public NumberOutputD HpOutput;
    public NumberOutputD ArmorOutput;
    public GameObject DeathEffect;

    public UnityEvent OnDeath;
    public UnityEvent OnHit;
    public UnityEvent OnCure;
    public UnityEvent OnArmor;


    private void Start()
    {
        SetHp(Hp);
        SetArmor(Armor);
    }

    public void DealDamage(float amount)
    {
        float absorbed = Math.Min(amount * ArmorAbsorption, Armor * ArmorAbsorption);

        SetArmor(Math.Max(0, Armor - absorbed));
        SetHp(Math.Max(0, Hp - (amount - absorbed)));

        OnHit?.Invoke();

        if (Hp <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        SetHp(Math.Min(MaxHp, Hp + amount));

        OnCure?.Invoke();
    }

    public void AddArmor(float amount)
    {
        SetArmor(Math.Min(MaxArmor, Armor + amount));

        OnArmor?.Invoke();
    }

    private void SetArmor(float value)
    {
        Armor = value;

        if (ArmorOutput != null)
            ArmorOutput.SetNumber(value);
    }

    private void SetHp(float value)
    {
        Hp = value;

        if (HpOutput != null)
            HpOutput.SetNumber(value);
    }

    public void Die()
    {
        SetHp(0.0f);
        OnDeath?.Invoke();

        if (DeathEffect != null)
            Instantiate(DeathEffect, transform.position, Quaternion.identity).SetActive(true);
        Destroy(gameObject);
    }
}