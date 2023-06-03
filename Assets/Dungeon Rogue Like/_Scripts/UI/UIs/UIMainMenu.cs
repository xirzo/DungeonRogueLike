using Dungeon.Logic.Utilities;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Dungeon.UI
{
    [RequireComponent(typeof(GameExitor))]
    public class UIMainMenu : UIElement
    {
        private GameExitor _exitor;

        private Button _playButton;
        private Button _settingsButton;
        private Button _exitButton;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _exitor);

            _playButton = Root.Q<Button>("PlayButton");
            _settingsButton = Root.Q<Button>("SettingsButton");
            _exitButton = Root.Q<Button>("ExitButton");


            _playButton.clicked += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            _exitButton.clicked += () => _exitor.Exit();
        }

        private void OnDestroy()
        {
            _playButton.clicked -= () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            _exitButton.clicked -= () => _exitor.Exit();
        }
    }
}
