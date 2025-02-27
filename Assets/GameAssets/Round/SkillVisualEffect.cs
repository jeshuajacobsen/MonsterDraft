using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Animations;
using Zenject;

public class SkillVisualEffect : MonoBehaviour
{
    public Sprite lightningAttack;

    [Header("Desired size of the sprite (width x height)")]
    public Vector2 desiredSize = new Vector2(1000, 150);

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

    private string attackVisualEffect;
    private float timeout = 0;

    public class Factory : PlaceholderFactory<SkillVisualEffect> { }

    void Awake()
    {
        
    }

    public void Update()
    {

        float step = moveSpeed * Time.deltaTime;
        traveledDistance += step;
        if (attackVisualEffect == "Lightning")
        {
            transform.Find("Mask/Image").GetComponent<RectTransform>().position += (Vector3)(direction * step);
            if (traveledDistance >= distanceToTravel)
            {
                reachedTarget.Invoke();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
        else if (attackVisualEffect == "Bite")
        {
            timeout -= Time.deltaTime;
            if (timeout <= 0)
            {
                reachedTarget.Invoke();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Find("Animation").GetComponent<RectTransform>().position += (Vector3)(direction * step);
            if (traveledDistance >= distanceToTravel)
            {
                reachedTarget.Invoke();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Vector2 startPosition, Vector2 targetPosition, string attackVisualEffect)
    {
        this.attackVisualEffect = attackVisualEffect;
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;

        if (attackVisualEffect == "Lightning")
        {
            transform.Find("Animation").gameObject.SetActive(false);
            transform.Find("Mask").gameObject.SetActive(true);

            spriteRenderer = transform.Find("Mask/Image")?.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                Debug.LogError("SpriteRenderer component not found on 'Mask/Image'!");

            if (attackVisualEffect == "Lightning") 
            {
                spriteRenderer.sprite = lightningAttack;
            }

            SpriteMask mask = transform.Find("Mask").GetComponent<SpriteMask>();

            if (mask == null)
            {
                Debug.LogError("Missing SpriteMask or SpriteRenderer reference!");
                return;
            }

            var maskBoundsSize = mask.bounds.size;
            var spriteBoundsSize = desiredSize;

            float scaleX = spriteBoundsSize.x / maskBoundsSize.x;
            float scaleY = spriteBoundsSize.y / maskBoundsSize.y;

            transform.Find("Mask").localScale = new Vector3(scaleX, scaleY, 1f);

            if (spriteRenderer != null)
            {
                float spriteScaleX = mask.bounds.size.x / spriteRenderer.bounds.size.x;
                float spriteScaleY = mask.bounds.size.y / spriteRenderer.bounds.size.y;
                spriteRenderer.transform.localScale = new Vector3(spriteScaleX, spriteScaleY, 1f);
            }

            transform.position = startPosition;
            gameObject.SetActive(true);

            direction = (targetPosition - startPosition).normalized;
            distanceToTravel = Vector2.Distance(startPosition, targetPosition);
            traveledDistance = 0f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            if (spriteRenderer != null)
                spriteRenderer.flipX = true;

            float halfWidth = transform.Find("Mask").GetComponent<RectTransform>().rect.width / 2;
            transform.Find("Mask").GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfWidth, 0);

            transform.Find("Mask/Image").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        } 
        else if (attackVisualEffect == "Bite") 
        {
            transform.Find("Animation").gameObject.SetActive(true);
            transform.Find("Mask").gameObject.SetActive(false);

            spriteRenderer = transform.Find("Animation")?.GetComponent<SpriteRenderer>();
            RectTransform animationTransform = transform.Find("Animation").GetComponent<RectTransform>();
            animationTransform.localScale = new Vector3(300f / spriteRenderer.bounds.size.x, 300f / spriteRenderer.bounds.size.y, 1f);

            Animator animator = transform.Find("Animation").GetComponent<Animator>();
            animator.SetTrigger("PlayBite");
            animator.Play("bite");

            transform.position = targetPosition;
            gameObject.SetActive(true);

            timeout = .5f;
            spriteRenderer.sortingLayerName = "Foreground";
            spriteRenderer.sortingOrder = 10;
        } 
        else
        {
            transform.Find("Animation").gameObject.SetActive(true);
            transform.Find("Mask").gameObject.SetActive(false);

            spriteRenderer = transform.Find("Animation")?.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
            RectTransform animationTransform = transform.Find("Animation").GetComponent<RectTransform>();
            animationTransform.localScale = new Vector3(300f / spriteRenderer.bounds.size.x, 300f / spriteRenderer.bounds.size.y, 1f);

            Animator animator = transform.Find("Animation").GetComponent<Animator>();

            int extraRotation = 0;
            if (attackVisualEffect == "Water")
            {
                animator.SetTrigger("PlayWave");
                animator.Play("wave");
            } else if(attackVisualEffect == "Fire")
            {
                animator.SetTrigger("PlayFireball");
                animator.Play("fireball");
                extraRotation = 50;
            } else if (attackVisualEffect == "Leaf")
            {
                animator.SetTrigger("PlayLeaf");
                animator.Play("leaf");
            }

            transform.position = startPosition;
            gameObject.SetActive(true);

            direction = (targetPosition - startPosition).normalized;
            distanceToTravel = Vector2.Distance(startPosition, targetPosition);
            traveledDistance = 0f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + extraRotation);
        }
        spriteRenderer.sortingLayerName = "Foreground"; 
        spriteRenderer.sortingOrder = 10;
    }
}
