using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StringListProviderD : MonoBehaviour
{
    [Serializable]
    public class StringListEntry
    {
        public string Language;
        
        [TextArea]
        public List<string> Strings;

        public override string ToString() => Language;
    }

    public GameManagerD Manager;
    public List<StringListEntry> StringLists;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    List<string> GetStringList()
    {
        string language = Manager.GetLanguage();
        var entry = StringLists.FirstOrDefault(s => s.Language == language);

        if (entry != null)
            return entry.Strings;

        Debug.LogWarning($"String list isn't specified for the language \"{language}\"");
        return new List<string>(0);
    }
}