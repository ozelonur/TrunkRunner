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
        if (!isCollided)
        {
            isCollided = true;
            gameManager.BulletCount++;
            pipeController.RewindScale();
            canvasManager.UpdateBulletCount();
            transform.DOMove(objectManager.Torus.transform.position, .5f);
            Invoke(Constants.HIDE_BULLET, .2f);
            Invoke(Constants.SHOOT, 1.2f);
        }
    }

    public void Shoot()
    {
        gameObject.SetActive(true);
        transform.position = objectManager.Torus.transform.position;
        bulletRigidBody.AddForce(Vector3.forward * bulletSpeed);
        Destroy(gameObject, 3f);
        gameManager.BulletCount--;
        canvasManager.UpdateBulletCount();
    }

    public void HideBullet()
    {
        gameObject.SetActive(false);
    }
}
