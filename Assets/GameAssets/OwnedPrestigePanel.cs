using UnityEngine;
using TMPro;
using Zenject;

public class OwnedPrestigePanel : MonoBehaviour
{
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        _gameManager.OwnedPrestigeChanged.AddListener(UpdatePrestige);
    }

    void Update()
    {
        
    }

    public void UpdatePrestige()
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = _gameManager.PrestigePoints.ToString();
    }
}
