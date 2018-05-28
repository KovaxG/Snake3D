using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighScore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public Text hscores;    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPointerEnter(PointerEventData e) {
        hscores.text = "<color=brown>High Score</color>";
    }

    public void OnPointerExit(PointerEventData e) {
        hscores.text = "High Score"; 
    }

    public void OnPointerClick(PointerEventData ex) {
        try {
            string scores = System.IO.File.ReadAllText("HighScores.txt");
            hscores.text = scores;
        } catch {
            string text = "No file found!";
            hscores.text = text;
        }
    }
}


