using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        int levelsCompleted = PlayerPrefs.GetInt("levels-completed", 0);

        for(int i = 0; i < Buttons.Count; ++i)
        {
            var button = Buttons[i].GetComponent<UnityEngine.UI.Button>();
            button.interactable = i <= levelsCompleted;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartLevel1() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void StartLevel2() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    public void StartLevel3() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);


}
