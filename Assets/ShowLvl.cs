using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowLvl : MonoBehaviour
{
    public Text Output;

    public string Prefix = "Level ";

    public string Postfix = "";
    // Start is called before the first frame update
    void Start()
    {
        Output.text = Prefix + (SceneManager.GetActiveScene().buildIndex) + Postfix;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
