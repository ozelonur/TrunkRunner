using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PipeController : MonoBehaviour
{
    public static PipeController Instance = null;
    public Action pipeAction;
    private Transform[] bones;
    private ObjectManager objectManager;
    private PlayerController player;
    private bool canTween = true;

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
        objectManager = ObjectManager.Instance;
        player = PlayerController.Instance;
        bones = transform.GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            for (int i = 2; i < bones.Length; i++)
            {
                if (i == 2)
                {
                    bones[i].transform.localEulerAngles = new Vector3(0, 0, 90);
                    bones[i].transform.LookAt(objectManager.Torus.transform);
                    bones[i].transform.Rotate(0, -90, 0);
                    //bones[i].transform.position = new Vector3(bones[i].transform.position.x, bones[i].transform.position.y, objectManager.Torus.transform.position.z);
                    bones[i].transform.position = Vector3.Lerp(bones[i].transform.position, (new Vector3(objectManager.Torus.transform.position.x, bones[i].transform.position.y, objectManager.Torus.transform.position.z)), .2f);
                }
                else
                {
                    bones[i].localEulerAngles = bones[i - 1].localEulerAngles;
                    bones[i].LookAt(bones[i - 1]);
                    bones[i].transform.Rotate(0, -90, 0);
                    bones[i].transform.position = Vector3.Lerp(bones[i].transform.position, (new Vector3(bones[i - 1].transform.position.x, bones[i].transform.position.y, bones[i - 1].transform.position.z)), .2f);
                }
            }
        }

    }

    public void RewindScale()
    {
        if (!canTween) { return; }

        canTween = false;

        Transform bone;

        for (int i = 2; i < 10; i++)
        {
            bone = bones[i].transform;
            if (i != 9)
                bone.DOScale(new Vector3(4f, 4f, 4f), .04f).SetLoops(2, LoopType.Yoyo).SetDelay(i * .06f);
            else
                bone.DOScale(new Vector3(4f, 4f, 4f), .04f).SetLoops(2, LoopType.Yoyo).SetDelay(i * .06f)
                   .OnComplete(() =>
                   {
                       canTween = true;
                   });

        }
    }
    public void DestroyPipe()
    {
        Destroy(gameObject);
    }

}
