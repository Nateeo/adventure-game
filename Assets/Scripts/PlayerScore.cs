﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour {
    //Fields for time and score

    public float startTime;
    public int maxPlayTimeInMinutes;
    public float maxTime;

    public int maxNumberOfBonuses;
    public int numberOfBonuses;

    public Text bountyText;
    //Fields for time and score ends here ===============


    // Use this for initialization
    void Start () {
        //Code for initializing time and score.
        startTime = Time.time;
		maxNumberOfBonuses = 3;
        maxTime = maxPlayTimeInMinutes * 60;
    }

	void Update() {
		if (Input.GetKeyDown(KeyCode.L))
		{
			endSceneAndDisplayScore();
		}
	}

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
        }
        else if (timeEllapsed < division * 3)
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
    //This will switch to scene number 2 (the score screen)
    public void endSceneAndDisplayScore()
    {
		calculateScore ();
		resetScore ();
        SceneManager.LoadScene(2);
    }

	public void calculateScore() {
		int timeScore = computeTimeBasedScore();
		int bonusScore = computeBonusBasedScore();

		float timeInSec = Time.time - startTime;
		int bonusPoints = numberOfBonuses;
		int maxBonusPoints = maxNumberOfBonuses;

		//PlayerPrefs is like a persistence class (database)
		PlayerPrefs.SetFloat("Time", timeInSec);
		PlayerPrefs.SetInt("TimeScore", timeScore);
		PlayerPrefs.SetInt("BonusScore", bonusScore);
		PlayerPrefs.SetInt("Bonus", numberOfBonuses);
		PlayerPrefs.SetInt("MaxBonus", maxNumberOfBonuses);
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

    //Use this method when a bonus object has been picked up
    public void incrementBonus()
    {
        numberOfBonuses++;
		setBonusText ();
    }
    //Use this method when you want to deduct points
    public void decrementBonus()
    {
        numberOfBonuses--;
		setBonusText ();
    }

	public void resetScore()
	{
		startTime = Time.time;
		maxTime = maxPlayTimeInMinutes * 60;
		numberOfBonuses = 0;
		setBonusText ();
	}

	private void setBonusText() {
		bountyText.text = "Bonus fugitives: " + numberOfBonuses + "/" + maxNumberOfBonuses;
	}

}
