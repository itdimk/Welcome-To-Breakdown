using System;
using UnityEngine;
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


        public Text WriteHpTo;
        public Text WriteArmorTo;

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

            if (hp <= 0)
                Die();
        }

        public void Cure(float amount)
        {
            SetHp(Math.Min(maxHp, hp + amount));
        }

        public void Armor(float amount)
        {
            SetArmor(Math.Min(maxArmor, armor + amount));
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
            Destroy(gameObject);
        }
    }
}