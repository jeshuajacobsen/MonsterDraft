using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDirections : MonoBehaviour
{
    string directions;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetDirections(string directions)
    {
        this.directions = directions;
        string[] directionArray = directions.Split(' ');
        
        transform.Find("LeftArrow").gameObject.SetActive(false);
        transform.Find("RightArrow").gameObject.SetActive(false);
        transform.Find("UpArrow").gameObject.SetActive(false);
        transform.Find("DownArrow").gameObject.SetActive(false);
        transform.Find("UpLeftArrow").gameObject.SetActive(false);
        transform.Find("UpRightArrow").gameObject.SetActive(false);
        transform.Find("DownLeftArrow").gameObject.SetActive(false);
        transform.Find("DownRightArrow").gameObject.SetActive(false);

        if (directionArray.Length == 0)
        {
            Debug.Log("No directions");
            return;
        }

        foreach (string direction in directionArray)
        {
            switch (direction)
            {
                case "Up":
                    transform.Find("UpArrow").gameObject.SetActive(true);
                    break;
                case "Down":
                    transform.Find("DownArrow").gameObject.SetActive(true);
                    break;
                case "Backward":
                    transform.Find("LeftArrow").gameObject.SetActive(true);
                    break;
                case "Forward":
                    transform.Find("RightArrow").gameObject.SetActive(true);
                    break;
                case "DiagonalBackward":
                    transform.Find("UpLeftArrow").gameObject.SetActive(true);
                    transform.Find("DownLeftArrow").gameObject.SetActive(true);
                    break;
                case "DiagonalForward":
                    transform.Find("UpRightArrow").gameObject.SetActive(true);
                    transform.Find("DownRightArrow").gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("Invalid direction");
                    break;
            }
        }
    }
}
