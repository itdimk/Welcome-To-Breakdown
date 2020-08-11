using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool UnloadCurrentScene = true;
    
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
    
    public void LoadSceneByOffset(int offset)
    {
        int currIndex = SceneManager.GetActiveScene().buildIndex;

        if (UnloadCurrentScene)
            SceneManager.UnloadSceneAsync(currIndex);

        SceneManager.LoadSceneAsync(currIndex + offset);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
