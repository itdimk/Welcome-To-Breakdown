using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Itdimk.HealthController))]
public class HelathBarOutput : MonoBehaviour
{
    public Image HpBar;

    private Itdimk.HealthController _controller;

    private
        // Start is called before the first frame update
        void Start()
    {
        _controller = GetComponent<Itdimk.HealthController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float amount = _controller.hp / _controller.maxHp;

        if (Mathf.Abs(HpBar.fillAmount - amount) > 0.01f)
            HpBar.fillAmount = amount;
    }
}