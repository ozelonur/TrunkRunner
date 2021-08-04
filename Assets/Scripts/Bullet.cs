using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour, IProperty
{
    private ObjectManager objectManager;
    private Rigidbody bulletRigidBody;
    private PlayerController player;
    private CanvasManager canvasManager;
    private GameManager gameManager;
    private PipeController pipeController;


    private float bulletSpeed;
    private bool isCollided = false;

    private void Start()
    {
        objectManager = ObjectManager.Instance;
        player = PlayerController.Instance;
        canvasManager = CanvasManager.Instance;
        gameManager = GameManager.Instance;
        pipeController = PipeController.Instance;
        bulletRigidBody = GetComponent<Rigidbody>();

        bulletSpeed = PlayerController.Instance.PlayerSettings.BulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IBulletProperty>()?.BulletHit(gameObject);
    }
    public void Interact()
    {
        if (!isCollided && player.CurrentShootState == ShootState.Collecting)
        {
            isCollided = true;
            gameManager.BulletCount++;
            player.Bullets.Add(gameObject);
            pipeController.RewindScale();
            canvasManager.UpdateBulletCount();
            transform.DOMove(objectManager.Torus.transform.position, .5f);
        }
    }

}
