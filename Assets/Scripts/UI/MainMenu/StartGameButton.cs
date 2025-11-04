using System;
using UnityEngine;
using UnityEngine.UI;

namespace ODG.UI.MainMenuUI
{
    public class StartGameButton : MonoBehaviour
    {
        public event Action BeforeGameStart;
        [SerializeField] private Button startGameButton;
        
    }
}
