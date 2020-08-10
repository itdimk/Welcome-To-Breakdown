using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Itdimk
{
    public class InventoryItem : MonoBehaviour
    {
        public int Id;
        public int MaxCount;
        public int Count;
        public Transform EquipPoint;

        public Sprite Avatar;
        public SpriteRenderer ShowAvatar;
        public Text ShowCount;

        public bool IsTool = true;
        public bool IsEquipment = false;

        public List<InventoryItem> Dependencies;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowAvatarIfRequired()
        {
            if (ShowAvatar != null)
                ShowAvatar.sprite = Avatar;
        }

        public void ShowCountfRequired()
        {
            if (ShowCount != null)
                ShowCount.text = Count.ToString();
        }
    }
}