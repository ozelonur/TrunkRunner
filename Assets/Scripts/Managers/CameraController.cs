using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance = null;
    private PlayerController player;

    private Vector3 offset;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance;
        offset = new Vector3(transform.position.x, transform.position.y, transform.position.z - player.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
