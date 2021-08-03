using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance = null;

    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private GameObject torus;
    [SerializeField] private ParticleSystem confetti;

    public Camera OrthographicCamera { get => orthographicCamera; set => orthographicCamera = value; }
    public GameObject Torus { get => torus; set => torus = value; }
    public ParticleSystem Confetti { get => confetti; set => confetti = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
   
}
