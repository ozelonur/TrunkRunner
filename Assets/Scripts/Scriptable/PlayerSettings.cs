using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "PlayerSettings/PlayerSetting", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float sensivity;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float xRange;
    [SerializeField] private float bulletSpeed;

    public float Sensivity { get => sensivity; }
    public float ForwardSpeed { get => forwardSpeed; }
    public float XRange { get => xRange; }
    public float BulletSpeed { get => bulletSpeed; }
}
