using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");
        }
    }  

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = Color.white;
        mouse_over = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = Color.black;
        mouse_over = false;
    }
}
