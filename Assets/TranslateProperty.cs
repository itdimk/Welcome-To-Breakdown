using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TranslateProperty : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string Language;
        [TextArea]
        public string Value;
    }
    
    public Component Target;
    public string TargetPropertyName = "text";

    [Space] 
    public string LanguagePrefsKey = "lang";
    
    [Space]
    public List<Item> Translations = new List<Item>
    {
        new Item { Language = "ru" }
    };
    
    // Start is called before the first frame update
    void Start()
    {
        var p = Target.GetType().GetProperty(TargetPropertyName);
        string lang = PlayerPrefs.GetString(LanguagePrefsKey);
        string newValue = Translations.FirstOrDefault(t => t.Language == lang)?.Value;
        if (p != null &&  !string.IsNullOrEmpty(newValue))
            p.SetValue(Target, newValue);
        else
            Debug.LogWarning("Either property is not found or value is not specified");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
