using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject Player;
    public bool SaveHealth = true;
    public bool SaveArmor = true;
    public bool MakeFullReset = false;

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
        Player.TryGetComponent<PlayerMovement>(out var healthCtr);

        SavePosition(checkpoint);

        if (SaveHealth) SaveHpInfo();
        if (SaveArmor) SaveArmorInfo();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(PosX) && PlayerPrefs.HasKey(PosY))
        {
            float playerX = PlayerPrefs.GetFloat(PosX);
            float playerY = PlayerPrefs.GetFloat(PosY);

            if (SaveArmor) LoadArmorInfo();
            if (SaveHealth) LoadHpInfo();

            Player.transform.position = new Vector2(playerX, playerY);
        }
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
        Player.TryGetComponent<PlayerMovement>(out var healthCtr);
        PlayerPrefs.SetFloat(Hp, healthCtr.Health);
    }

    private void SaveArmorInfo()
    {
        Player.TryGetComponent<PlayerMovement>(out var healthCtr);
        PlayerPrefs.SetFloat(Armor, healthCtr.Armor);
    }

    private void LoadArmorInfo()
    {
        if (PlayerPrefs.HasKey(Armor))
        {
            Player.GetComponent<PlayerMovement>().Armor = PlayerPrefs.GetFloat(Armor);

            if (Player.GetComponent<PlayerMovement>().ArmorText != null)
                
                Player.GetComponent<PlayerMovement>().ArmorText.text =
                    Mathf.Round(PlayerPrefs.GetFloat(Armor)).ToString();
        }
    }

    private void LoadHpInfo()
    {
        if (PlayerPrefs.HasKey(Hp))
        {
            Player.GetComponent<PlayerMovement>().Health = PlayerPrefs.GetFloat(Hp);

            if (Player.GetComponent<PlayerMovement>().HelthText != null)
                Player.GetComponent<PlayerMovement>().HelthText.text = Mathf.Round(PlayerPrefs.GetFloat(Hp)).ToString();
        }
    }
}