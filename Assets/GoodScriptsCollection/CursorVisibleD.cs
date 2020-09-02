using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorVisibleD : MonoBehaviour
{
    public bool ShowCursor;
    public bool UseFixedUpdate;
    public float UpdateInterval = 0.2F;

    private float _startTick;

    public UnityEvent OnCursorVisibleSet;

    // Start is called before the first frame update
    void Start()
    {
        _startTick = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UseFixedUpdate && Time.time - _startTick >= UpdateInterval)
        {
            Cursor.visible = ShowCursor;
            _startTick = Time.time;
            OnCursorVisibleSet?.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (UseFixedUpdate && Time.time - _startTick >= UpdateInterval)
        {
            Cursor.visible = ShowCursor;
            _startTick = Time.time;
            OnCursorVisibleSet?.Invoke();
        }
    }
}