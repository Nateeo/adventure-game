﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubmitPasscode : MonoBehaviour {

    public GameObject UIManager;
    public GameObject passcodeScreen;
    public GameObject door;
    public Text responseText;
    public Text passcodeInput1;
    public Text passcodeInput2;
    public Text passcodeInput3;
    public Text passcodeInput4;

    private string passcode;
    private bool hasAnswered = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            onPassCodeEnter();
        }
    }

    public void onPassCodeEnter()
    {
        string passcodeInput = "";
        passcodeInput += passcodeInput1.text;
        passcodeInput += passcodeInput2.text;
        passcodeInput += passcodeInput3.text;
        passcodeInput += passcodeInput4.text;
        if (passcodeInput.Equals(passcode))
        {
            door.transform.Rotate(0, 80, 0);
            passcodeScreen.SetActive(false);
            hasAnswered = true;
            UIManager.GetComponent<UIManager>().interfaceClosed();
        } else
        {
            responseText.text = "Wrong, try again!";
        }
    }

    public void setPasscode(string realPasscode)
    {
        passcode = realPasscode;
    }

    public bool getAnswer()
    {
        return hasAnswered;
    }
}
