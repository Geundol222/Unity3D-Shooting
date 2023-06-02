using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork0602
{
    public class GameSetting : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            InitGameManager();
        }

        private static void InitGameManager()
        {
            if (GameManager.Instance == null)
            {
                GameObject gameManager = new GameObject();
                gameManager.name = "GameManager";
                gameManager.AddComponent<GameManager>();
            }
        }
    }
}