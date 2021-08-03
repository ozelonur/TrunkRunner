using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, IProperty
{
    PlayerController player;
    GameManager gameManager;

    private void Start()
    {
        player = PlayerController.Instance;
        gameManager = GameManager.Instance;
    }
    public void Interact()
    {
        gameManager.gameComplete();

    }
}
