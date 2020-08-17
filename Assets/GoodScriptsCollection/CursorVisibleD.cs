using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorVisibleD : MonoBehaviour
{
    public bool ShowCursor;
    public int UpdateInterval = 10;

    public UnityEvent OnCursorVisibleSet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % UpdateInterval == 0 && Cursor.visible != ShowCursor)
        {
            Cursor.visible = ShowCursor;
            OnCursorVisibleSet?.Invoke();
        }
    }
}
