using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GameMode
{
    Start,
    Playing,
    Dead,
    Complete
}

public enum ShootState
{
    Shooting, 
    Collecting
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;
    [SerializeField] private PlayerSettings playerSettings;

    private GameManager gameManager;
    private CanvasManager canvasManager;
    private ObjectManager objectManager;
    private PipeController pipeController;

    private List<GameObject> bullets;


    private GameMode currentGameMode;
    private ShootState currentShootState;
    public PlayerSettings PlayerSettings { get => playerSettings; }
    public GameMode CurrentGameMode { get => currentGameMode; set => currentGameMode = value; }
    public List<GameObject> Bullets { get => bullets; set => bullets = value; }
    public ShootState CurrentShootState { get => currentShootState; set => currentShootState = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentGameMode = GameMode.Start;
        gameManager = GameManager.Instance;
        canvasManager = CanvasManager.Instance;
        objectManager = ObjectManager.Instance;
        pipeController = PipeController.Instance;
        gameManager.gameOver += GameOver;
        gameManager.gameComplete += GameComplete;
        gameManager.gameComplete += LookAtTheCamera;
        gameManager.gameComplete += LaunchConfetti;
        gameManager.gameComplete += pipeController.DestroyPipe;
        gameManager.gameOver += pipeController.DestroyPipe;

        bullets = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IProperty>()?.Interact();
    }

    private void GameOver()
    {
        CurrentGameMode = GameMode.Dead;
        Invoke(Constants.SHOW_UI, 1.5f);
        Destroy(objectManager.Torus);
    }

    private void GameComplete()
    {
        CurrentGameMode = GameMode.Complete;
        LevelManager.Instance.LevelIndex++;
        Destroy(objectManager.Torus);
        transform.DOLookAt(Camera.main.transform.position, .3f);
        Invoke(Constants.SHOW_UI, 1.5f);
    }

    private void ShowUI()
    {
        canvasManager.TapText.gameObject.SetActive(true);
        if (currentGameMode == GameMode.Complete)
        {
            canvasManager.TapText.text = Constants.NEXT_LEVEL;
        }
        else if (currentGameMode == GameMode.Dead)
        {
            canvasManager.TapText.text = Constants.TRY_AGAIN;
        }
    }

    private void LookAtTheCamera()
    {
        Vector3 camPos = Camera.main.transform.position;

        transform.DOLookAt(new Vector3(camPos.x, transform.position.y, camPos.z), 3.5f);
    }

    private void LaunchConfetti()
    {
        Instantiate(objectManager.Confetti, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z + 1), transform.rotation);
    }


    public IEnumerator ShootBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            print(bullets.Count);
           
        }

        while (bullets.Count > 0)
        {
            yield return new WaitForSeconds(.5f);
            bullets[0].SetActive(true);
            bullets[0].transform.position = objectManager.Torus.transform.position;
            bullets[0].GetComponent<Rigidbody>().AddForce(Vector3.forward * 1000);
            Destroy(bullets[0], 3);
            bullets.RemoveAt(0);
            gameManager.BulletCount--;
            canvasManager.UpdateBulletCount();
        }
    }

}
