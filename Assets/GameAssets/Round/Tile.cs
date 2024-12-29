using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Monster monster;
    private Camera mainCamera;
    public DungeonRow dungeonRow;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        
        #else
        HandleTouchInput();
        #endif
    }

    public void HandleMouseDown()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane + 1.0f));
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && collider.OverlapPoint(mousePosition))
        {
            RoundManager.instance.gameState.SelectTile(this, mousePosition);
        }
    }

    public void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane + 1.0f));
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null && collider.OverlapPoint(touchPosition))
            {
                RoundManager.instance.gameState.SelectTile(this, touchPosition);
            }
        }
    }
}
