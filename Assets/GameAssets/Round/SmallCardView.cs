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
        #if UNITY_EDITOR || UNITY_STANDALONE
            if (isDragging)
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane + 1.0f));
                transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            }
        #else
        HandleTouchInput();
        #endif
    }

    public void HandleMouseDown()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
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

    public void HandleMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            MoveToCanvas(originalCanvas);
            transform.SetParent(originalParent);

            if (scrollRect != null)
            {
                scrollRect.enabled = true;
            }
        }
        // if (isDragging)
        // {
        //     isDragging = false;
        //     MoveToCanvas(originalCanvas);
        //     transform.SetParent(originalParent);

        //     // Ensure consistent screen space coordinates
        //     Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, transform.position);
        //     RectTransform parentRect = originalParent.GetComponent<RectTransform>();

        //     bool isInsideOriginalParent = RectTransformUtility.RectangleContainsScreenPoint(
        //         parentRect,
        //         screenPoint,
        //         mainCamera
        //     );

        //     if (isInsideOriginalParent)
        //     {
        //         CancelPlay();
        //     }
        //     else
        //     {
        //         if (RoundManager.instance.gameState.CanPlayCard(card, transform.position))
        //         {
        //             RoundManager.instance.gameState.PlayCard(card, transform.position);
        //             Destroy(gameObject);
        //         }
        //         else
        //         {
        //             CancelPlay();
        //         }
        //     }

        //     if (scrollRect != null)
        //     {
        //         scrollRect.enabled = true;
        //     }
        // }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane + 1.0f));
                transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
            }

            if (touch.phase == TouchPhase.Began)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = touch.position
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject == gameObject)
                    {
                        isDragging = true;
                        originalPosition = transform.position;
                        
                        if (dragCanvas != null)
                        {
                            MoveToCanvas(dragCanvas);
                        }

                        // Disable the ScrollRect
                        if (scrollRect != null)
                        {
                            scrollRect.enabled = false;
                        }
                        return;
                    }
                }
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isDragging)
            {
                isDragging = false;
                MoveToCanvas(originalCanvas);
                transform.SetParent(originalParent);
                transform.position = originalPosition;

                if (scrollRect != null)
                {
                    scrollRect.enabled = true;
                }
            }
        }
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
