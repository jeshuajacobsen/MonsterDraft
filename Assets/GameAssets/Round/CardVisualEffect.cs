using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CardVisualEffect : MonoBehaviour
{

    public Sprite effectSprite;

    [Header("Desired size of the sprite (width x height)")]
    public Vector2 desiredSize = new Vector2(150, 150);

    [Header("Movement speed of the sprite")]
    public float moveSpeed = 1000f;

    public UnityEvent reachedTarget;

    // References
    private SpriteRenderer spriteRenderer;
    private RectTransform maskTransform;
    private RectTransform imageTransform;

    // Movement data
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float distanceToTravel;
    private float traveledDistance;
    private Vector3 direction;

    private string visualEffectName;
    private float timeout = 0;

    public class Factory : PlaceholderFactory<CardVisualEffect> { }
    
    void Start()
    {
        
    }

    public void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        traveledDistance += step;
        transform.Find("Animation").GetComponent<RectTransform>().position += (Vector3)(direction * step);
        if (traveledDistance >= distanceToTravel)
        {
            reachedTarget.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 startPosition, Vector2 targetPosition, string visualEffectName)
    {
        this.visualEffectName = visualEffectName;
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;

        transform.Find("Animation").gameObject.SetActive(true);
        //transform.Find("Mask").gameObject.SetActive(false);

        spriteRenderer = transform.Find("Animation")?.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        RectTransform animationTransform = transform.Find("Animation").GetComponent<RectTransform>();
        animationTransform.localScale = new Vector3(300f / spriteRenderer.bounds.size.x, 300f / spriteRenderer.bounds.size.y, 1f);

        Animator animator = transform.Find("Animation").GetComponent<Animator>();

        int extraRotation = 0;
        animator.SetTrigger("PlayFireball");
        animator.Play("fireball");
        extraRotation = 50;

        transform.position = startPosition;
        gameObject.SetActive(true);

        direction = (targetPosition - startPosition).normalized;
        distanceToTravel = Vector2.Distance(startPosition, targetPosition);
        traveledDistance = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + extraRotation);
        
        spriteRenderer.sortingLayerName = "Foreground"; 
        spriteRenderer.sortingOrder = 10;
    }
}
