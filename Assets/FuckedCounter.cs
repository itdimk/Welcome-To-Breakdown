using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuckedCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private decimal number;
    
    // Start is called before the first frame update
    void Start()
    {
        number = Random.Range(int.MaxValue - 2000, int.MaxValue);
        FindObjectOfType<AudioManager>().Play("LevelComplete");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(number < decimal.MaxValue / 2)
            number *= (decimal)(Random.Range(1f, 2f));
        
        Text.text = $"{number}E+{Random.Range(1f, 10f)}";

    }
}
