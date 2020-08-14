using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Util;
using UnityEngine;

namespace Itdimk
{
    public enum Mode
    {
        Nearest,
        Farthest,
        Any,
    }

    public class TargetSearcher : TargetSetterBase
    {
        public string Tag;
        public int ScanInterval = 4;
        public Mode Priority;
        public float MaxDistance = 10.0f;

        private List<GameObject> GameObjects = new List<GameObject>();
        private bool _refreshed;


        // Start is called before the first frame update
        public override Vector3 GetTargetPos()
        {
            if(GameObjects.Count > 0)
                return GameObjects.First(o => o.gameObject != null).transform.position;
            return Vector3.zero;
        }

        void Start()
        {
            RefreshGameObjects();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_refreshed && Time.time % ScanInterval < 1)
            {
                RefreshGameObjects();
                _refreshed = true;
            }

            if (Time.time % ScanInterval > 1)
                _refreshed = false;
        }


        void RefreshGameObjects()
        {
            GameObjects.Clear();
            var a = GameObject.FindGameObjectsWithTag(Tag);

            var objects = SortGameObjectsIfRequired(a);
            GameObjects.AddRange(objects.ToArray());
        }

        private IEnumerable<GameObject> SortGameObjectsIfRequired(IEnumerable<GameObject> objects)
        {
            objects = objects.Where(o => Vector2.Distance(o.transform.position, transform.position) <= MaxDistance);
            
            if (Priority == Mode.Nearest)
                return objects.OrderBy(o => Vector2.Distance(o.transform.position, transform.position));

            if (Priority == Mode.Farthest)
                return objects.OrderByDescending(o => Vector2.Distance(o.transform.position, transform.position));

            return objects;
        }
    }
}