using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SmallCardView : MonoBehaviour
{
    public Card card;

    private Camera mainCamera;
    public bool isDragging = false;
    private Vector3 originalPosition;
    private ScrollRect scrollRect;
    private Transform originalParent;
    private Canvas dragCanvas;
    private Canvas originalCanvas;

    void Start()
    {
        // Cache the main camera reference
        mainCamera = Camera.main;

        scrollRect = GetComponentInParent<ScrollRect>();
        GameObject canvasObject = GameObject.FindGameObjectWithTag("DraggableCanvas");
        if (canvasObject != null)
        {
            dragCanvas = canvasObject.GetComponent<Canvas>();
        }
        else
        {
            Debug.LogError("DragCanvas not found! Make sure the Canvas has the correct tag.");
        }

        originalCanvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {

    }

    public void HandleMouseDown(Vector2 mousePosition)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return;
            }

            if (result.gameObject == gameObject)
            {
                isDragging = true;
                originalPosition = transform.position;
                originalParent = transform.parent;
                if (dragCanvas != null)
                {
                    MoveToCanvas(dragCanvas);
                }

                if (scrollRect != null)
                {
                    scrollRect.enabled = false;
                }
                return;
            }
        }
    }

    public void HandleDrag(Vector2 mousePosition)
    {
        if (isDragging)
        {
            Vector3 worldPosition3D = mainCamera.ScreenToWorldPoint(new Vector3(
                mousePosition.x,
                mousePosition.y,
                0f
            ));

            Vector2 worldPosition2D = new Vector2(worldPosition3D.x, worldPosition3D.y);
            transform.position = worldPosition2D;
        }
    }

    public void CancelPlay()
    {
        MoveToCanvas(originalCanvas);
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        if (scrollRect != null)
        {
            scrollRect.enabled = true;
        }
        isDragging = false;
    }

    private void MoveToCanvas(Canvas targetCanvas)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Convert World Position to Local Position in the target Canvas
        Vector3 worldPosition = rectTransform.position;
        Vector3 localPosition = targetCanvas.transform.InverseTransformPoint(worldPosition);

        // Set the new parent and position
        transform.SetParent(targetCanvas.transform, false);
        rectTransform.localPosition = localPosition;

        // Match the scale
        Vector3 scaleFactor = targetCanvas.transform.lossyScale;
        rectTransform.localScale = new Vector3(1 / scaleFactor.x, 1 / scaleFactor.y, 1 / scaleFactor.z);
    }

    public void InitValues(Card card)
    {
        this.card = card;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("CardImage").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(card.Name);
        //transform.Find("CardCost").GetComponent<TextMesh>().text = card.cost.ToString();
        //transform.Find("CardDescription").GetComponent<TextMesh>().text = card.Description;
    }
}
