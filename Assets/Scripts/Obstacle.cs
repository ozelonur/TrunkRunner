using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IProperty, IBulletProperty
{
    private GameManager gameManager;
    private bool isCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void Interact()
    {
        gameManager.gameOver();
    }

    public void BulletHit(GameObject bullet)
    {
        if (!isCollided)
        {
            isCollided = true;
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}
