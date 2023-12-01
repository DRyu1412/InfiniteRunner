using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody rigid;

    [SerializeField] Transform[] LaneTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;

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
        rigid = GetComponent<Rigidbody>();

        playerInput.gameplay.Move.performed += MovePerformed;
        playerInput.gameplay.Jump.performed += JumpPerformed;

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
        float TranformX = Mathf.Lerp(transform.position.x, destination.x, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(TranformX, transform.position.y, transform.position.z);

    }

    private void MovePerformed(InputAction.CallbackContext obj)
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

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
        float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
        rigid.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f), ForceMode.VelocityChange);
    }
}
