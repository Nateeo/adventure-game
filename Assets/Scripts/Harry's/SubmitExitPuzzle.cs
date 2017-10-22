﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubmitExitPuzzle : MonoBehaviour {

    public GameObject canvas;
    public GameObject submitButton;
    public Text responseText;
    public Text answer1;
    public Text answer2;
    public Text answer3;
    public Text answer4;

    private List<string> answers = new List<string>();
    
	void Start () {
        generateAnswers();
	}
	
    private void generateAnswers()
    {
        answers.Add("apepi");
        answers.Add("shebitku");
        answers.Add("takelot");
        answers.Add("tutankhamen");
    }

    public void checkAnswer()
    {
        bool correct = true;
        if (answer1.text.ToLower().Equals(answers[0]))
        {
            correct = false;
        }
        if (answer2.text.ToLower().Equals(answers[1]))
        {
            correct = false;
        }
        if (answer3.text.ToLower().Equals(answers[2]))
        {
            correct = false;
        }
        if (answer4.text.ToLower().Equals(answers[3]))
        {
            correct = false;
        }

        if (correct)
        {
            canvas.SetActive(false);
        } else
        {
            responseText.text = "Wrong, try again!";
        }
    }
}
