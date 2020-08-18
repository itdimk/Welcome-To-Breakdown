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
        int levelsCompleted = PlayerPrefs.GetInt("levels-completed", 0) ;

     
        
        for(int i = 0; i < Buttons.Count; ++i)
        {
            var button = Buttons[i].GetComponent<UnityEngine.UI.Button>();
            button.interactable = i <= levelsCompleted;
        }
        
        Debug.Log($"Levels completed: {levelsCompleted}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int lvl) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + lvl);


}
