using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour, IPointerDownHandler {

    Image image;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();

	}   

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {       
        Debug.Log("pointer down");
    }
}
