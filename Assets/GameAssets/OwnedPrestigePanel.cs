using UnityEngine;
using TMPro;

public class OwnedPrestigePanel : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.OwnedPrestigeChanged.AddListener(UpdatePrestige);
    }

    void Update()
    {
        
    }

    public void UpdatePrestige()
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = GameManager.instance.PrestigePoints.ToString();
    }
}
