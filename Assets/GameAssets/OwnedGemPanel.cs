using UnityEngine;
using TMPro;
using Zenject;

public class OwnedGemPanel : MonoBehaviour
{
    private IGameManager _gameManager;

    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        _gameManager.OwnedGemsChanged.AddListener(UpdateGems);
    }

    void Update()
    {
        
    }

    private void UpdateGems()
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = _gameManager.Gems.ToString();
    }
}
