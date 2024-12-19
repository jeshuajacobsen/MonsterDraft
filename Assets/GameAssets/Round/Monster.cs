using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{

    public string name;
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string name, MonsterCard monsterCard)
    {
        this.name = name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(name);
        this.Health = monsterCard.Health;
        this.Attack = monsterCard.Attack;
        this.Defense = monsterCard.Defense;
    }
}
