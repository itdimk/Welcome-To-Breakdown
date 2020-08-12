using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogSys : MonoBehaviour
{
    public Text TextMesh;
    public List<string> Sentences;
    public List<ModalWindow> Modals;
    private int currIndex = 0;
    public GameObject ContinueButton;

    private string smoothTyping;
    private int smoothTypingIndex;
    public int CrutchedSpeed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowNext();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % CrutchedSpeed  == 0)
            SmoothTyping();
    }

    public void ShowNext()
    {
        if (currIndex >= Sentences.Count)
        {
            Destroy(this);
            TextMesh.text = "";
            ContinueButton.gameObject.SetActive(false);
        }
        else
        {
            ShowCurrent();
            MoveNext();
        }
    }

    private void MoveNext()
    {
      

        currIndex = Mathf.Clamp(currIndex + 1, 0, Sentences.Count );
    }

    private void ShowCurrent()
    {
        var modalToShow = Modals.FirstOrDefault(m => m.SentencesToShowAfter.Contains(currIndex));

        if (modalToShow != null)
        {
            modalToShow.gameObject.SetActive(true);
            ContinueButton.SetActive(false);
        }
        else
        {
            Modals.First().gameObject.SetActive(false);
            ContinueButton.SetActive(true);
        }

        SetSmoothTyping(Sentences[currIndex]);
    }

    private void SmoothTyping()
    {
        if (smoothTypingIndex < smoothTyping.Length)
        {
            TextMesh.text += smoothTyping[smoothTypingIndex++];
        }
    }

    private void SetSmoothTyping(string toType)
    {
        TextMesh.text = "";
        smoothTypingIndex = 0;
        smoothTyping = toType;
    }
}