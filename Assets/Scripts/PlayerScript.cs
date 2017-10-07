﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	public float speed;

    //Fields for time and score
    public Canvas scoreScreen;

    private float startTime;
    public int maxPlayTimeInMinutes;
    private float maxTime;

    public GameObject fullStar0;
    public GameObject fullStar1;
    public GameObject fullStar2;
    public GameObject fullStar3;
    public GameObject fullStar4;
    public GameObject fullStar5;
    private List<GameObject> listOfStars = new List<GameObject>();

    public int maxNumberOfBonuses;
    private int numberOfBonuses;
    //Fields for time and score ends here ===============

    private Rigidbody rigidBody;
	Vector3 movement;

	// Use this for initialization
	void Start () {
        //Code for initializing time and score.
        startTime = Time.time;
        maxTime = maxPlayTimeInMinutes * 60; 
        scoreScreen.enabled = false;

        listOfStars.Add(fullStar0);
        listOfStars.Add(fullStar1);
        listOfStars.Add(fullStar2);
        listOfStars.Add(fullStar3);
        listOfStars.Add(fullStar4);
        listOfStars.Add(fullStar5);

        rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);

        if (Input.GetKeyDown(KeyCode.P))
        {
            incrementBonus();
            setScoreScreenVisible(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            decrementBonus();
            setScoreScreenVisible(false);
        }
    }

    //Method for computing the score based on a maximum time.
    //The policy is a 3 section idea: 0/1/2/3 stars.
    int computeTimeBasedScore()
    {
        float timeEllapsed = Time.time - startTime;
        float division = maxTime / 3;
        if (timeEllapsed < division)
        {
            return 3;
        }
        else if (timeEllapsed < division * 2)
        {
            return 2;
        } else if (timeEllapsed < division * 3)
        {
            return 1;
        }
        return 0;
    }

    //Method for computing the score based on a number of bonuses picked up.
    //The policy is a 3 section idea: 0/1/2/3 stars.
    int computeBonusBasedScore()
    {
        if (numberOfBonuses >= maxNumberOfBonuses)
        {
            return 3;
        }
        else if (numberOfBonuses >= maxNumberOfBonuses / 2.0)
        {
            return 2;
        }
        else if (numberOfBonuses > 0)
        {
            return 1;
        }
        return 0;
    }

    //Use this method to display the score.
    private void setScoreScreenVisible(bool visible)
    {
        int timeScore = computeTimeBasedScore();
        int bonusScore = computeBonusBasedScore();
        changeStar(timeScore, bonusScore);
        scoreScreen.enabled = visible;
    }

    //private function for updating the time and the slider. 
    /* TIMER REMOVED (code might be useful some time, so has been left in here!)
     * 
     * 
    private void updateTimeSlider()
    {
        // This section is to do with displaying the time. 
        float timeinSec = Time.time - startTime;
        int minutes = ((int)timeinSec / 60);
        int seconds = (int)(timeinSec % 60);

        string minToDisplay = minutes.ToString("00");
        string secToDisplay = seconds.ToString("00");

        timeText.text = minToDisplay + ":" + secToDisplay;

        float proportion = timeinSec / maxTime;
        if (proportion > 1) { proportion = 1; }

        float proportionRemaining = 1 - proportion;
        timeSlider.value = proportionRemaining;

        var fill = (timeSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
        if (fill != null)
        {
            fill.color = Color.Lerp(Color.red, Color.green, proportionRemaining);
        }
    }
    */

    //private helper method for changing number of stars (score).
    //use only 0-3 stars for both integers.
    private void changeStar (int firstRow, int secondRow)
    {
        if (firstRow < 0 || firstRow > 3 || secondRow < 0 || secondRow > 3)
        {
            throw new System.ArgumentException("the number of stars for both rows must be between 0-3.");
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < firstRow)
                {
                    listOfStars[i].SetActive(true);
                }
                else
                {
                    listOfStars[i].SetActive(false);
                }
            }

            for (int i = 3; i < 6; i++)
            {
                if (i < secondRow + 3)
                {
                    listOfStars[i].SetActive(true);
                }
                else
                {
                    listOfStars[i].SetActive(false);
                }
            }
        }
    }

    //Use this method when a bonus object has been picked up
    private void incrementBonus()
    {
        numberOfBonuses++;
    }
    //Use this method when you want to deduct points
    private void decrementBonus()
    {
        numberOfBonuses--;
    }

    void Move(float h, float v) {
		Vector3 movement = new Vector3(h, 0.0f, v);
		movement = Camera.main.transform.TransformDirection(movement);
		//make sure player always moves in same speed no matter what combination of keys
		//this is called every with every FixedUpdate- dont want it to move 6 units every fixed update
		//want to change it so that it is per second- multiple it by delta time. delta time is the time between each update call
		//so if youre updating every 50th of a second, over the course of 50 50th of a second its going to move 6 units
		movement = movement.normalized * speed * Time.deltaTime;
		rigidBody.MovePosition (transform.position + movement);
	}
}
