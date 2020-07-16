using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class scaryDisappear : MonoBehaviour
{
    public int AliveTime;
    private Stopwatch _timer;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        _timer = new Stopwatch();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(111111);
        
        if (sr.sortingOrder > 1)
        {
            _timer.Restart();
        }
        
        if (_timer.IsRunning && _timer.ElapsedMilliseconds >= AliveTime)
        {
          
            _timer.Stop();
            sr.sortingOrder = 0;
        }
   
    }
}
