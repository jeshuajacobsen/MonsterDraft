using UnityEngine;
using TMPro;

public class OwnedGemPanel : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.OwnedGemsChanged.AddListener(UpdateGems);
    }

    void Update()
    {
        
    }

    private void UpdateGems()
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = GameManager.instance.Gems.ToString();
    }
}
