using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Color originalColor;

    private void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    public void AddHighlight()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void RemoveHighlight()
    {
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }
}


