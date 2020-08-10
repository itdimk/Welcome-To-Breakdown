using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Itdimk
{
    public class Inventory : MonoBehaviour
    {
        public List<InventoryItem> InventoryItems;

        public string ScrollItemsButton;

        private int _currItemIndex;
        private Animator _animator;
        private List<InventoryItem> _tools = new List<InventoryItem>();

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();

            foreach (var item in InventoryItems)
            {
                SetTransformIfRequired(item);

                if (item.EquipPoint != null)
                    _tools.Add(item);
            }

            EquipNextItem();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(ScrollItemsButton))
                EquipNextItem();
        }

        void EquipNextItem()
        {
            if (_tools.Count == 0) return;

            UnequipCurrentItem();

            if (++_currItemIndex >= _tools.Count)
                _currItemIndex = 0;

            EquipCurrentItem();
        }

        private void UnequipCurrentItem()
        {
            InventoryItem equipable = _tools[_currItemIndex];

            equipable.gameObject.SetActive(false);

            equipable.Dependencies?.ForEach(i => i.gameObject.SetActive(false));
        }

        private void EquipCurrentItem()
        {
            InventoryItem equipable = _tools[_currItemIndex];

            equipable.gameObject.SetActive(true);
            equipable.ShowAvatarIfRequired();
            equipable.ShowCountfRequired();

            _animator.SetInteger("InventoryItemId", equipable.Id);

            equipable.Dependencies?.ForEach(i => i.gameObject.SetActive(true));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out CollectableItem collectable))
            {
                Collect(collectable);

                if (_tools.Count == 1)
                    EquipNextItem();
            }
        }

        private void Collect(CollectableItem item)
        {
            AddToInventory(item.InventoryItem);
            Destroy(item.gameObject);
            
            if(item.InventoryItem.IsTool)
                EquipNextItem();
        }

        public void AddToInventory(InventoryItem item)
        {
            SetTransformIfRequired(item);

            InventoryItem existed = InventoryItems
                .FirstOrDefault(o => o.Id == item.Id);

            if (existed == null)
            {
                InventoryItems.Add(item);
                if (item.IsTool && item.EquipPoint != null)
                {
                    item.gameObject.SetActive(false);
                    _tools.Add(item);
                }
                else if (item.IsEquipment && item.EquipPoint != null)
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.ShowAvatarIfRequired();
                    item.ShowCountfRequired();
                }
            }
            else
            {
                existed.Count = Math.Min(existed.MaxCount, existed.Count + item.Count);
            }
        }

        private void SetTransformIfRequired(InventoryItem item)
        {
            if (item.EquipPoint != null)
            {
                Transform itemTransform = item.transform;
                itemTransform.parent = item.EquipPoint;
                itemTransform.position = item.EquipPoint.position;

            }
            else
            {
                item.transform.parent = null;
            }
        }
    }
}