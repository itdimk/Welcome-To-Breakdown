using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextCheck : MonoBehaviour
{
    public InputField Field;

    public string ValidText;


    public string EnterButtonName;
    public UnityEvent TextValid;

    public UnityEvent TextInvalid;

    // Start is called before the first frame update
    void Start()
    {
        Field.text += "_";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(EnterButtonName))
        {
            (ValidText.ToLower() == Field.text.Trim('_').ToLower() ? TextValid : TextInvalid)?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Field.text[Field.text.Length - 1] != '_')
            Field.text += "_";
    }
}