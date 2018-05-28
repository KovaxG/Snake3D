using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public Text newGame;    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPointerEnter(PointerEventData e) {
        newGame.text = "<color=brown>New Game</color>";
    }

    public void OnPointerExit(PointerEventData e) {
        newGame.text = "New Game";
    }

    public void OnPointerClick(PointerEventData e) {
        SceneManager.LoadScene("Game");
    }
}
