using System;
using System.Reflection;
using UnityEngine;

public class AnimatorParameterMapperD : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string ParameterName;
        public string PropertyName;
        
        public PropertyInfo Property { get; private set; }
        
        public void Init(Type type)
        {
            Property = type.GetProperty(PropertyName);
        }

        public override string ToString()
        {
            return ParameterName;
        }
    }

    public Component DataSource;
    public Item[] MappingInfo;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        foreach (var item in MappingInfo)
            item.Init(DataSource.GetType());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in MappingInfo)
        {
            var property = item.Property;

            if (property.PropertyType == typeof(float))
                _animator.SetFloat(item.ParameterName, (float) property.GetValue(DataSource));

            else if (property.PropertyType == typeof(bool))
                _animator.SetBool(item.ParameterName, (bool) property.GetValue(DataSource));

            else if (property.PropertyType == typeof(int))
                _animator.SetInteger(item.ParameterName, (int) property.GetValue(DataSource));

            else
                throw new Exception($"Unsupported property type: {property.PropertyType}");
        }
    }
}