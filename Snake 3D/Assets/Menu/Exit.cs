using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public Text newGame;    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPointerEnter(PointerEventData e) {
        newGame.text = "<color=brown>Exit</color>";
    }

    public void OnPointerExit(PointerEventData e) {
        newGame.text = "Exit"; 
    }

    public void OnPointerClick(PointerEventData e) {
        Debug.Log("Quitting");    
        Application.Quit();
    }
}


