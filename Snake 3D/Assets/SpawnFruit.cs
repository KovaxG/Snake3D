﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SpawnFruit : MonoBehaviour {

    public GameObject fruit;
    public GameObject snakeHead;
    int score;
    public Text scoreText;
    public Game game;

    bool fruitExists = false;
    System.Random random = new System.Random();
    Vector3 position = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		Vector3 scale = fruit.transform.localScale;
        scale.x = 0.05f;
        scale.y = 0.05f;
        scale.z = 0.05f;

        fruit.transform.localScale = scale;

        fruit = Instantiate(fruit, position, Quaternion.identity);

        score = 0;
        scoreText.text = "Score: " + score;
	}
	
	// Update is called once per frame
	void Update () {
        if (!fruitExists) {
            float x = random.Next(-2, 2);
            float y = random.Next(-2, 2);
            float z = random.Next(-2, 2);

            position = new Vector3(x, y, z);

		    fruit.transform.position = position;
            fruitExists = true;
        }
        else {    
            float distance = Vector3.Distance(snakeHead.transform.position, position);
            if (distance < 0.1) {
                score++;    
                scoreText.text = "Score: " + score; 
                fruitExists = false;
                game.addCounter(position);
            }
        }
	}
}
