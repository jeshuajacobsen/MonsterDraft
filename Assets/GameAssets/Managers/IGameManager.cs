using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGameManager
{
    IGameData GameData { get; }
    InitialDeck SelectedInitialDeck { get; }
    List<string> UnlockedDungeonLevels { get; }
    Dictionary<string, int> CardLevels { get; }

    int PrestigePoints { get; set; }
    int Gems { get; set; }

    UnityEvent StartRunEvent { get; }
    UnityEvent OwnedPrestigeChanged { get; }
    UnityEvent OwnedGemsChanged { get; }

    LargeCardView LargeCardView1 { get; }
    LargeCardView LargeCardView2 { get; }
    LargeCardView LargeCardView3 { get; }

    void AddUnlockedDungeonLevel(string level);
    void StartRun();
    void OpenDeckEditor();
    void CloseDeckEditor();
    void OpenCardImprovementPanel();
    void CloseCardImprovementPanel();
    void OpenSelectedCardImprovementPanel(Card card);
    void CloseSelectedCardImprovementPanel();
    void SaveGame();
    void LoadGame();
}
