using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using System;
using System.IO;

public class Game : MonoBehaviour {

    // Snake Head
    public GameObject snakeHead;    
    float moveTime = 1f;
    float elapsedTime = 0;
    bool canPressButton = true;
    bool gameOver = false;
    float sensitivity = 0.1f;

    Vector3 forward = new Vector3(0, 0, 0.5f);
    Vector3 down = new Vector3(90, 0, 0);
    Vector3 left = new Vector3(0, 90, 0);

    enum Direction {UP, DOWN, RIGHT, LEFT, NOPE};
    Direction direction = Direction.NOPE;

    // Snake Body
    public GameObject snakeBody;
    List<GameObject> bodies = new List<GameObject>();
    public List<Counter> counters = new List<Counter>();

    // Other
    public Text scoreText;
    public InputField nameField;
    public Image nameBg;
    FileAppender fa = new FileAppender("HighScores.txt");

	void Start () {
        Debug.Log("Starting Game");    
        Cursor.visible = false;

        snakeHead.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        snakeBody.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        snakeHead.transform.localPosition = new Vector3(0, 0, 0);
        snakeBody.transform.localPosition = new Vector3(0, 0, -0.5f);

        bodies.Add(snakeHead);

        scoreText.enabled = false;
        nameField.enabled = false;
        var myColor = nameBg.color;
        myColor.a = 0;
        nameBg.color = myColor;
	}
	
	void Update () {

        if (gameOver) {
            Cursor.visible = true;    
                
            if (Input.GetKey("return")) {
                string name = nameField.text;
                fa.append(name + " " + (bodies.Count-1));
                SceneManager.LoadScene("Menu");
            }    
            return;    
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime > moveTime) {
            updateBody();
            elapsedTime = 0;
            
            for (int i = 1; i < bodies.Count; i++) {
                if (bodies[i].transform.position == bodies[0].transform.position) {
                    gameOver = true;
                    highScoreForm();
                }    
            }
            if (gameOver) Debug.Log("Game Over ");
        }

        if (Input.GetKey("escape")) {
            gameOver = true;
            Cursor.visible = true;
            SceneManager.LoadScene("Menu");
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        if (canPressButton) {
            if      (horizontal >= sensitivity) direction = Direction.LEFT;
            else if (horizontal <= -sensitivity) direction = Direction.RIGHT;
            else if (vertical >= sensitivity)   direction = Direction.UP;
            else if (vertical <= -sensitivity)   direction = Direction.DOWN;

            
        }
        else direction = Direction.NOPE;

        canPressButton = (horizontal <= sensitivity && horizontal >= -sensitivity) && 
                             (vertical <= sensitivity && vertical >= -sensitivity);

        switch(direction) {
            case Direction.LEFT:  snakeHead.transform.Rotate(left);  break;
            case Direction.RIGHT: snakeHead.transform.Rotate(-left); break;
            case Direction.UP:    snakeHead.transform.Rotate(-down); break;
            case Direction.DOWN:  snakeHead.transform.Rotate(down);  break;
            default: break;
        }
	}

    private void updateBody() {
    
        // update spawn counters and spawn    
        foreach (var counter in counters) {
            counter.tick();
            if (counter.isOver()) {
                var newBody = Instantiate(snakeBody);
                newBody.transform.position = counter.getPosition();
                bodies.Add(newBody);
            }
        }
        counters.RemoveAll(c => c.isOver());

        // move each body part
        for (int i = bodies.Count - 1; i > 0; i--) {
            bodies[i].transform.position = bodies[i-1].transform.position;
        }
        snakeHead.transform.Translate(forward);
    }

    public void addCounter(Vector3 position) {
        counters.Add(new Counter(bodies.Count, position));
    }

    private void highScoreForm() {
        scoreText.enabled = true;
        nameField.enabled = true;  
        var myColor = nameBg.color;
        myColor.a = 1f;
        nameBg.color = myColor;
        scoreText.text = "Your score is: " + (bodies.Count-1) + "\n Enter name here!";
    }
}

public class Counter {

    int counter;        
    Vector3 position;
    
    public Counter(int start, Vector3 position) {
        this.counter = start;
        this.position = position;
    }

    public void tick() {
        counter--;
    }

    public bool isOver() {
        return counter <= 0;
    }

    public Vector3 getPosition() {
        return position;
    }
}

public class FileAppender {
    string path;

    public FileAppender(string path) {
        this.path = path;
    }

    public void append(string text) {
        if (!File.Exists(path)) {
            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(text);
            }
        }
        else {
            using(StreamWriter sw = File.AppendText(path)) {
                sw.WriteLine(text);
            }
        }
    }
}
