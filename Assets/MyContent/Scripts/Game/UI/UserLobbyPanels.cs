using System.Collections;
using System.Collections.Generic;
using Utils.Consts;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class UserLobbyPanels : MonoBehaviourPunCallbacks
{
    private const string LOGIN = "Login";
    private const string SELECTION = "Selection";
    private const string CREATE_ROOM = "CreateRoom";
    private const string RANDOM_ROOM = "RandomRoom";
    private const string LIST_ROOMS = "ListRoom";
    private const string HOW_TO_PLAY = "HowToPlay";
    private const string CREDITS = "Credits";
    private const string INSIDE_ROOM = "InsideRoom";
    private const string LOADING = "Loading";
    private const string PATH_INSIDE_ROOM_PLAYER = "Prefabs/PlayerRoom";
    private const string PATH_LIST_OBJECT_ROOM = "Prefabs/ListObjectRoom";

    // Login
    public GameObject loginPanel;
    public TMP_InputField inputPlayerName;

    // Selection
    public GameObject selectionPanel;
    public TMP_InputField inputRoomName;
    public TMP_InputField inputMaxPlayers;

    // Create Room
    public GameObject createRoomPanel;

    // Random Room
    public GameObject randomRoomPanel;

    // List Room
    public GameObject listRoomPanel;
    public GameObject roomListContent;
    
    // How to play
    public GameObject howToPlayPanel;

    // Credits
    public GameObject creditsPanel;
    
    // Loading
    public GameObject LoadingPanel;
    public UserProgress progress;
    
    // Inside Room
    public GameObject insideRoomPanel;
    public Button startGameButton;

    private readonly Dictionary<string, EventsAction> _onActivePanel = new Dictionary<string, EventsAction>();
    private Dictionary<string, RoomInfo> _cachedRoomList;
    private Dictionary<string, GameObject> _roomListEntries;
    private Dictionary<int, GameObject> _playerListEntries;
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        _cachedRoomList = new Dictionary<string, RoomInfo>();
        _roomListEntries = new Dictionary<string, GameObject>();

        inputPlayerName.text = "Player " + (int)Rand.Range(1000, 10000);
        //loadBalancingClient.ConnectToRegionMaster("us");
    }

    private void Start()
    {
        _onActivePanel.Add(LOGIN, new EventsAction());
        _onActivePanel[LOGIN].Add(() => {
            DisableAllPanels();
            loginPanel.SetActive(true);
        });

        _onActivePanel.Add(SELECTION, new EventsAction());
        _onActivePanel[SELECTION].Add(() => {
            DisableAllPanels();
            selectionPanel.SetActive(true);
        });

        _onActivePanel.Add(CREATE_ROOM, new EventsAction());
        _onActivePanel[CREATE_ROOM].Add(() => {
            DisableAllPanels();
            createRoomPanel.SetActive(true);
        });

        _onActivePanel.Add(RANDOM_ROOM, new EventsAction());
        _onActivePanel[RANDOM_ROOM].Add(() => {
            DisableAllPanels();
            randomRoomPanel.SetActive(true);
        });

        _onActivePanel.Add(LIST_ROOMS, new EventsAction());
        _onActivePanel[LIST_ROOMS].Add(() => {
            DisableAllPanels();
            listRoomPanel.SetActive(true);
        });

        _onActivePanel.Add(INSIDE_ROOM, new EventsAction());
        _onActivePanel[INSIDE_ROOM].Add(() => {
            DisableAllPanels();
            insideRoomPanel.SetActive(true);
        });
        
        _onActivePanel.Add(HOW_TO_PLAY, new EventsAction());
        _onActivePanel[HOW_TO_PLAY].Add(() => {
            DisableAllPanels();
            howToPlayPanel.SetActive(true);
        });
        
        _onActivePanel.Add(CREDITS, new EventsAction());
        _onActivePanel[CREDITS].Add(() => {
            DisableAllPanels();
            creditsPanel.SetActive(true);
        });
        
        _onActivePanel.Add(LOADING, new EventsAction());
        _onActivePanel[LOADING].Add(() => {
            DisableAllPanels();
            LoadingPanel.SetActive(true);
        });
    }

    private void DisableAllPanels()
    {
        loginPanel.SetActive(false);
        selectionPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        randomRoomPanel.SetActive(false);
        listRoomPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        insideRoomPanel.SetActive(false);
        LoadingPanel.SetActive(false);
    }

    public void SetActivePanel(string activePanel)
    {
        _onActivePanel[activePanel].Execute();
    }

    #region INSIDE-ROOM
    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient /*|| PhotonNetwork.PlayerList.Length < 2*/) return false;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(UserGame.PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void LocalPlayerPropertiesUpdated()
    {
        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    #endregion INSIDE-ROOM

    #region ROOM-LIST
    private void UpdateRoomListView()
    {
        foreach (var info in _cachedRoomList.Values)
        {
            var objectToInstantiate = Resources.Load(PATH_LIST_OBJECT_ROOM);
            var entry = Instantiate((GameObject)objectToInstantiate);
            entry.transform.SetParent(roomListContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<UserRoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, info.MaxPlayers);

            _roomListEntries.Add(info.Name, entry);
        }
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (_cachedRoomList.ContainsKey(info.Name))
                {
                    _cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            // Update cached room info
            if (_cachedRoomList.ContainsKey(info.Name))
            {
                _cachedRoomList[info.Name] = info;
            }
            // Add new room info to cache
            else
            {
                _cachedRoomList.Add(info.Name, info);
            }
        }
    }

    private void ClearRoomListView()
    {
        foreach (var entry in _roomListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        _roomListEntries.Clear();
    }
    #endregion ROOM-LIST

    #region PUN-CALLBACKS
    public override void OnConnectedToMaster()
    {
        this.SetActivePanel(SELECTION);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();

        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }

    public override void OnLeftLobby()
    {
        _cachedRoomList.Clear();

        ClearRoomListView();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetActivePanel(SELECTION);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        SetActivePanel(SELECTION);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + (int)Rand.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 8 };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        SetActivePanel(INSIDE_ROOM);

        if (_playerListEntries == null)
        {
            _playerListEntries = new Dictionary<int, GameObject>();
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            var objectToInstantiate = Resources.Load(PATH_INSIDE_ROOM_PLAYER);
            
            //Players canvas in gameObject
            var canvasPlayers = insideRoomPanel.transform.GetChild(0);
            var  entry = Instantiate((GameObject)objectToInstantiate, canvasPlayers, true);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<UserPlayerListEntry>().Initialize(p.ActorNumber, p.NickName);

            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(UserGame.PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<UserPlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            }

            _playerListEntries.Add(p.ActorNumber, entry);
        }

        startGameButton.gameObject.SetActive(CheckPlayersReady());

        Hashtable props = new Hashtable
            {
                {UserGame.PLAYER_LOADED_LEVEL, false}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public override void OnLeftRoom()
    {
        SetActivePanel(SELECTION);

        foreach (GameObject entry in _playerListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        _playerListEntries.Clear();
        _playerListEntries = null;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        var objectToInstantiate = Resources.Load(PATH_INSIDE_ROOM_PLAYER);
        GameObject entry = Instantiate((GameObject)objectToInstantiate);
        //Players canvas in gameObject
        var canvasPlayers = insideRoomPanel.transform.GetChild(0);
        entry.transform.SetParent(canvasPlayers.transform);
        entry.transform.localScale = Vector3.one;
        entry.GetComponent<UserPlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

        _playerListEntries.Add(newPlayer.ActorNumber, entry);

        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(_playerListEntries[otherPlayer.ActorNumber].gameObject);
        _playerListEntries.Remove(otherPlayer.ActorNumber);

        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            startGameButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (_playerListEntries == null)
        {
            _playerListEntries = new Dictionary<int, GameObject>();
        }

        GameObject entry;
        if (_playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
        {
            object isPlayerReady;
            if (changedProps.TryGetValue(UserGame.PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<UserPlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            }
        }

        startGameButton.gameObject.SetActive(CheckPlayersReady());
    }
    #endregion PUN-CALLBACKS

    #region UI
    public void OnLoginButton()
    {
        string userID = inputPlayerName.text;

        if (!string.IsNullOrEmpty(userID))
        {
            PhotonNetwork.LocalPlayer.NickName = userID;
            PhotonNetwork.ConnectUsingSettings();
        }

        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public void OnBackButton()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        SetActivePanel(SELECTION);
    }

    public void OnLeaveGameButton()
    {
        //Execute -> LeftRoom callback
        PhotonNetwork.LeaveRoom();
    }

    public void OnCreateRoomButton()
    {
        string roomName = inputRoomName.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room#" + (int)Rand.Range(1000, 10000) : roomName;

        byte maxPlayers;
        byte.TryParse(inputMaxPlayers.text, out maxPlayers);
        maxPlayers = (byte)Mathf.Clamp(maxPlayers, 2, 8);

        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers };
        //Execute -> OnJoinedRoom callback
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public void OnRandomRoomButton()
    {
        SetActivePanel(RANDOM_ROOM);
        //Execute -> OnJoinedRoom callback
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnRoomListButton()
    {
        if (!PhotonNetwork.InLobby)
        {
            //Execute ->  callback
            PhotonNetwork.JoinLobby();
        }

        SetActivePanel(LIST_ROOMS);
    }

    public void OnStartGameButton(string scene)
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel(scene);
        StartCoroutine ("LoadScene");
    }

    private IEnumerator LoadScene()
    {
        while (PhotonNetwork.LevelLoadingProgress < 0.98f)
        {
            progress.SetProgress(PhotonNetwork.LevelLoadingProgress);
            progress.SetText((PhotonNetwork.LevelLoadingProgress * 100).ToString("F0") + "%"); 
            yield return null;
        }
        LoadingPanel.SetActive (false);
    }

    #endregion UI
}
