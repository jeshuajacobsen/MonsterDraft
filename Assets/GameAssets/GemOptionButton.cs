using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GemOptionButton : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private int gemAmount;
    private IGameManager _gameManager;

    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_gameManager == null)
        {
            return;
        }

        _gameManager.Gems += gemAmount;
    }
}
