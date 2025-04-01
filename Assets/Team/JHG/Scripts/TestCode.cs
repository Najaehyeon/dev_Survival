using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    private Collider2D collider;
    bool isOn;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOn = false;
    }
}
