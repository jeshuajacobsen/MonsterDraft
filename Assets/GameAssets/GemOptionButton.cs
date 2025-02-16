using UnityEngine;
using UnityEngine.UI;

public class GemOptionButton : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private int gemAmount;
    
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        
    }

    private void OnClick()
    {
        GameManager.instance.Gems += gemAmount;
    }
}
