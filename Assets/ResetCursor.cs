using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCursor : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}