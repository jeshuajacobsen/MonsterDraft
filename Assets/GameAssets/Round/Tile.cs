using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

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
}
