using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance = null;

    private GameManager gameManager;

    [SerializeField] private Text tapText;
    [SerializeField] private Text diamondText;
    [SerializeField] private Text bulletText;

    public Text TapText { get => tapText; set => tapText = value; }
    public Text DiamondText { get => diamondText; set => diamondText = value; }
    public Text BulletText { get => bulletText; set => bulletText = value; }

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
        diamondText.text = PlayerPrefs.GetInt(Constants.DIAMOND_COUNT, 0).ToString();
        bulletText.text = PlayerPrefs.GetInt(Constants.BULLET_COUNT, 0).ToString();
    }

    public void UpdateDiamondCount()
    {
        diamondText.text = PlayerPrefs.GetInt(Constants.DIAMOND_COUNT, 0).ToString();
    }

    public void UpdateBulletCount()
    {
        bulletText.text = PlayerPrefs.GetInt(Constants.BULLET_COUNT, 0).ToString();
    }
}
