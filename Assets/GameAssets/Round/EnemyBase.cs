using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int _health;
    public int Health { 
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            GameObject healthBar = transform.Find("HealthBar/BarFill").gameObject;
            if (healthBar != null)
            {
                RectTransform barRectTransform = healthBar.GetComponent<RectTransform>();

                float healthPercent = Mathf.Clamp01((float)_health / MaxHealth);

                barRectTransform.localScale = new Vector3(healthPercent, 1, 1);
            }
            if (_health <= 0)
            {
                
            }
        }
    }

    public int MaxHealth { get; set; }
    
    void Start()
    {
        Health = 100;
    }

    void Update()
    {
        
    }
}
