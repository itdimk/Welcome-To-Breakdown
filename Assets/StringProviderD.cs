using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StringProviderD : MonoBehaviour
{
    [Serializable]
    public class StringEntry
    {
        public string Language;
        
        [TextArea]
        public string String;

        public override string ToString() => Language;
    }

    public GameManagerD Manager;
    public List<StringEntry> StringLists;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    string GetString()
    {
        string language = Manager.GetLanguage();
        var entry = StringLists.FirstOrDefault(s => s.Language == language);

        if (entry != null)
            return entry.String;

        Debug.LogWarning($"String isn't specified for the language \"{language}\"");
        return string.Empty;
    }
}
