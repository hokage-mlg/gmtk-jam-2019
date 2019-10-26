using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Camera mainCam;
    private GameObject player;
    [SerializeField]
    private bool ShouldRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        var screenPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        screenPoint.z = 0;
        //Vector3 mousePos = Input.mousePosition;
        transform.position = screenPoint;

        if (ShouldRotate) RotateFromCharacter(mousePos);
    }

    // Rotate cursor towards main character
    void RotateFromCharacter(Vector3 mousePos)
    {
        var characterPos = mainCam.WorldToScreenPoint(player.transform.localPosition);
        var offset = new Vector2(mousePos.x - characterPos.x, mousePos.y - characterPos.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
