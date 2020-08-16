using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Itdimk
{
    public class HealthController : MonoBehaviour
    {
        [FormerlySerializedAs("HP")] public float hp = 100.0F;
        [FormerlySerializedAs("Max HP")] public float maxHp = 100.0F;
        public float armor = 0f;
        public float maxArmor = 100.0f;

        public float armorAbsorption = 0.9f;

        public TextMeshProUGUI WriteHpTo;
        public TextMeshProUGUI WriteArmorTo;
        public GameObject DeathEffect;
        public float DeathDelay = 0f;

        public UnityEvent OnDeath;
        public UnityEvent OnHit;
        public UnityEvent OnCure;
        public UnityEvent OnArmor;
        public UnityEvent BeforeDeath;


        private float _deathStartTick;

        private void Start()
        {
            SetHp(hp);
            SetArmor(armor);
        }

        public void DealDamage(float amount)
        {
            float absorbed = Math.Min(amount * armorAbsorption, armor * armorAbsorption);

            SetArmor(Math.Max(0, armor - absorbed));
            SetHp(Math.Max(0, hp - (amount - absorbed)));

            OnHit?.Invoke();

            if (hp <= 0)
            {
                SetHp(0.0f);

                if (_deathStartTick == 0)
                {
                    BeforeDeath?.Invoke();
                    _deathStartTick = Time.time;
                }
                else if (Time.time >= _deathStartTick + DeathDelay)
                    Die();
            }
        }

        public void Cure(float amount)
        {
            SetHp(Math.Min(maxHp, hp + amount));

            OnCure?.Invoke();
        }

        public void Armor(float amount)
        {
            SetArmor(Math.Min(maxArmor, armor + amount));

            OnArmor?.Invoke();
        }

        public void SetArmor(float value)
        {
            armor = value;

            if (WriteArmorTo != null)
                WriteArmorTo.text = Mathf.Round(value).ToString();
        }

        public void SetHp(float value)
        {
            hp = value;

            if (WriteHpTo != null)
                WriteHpTo.text = Mathf.Round(value).ToString();
        }

        public void Die()
        {
            OnDeath?.Invoke();

            if (DeathEffect != null)
                Instantiate(DeathEffect, transform.position, Quaternion.identity).SetActive(true);
            Destroy(gameObject);
        }
    }
}