using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    private ObjectManager objectManager;
    private PlayerSettings playerSettings;
    private PlayerController player;
    private CameraController mainCamera;

    private Rigidbody playerRigidBody;
    private Animator playerAnimator;

    private Vector3 difference;
    private Vector3 firstPosition;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = ObjectManager.Instance;
        playerSettings = PlayerController.Instance.PlayerSettings;
        player = PlayerController.Instance;
        mainCamera = CameraController.Instance;
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        firstPosition = Vector3.Lerp(firstPosition, mousePosition, .1f);
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseHold(Input.mousePosition);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -playerSettings.XRange, playerSettings.XRange), transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            playerRigidBody.velocity = Vector3.Lerp(playerRigidBody.velocity, new Vector3(difference.x, playerRigidBody.velocity.y, playerSettings.ForwardSpeed), 1f);
            playerAnimator.SetBool(Constants.ANIM_RUN, true);
        }
        else if (player.CurrentGameMode == GameMode.Dead)
        {
            playerRigidBody.velocity = Vector3.zero;
            playerAnimator.SetBool(Constants.ANIM_RUN, false);
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(Constants.ANIM_DIE))
            {
                playerAnimator.SetTrigger(Constants.ANIM_DIE);
                mainCamera.transform.DOMoveZ(mainCamera.transform.position.z - .2f, .2f);
            }
        }
        else if (player.CurrentGameMode == GameMode.Complete)
        {
            playerRigidBody.velocity = Vector3.zero;
            playerAnimator.SetBool(Constants.ANIM_RUN, false); 
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(Constants.ANIM_DANCE))
            {
                playerAnimator.SetTrigger(Constants.ANIM_DANCE);
                mainCamera.transform.DOMoveZ(mainCamera.transform.position.z - .2f, .2f);
            }
        }
    }
    private void MouseHold(Vector3 inputPosition)
    {
        mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        difference = mousePosition - firstPosition;
        difference *= playerSettings.Sensivity;
    }

    private void MouseUp()
    {
        difference = Vector3.zero;
    }

    private void MouseDown(Vector3 inputPosition)
    {
        mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        firstPosition = mousePosition;
    }
}
