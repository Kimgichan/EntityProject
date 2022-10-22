using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private VirtualJoystick joyStick;
    // Start is called before the first frame update
    void Start()
    {
        joyStick.Drag += (v) =>
        {
            var pos = transform.position;
            pos += (Vector3)v * GameManager.Instance.GameController.PlayerMoveSpeed * Time.deltaTime;
            transform.position = pos;
        };
    }

}
