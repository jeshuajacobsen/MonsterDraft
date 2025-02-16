using UnityEngine;

public class CardImprovementPanel : MonoBehaviour
{
    void Start()
    {
        transform.parent.parent.Find("CloseButton").GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(GameManager.instance.CloseCardImprovementPanel);
    }

    void Update()
    {
        
    }

    public void OnOpen()
    {
        
    }
}
