using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField]
    Transform[] LaneTransform;

    Vector3 destination;
    int currentLaneIndex;

    private void OnEnable()
    {
        if (playerInput == null)
            playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.gameplay.Move.performed += MovePerformed;
        for(int i = 0; i< LaneTransform.Length; i++)
        {
            if(LaneTransform[i].position == transform.position)
            {
                currentLaneIndex = i;
                destination = LaneTransform[i].position;
            }
        }
    }
    void Update()
    {
        transform.position = destination;
    }

    private void MovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        float inputValue = obj.ReadValue<float>();
        if(inputValue < 0)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        if (currentLaneIndex == 0)
            return;
        currentLaneIndex--;
        destination = LaneTransform[currentLaneIndex].position;
    }

    private void MoveRight()
    {
        if (currentLaneIndex == LaneTransform.Length - 1)
            return;
        currentLaneIndex++;
        destination = LaneTransform[currentLaneIndex].position;
    }
}
