using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 20f;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 20f;
    [Tooltip("in m")] [SerializeField] float xRange = 5f;
    [Tooltip("in m")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlYawFactor = -30f;

    float xThrow, yThrow;


    // Use this for initialization
    void Start()
    {

     }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        MoveVertical();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToControl + pitchDueToPosition;

        float yaw = transform.localPosition.x * positionYawFactor;  

        float roll = xThrow * controlYawFactor;
                

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void MoveVertical()
    {
        float NewYPos;
        yThrow = CrossPlatformInputManager.GetAxis("Vertical"); //recieve input
        float yOffset = yThrow * ySpeed * Time.deltaTime; // find out how much movement to be made in m per frame accounting for frame load times
        GetNewAxisPosition("y", yThrow, out NewYPos); //get new position 
        transform.localPosition = new Vector3(transform.localPosition.x, NewYPos, transform.localPosition.z); //move to new position
    }

    private void MoveHorizontal()
    {
        float NewXPos;
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        GetNewAxisPosition("x", xThrow, out NewXPos);
        transform.localPosition = new Vector3(NewXPos, transform.localPosition.y, transform.localPosition.z);
    }
    

    private void GetNewAxisPosition(string axis, float posThrow, out float clampedNewPos)
    {
        clampedNewPos = 0f; //need to assign for compiler

        if (axis == "x")
        {
            float xOffset = posThrow * xSpeed * Time.deltaTime;
            float rawNewXPos = transform.localPosition.x + xOffset;
            clampedNewPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        }

        if (axis == "y")
        {
            float yOffset = posThrow * ySpeed * Time.deltaTime;
            float rawNewYPos = transform.localPosition.y + yOffset;
            clampedNewPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);
        }

    }
}