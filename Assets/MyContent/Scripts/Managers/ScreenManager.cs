using Events;
using UnityEngine;

public class ScreenManager : MonoBehaviour, IObserverMainEvents
{
    private const string SCREEN_IN_GAME_NAME = "InGame";
    private const string SCREEN_GAME_OVER_NAME = "GameOver";
    private const string SCREEN_GAME_COMPLETED_NAME = "GameCompleted";
    private static ScreenManager _instance;
    public static ScreenManager Instance => _instance;

    public GameObject InGame;
    public GameObject GameOver;
    public GameObject GameCompleted;
    
    #region MonoBehavior

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            _instance = this;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        // GameManager.Instance.SubscribeCompletedGame(OnCompletedGame);
        // GameManager.Instance.SubscribeGameOver(OnGameOver);
        OnChangeScreen(SCREEN_IN_GAME_NAME);
    }
    #endregion MonoBehavior

    #region IObserverMainEvents
    public void OnCompletedGame()
    {
        OnChangeScreen(SCREEN_GAME_COMPLETED_NAME);
    }

    public void OnGameOver()
    {
        OnChangeScreen(SCREEN_GAME_OVER_NAME);
    }
    #endregion vIObserverMainEvents

    public void OnChangeScreen(string activeScreen)
    {
        GameCompleted.SetActive(GameCompleted.transform.name == activeScreen);
        GameOver.SetActive(GameOver.transform.name == activeScreen);
        InGame.SetActive(InGame.transform.name == activeScreen);
    }
}
