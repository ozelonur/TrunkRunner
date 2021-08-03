using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour, IProperty
{
    private ObjectManager objectManager;
    private CanvasManager canvasManager;
    private GameManager gameManager;
    private PipeController pipeController;

    private bool isCollided = false;
    
    // Start is called before the first frame update
    void Start()
    {
        objectManager = ObjectManager.Instance;
        canvasManager = CanvasManager.Instance;
        gameManager = GameManager.Instance;
        pipeController = PipeController.Instance;
        transform.DORotate(new Vector3(transform.localEulerAngles.x, Random.Range(20, 40), transform.localEulerAngles.z), .2f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental);
    }


    public void Interact()
    {
        if (!isCollided)
        {
            pipeController.RewindScale();
            gameManager.DiamondCount++;
            canvasManager.UpdateDiamondCount();
            transform.DOMove(objectManager.Torus.transform.position, .4f);
            Destroy(gameObject, .3f);
        }
        
    }
}
