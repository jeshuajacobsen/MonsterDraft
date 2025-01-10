using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    }

    public void HandlePointerDown(Vector2 pointerPosition)
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("No RectTransform found on this GameObject. " +
                            "RectangleContainsScreenPoint requires a RectTransform.");
            return;
        }

        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, pointerPosition, mainCamera))
        {
            Vector3 worldPosition3D = mainCamera.ScreenToWorldPoint(new Vector3(
                pointerPosition.x,
                pointerPosition.y,
                0f
            ));

            Vector2 worldPosition2D = new Vector2(worldPosition3D.x, worldPosition3D.y);

            RoundManager.instance.gameState.SelectTile(this, worldPosition2D);
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
