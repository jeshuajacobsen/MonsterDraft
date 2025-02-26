using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VisualEffect
{
    GameObject effect;
    Tile target;
    public UnityEvent reachedTarget = new UnityEvent();
    public bool done = false;

    void Start()
    {
    }

    public void Update()
    {
        if (effect != null && target != null)
        {
            float step = 5000 * Time.deltaTime;
            effect.transform.position = Vector3.MoveTowards(effect.transform.position, target.transform.position, step);

            if (Vector3.Distance(effect.transform.position, target.transform.position) <= step)
            {
                reachedTarget.Invoke();
                effect.SetActive(false);
                done = true;
            }
        }
    }

    public void Initialize(GameObject effect, Vector2 startPosition, Tile target)
    {
        effect.gameObject.SetActive(true);
        
        effect.transform.position = startPosition;
        Vector3 direction = (target.transform.position - effect.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        this.effect = effect;
        this.target = target;

    }

}