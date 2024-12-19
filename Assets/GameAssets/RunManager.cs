using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    
    public RunDeck runDeck;

    [SerializeField] private GameObject roundPanel;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRun()
    {
        runDeck = new RunDeck(GameManager.instance.selectedInitialDeck);
        StartRound();
    }

    public void StartRound()
    {
        roundPanel.gameObject.SetActive(true);
        RoundManager.instance.StartRound();
    }
}