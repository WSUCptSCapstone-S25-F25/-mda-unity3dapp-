using UnityEngine;
using System;

namespace BoardComponents
{
    public class BoardManager : MonoBehaviour
    {
        public static BoardManager Instance { get; private set; }
        public GameObject MainBoard { get; private set; }
        public event Action<GameObject> OnBoardChanged;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Start the process: pick a file, load, upgrade, and register the board.
        /// </summary>
        public void LoadNewBoard(string relativePath = "Boards")
        {
            BoardLoader.LoadBoard(relativePath, OnBoardLoaded);
        }

        /// <summary>
        /// Callback from BoardLoader with the raw imported board.
        /// </summary>
        private void OnBoardLoaded(GameObject rawBoard)
        {
            if (rawBoard == null)
            {
                Debug.LogWarning("No board loaded.");
                return;
            }

            GameObject upgradedBoard = UpgradeBoard.Upgrade(rawBoard);

            RegisterBoard(upgradedBoard);
        }

        /// <summary>
        /// Destroys previous main board, sets up new one, and fires event.
        /// </summary>
        public void RegisterBoard(GameObject board)
        {
            if (MainBoard != null)
            {
                Destroy(MainBoard);
                MainBoard = null;
            }

            MainBoard = board;
            MainBoard.transform.SetParent(this.transform);
            MainBoard.transform.localPosition = Vector3.zero;
            MainBoard.transform.localRotation = Quaternion.identity;

            OnBoardChanged?.Invoke(MainBoard);
        }

        public GameObject CreateBoardReplica()
        {
            if (MainBoard == null)
            {
                Debug.LogError("No main board loaded!");
                return null;
            }
            GameObject replica = Instantiate(MainBoard);
            replica.name = "SimBoardReplica";
            replica.SetActive(false);
            return replica;
        }
    }
}

