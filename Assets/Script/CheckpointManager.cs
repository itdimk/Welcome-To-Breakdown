using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Itdimk
{
    public class CheckpointManager : MonoBehaviour
    {
        public GameObject Player;
        public bool SaveHealth = true;
        public bool SaveArmor = true;
        public bool MakeFullReset = false;
        public bool SaveInventoryItems = false;

        private const string
            PosX = "checkpoint-x",
            PosY = "checkpoint-y",
            Hp = "checkpoint-hp",
            Armor = "checkpoint-armor",
            InventoryItem = "checkpoint-inventory-item-id-";

        void Start()
        {
            if (MakeFullReset)
                FullReset();

            LoadData();
        }

        public void SaveData(Checkpoint checkpoint)
        {
            Player.TryGetComponent<HealthController>(out var healthCtr);

            SavePosition(checkpoint);

            if (SaveHealth) SaveHpInfo();
            if (SaveArmor) SaveArmorInfo();
            if (SaveInventoryItems) SaveInventory();
        }

        public void LoadData()
        {
            float playerX = PlayerPrefs.GetFloat(PosX);
            float playerY = PlayerPrefs.GetFloat(PosY);

            if (SaveArmor) LoadArmorInfo();
            if (SaveHealth) LoadHpInfo();
            if (SaveInventoryItems) LoadInventory();

            Player.transform.position = new Vector2(playerX, playerY);
        }

        public void FullReset()
        {
            PlayerPrefs.DeleteKey(PosX);
            PlayerPrefs.DeleteKey(PosY);
            PlayerPrefs.DeleteKey(Hp);
            PlayerPrefs.DeleteKey(Armor);

        }

        private void SavePosition(Checkpoint checkpoint)
        {
            Vector2 position = checkpoint.transform.position;
            PlayerPrefs.SetFloat(PosX, position.x);
            PlayerPrefs.SetFloat(PosY, position.y);
        }

        private void SaveHpInfo()
        {
            Player.TryGetComponent<HealthController>(out var healthCtr);
            PlayerPrefs.SetFloat(Hp, healthCtr.hp);
        }

        private void SaveArmorInfo()
        {
            Player.TryGetComponent<HealthController>(out var healthCtr);
            PlayerPrefs.SetFloat(Armor, healthCtr.armor);
        }

        private void LoadArmorInfo()
        {
            if (PlayerPrefs.HasKey(Armor))
                Player.GetComponent<HealthController>().SetArmor(PlayerPrefs.GetFloat(Armor));
        }

        private void LoadHpInfo()
        {
            if (PlayerPrefs.HasKey(Hp))
                Player.GetComponent<HealthController>().SetHp(PlayerPrefs.GetFloat(Hp));
        }

        private void SaveInventory()
        {
            Player.TryGetComponent<Inventory>(out var inventory);

            foreach (var item in inventory.InventoryItems)
                PlayerPrefs.SetInt(InventoryItem + item.Id, item.Id);
        }

        private void LoadInventory()
        {
            Player.TryGetComponent<Inventory>(out var inventory);
            var items = GameObject.FindObjectsOfType(typeof(InventoryItem)).Cast<InventoryItem>();

            foreach (var item in items)
            {
                if (PlayerPrefs.HasKey(InventoryItem + item.Id)
                    && !inventory.InventoryItems.Exists(o => o.Id == item.Id))
                {
                    inventory.AddToInventory(item);
                }
            }
        }
    }
}