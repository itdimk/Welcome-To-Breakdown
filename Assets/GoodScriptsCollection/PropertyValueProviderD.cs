using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public class PropertyValueProviderD : ValueProviderBaseD
{
    public Object Target;
    public string PropertyName;
    public bool IgnoreCase = true;

    private FieldInfo _field;
    private PropertyInfo _property;

    // Start is called before the first frame update
    void Start()
    {
        _field = GetTargetField();
        _property = GetTargetProperty();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override object GetValue()
    {
        if (_field != null)
            return _field.GetValue(Target);

        if (_property != null)
            return _property.GetValue(Target);

        Debug.LogWarning($"Can't find field or property \"{PropertyName}\" of \"{Target.name}\"");
        
        return null;
    }

    public override void SetValue(object value)
    {
        _field?.SetValue(Target, value);
        _property?.SetValue(Target, value);
    }

    public override Type GetValueType()
    {
        if (_field != null)
            return _field.FieldType;

        if (_property != null)
            return _property.PropertyType;

        return default;
    }

    FieldInfo GetTargetField()
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;

        if (IgnoreCase)
            flags |= BindingFlags.IgnoreCase;

        return Target.GetType().GetField(PropertyName, flags);
    }

    PropertyInfo GetTargetProperty()
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;

        if (IgnoreCase)
            flags |= BindingFlags.IgnoreCase;

        return Target.GetType().GetProperty(PropertyName, flags);
    }

    void SetTargetField(FieldInfo targetField, object value)
    {
        targetField.SetValue(Target, Convert.ChangeType(value, targetField.FieldType));
    }

    void SetTargetProperty(PropertyInfo targetProperty, object value)
    {
        targetProperty.SetValue(Target, Convert.ChangeType(value, targetProperty.PropertyType));
    }
}