using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private Text fpsTxt;

    // Update is called once per frame
    void Update()
    {
        fpsTxt.text = $"{string.Format("{0,10:N2}", 1f / Time.deltaTime)}fps";
    }
}
