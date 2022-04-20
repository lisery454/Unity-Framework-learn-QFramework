using UnityEngine;

namespace FrameworkDesign.Example {
    public class UI : MonoBehaviour, IController {
        private void Start() {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
            this.RegisterEvent<GameStartEvent>(OnGameStart);
            this.RegisterEvent<NewGameEvent>(OnNewGame);
            this.RegisterEvent<CountDownEndEvent>(e => {
                transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
                transform.Find("Canvas/GameOverPanel").gameObject.SetActive(true);
            }).UnregisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnNewGame(NewGameEvent e) {
            transform.Find("Canvas/GameStartPanel").gameObject.SetActive(true);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(false);
            transform.Find("Canvas/GameOverPanel").gameObject.SetActive(false);
        }

        private void OnGameStart(GameStartEvent e) {
            transform.Find("Canvas/GamePanel").gameObject.SetActive(true);
            transform.Find("Canvas/GameStartPanel").gameObject.SetActive(false);
        }

        private void OnGamePass(GamePassEvent e) {
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        private void OnDestroy() {
            this.UnregisterEvent<GamePassEvent>(OnGamePass);
            this.UnregisterEvent<GameStartEvent>(OnGameStart);
        }

        public IArchitecture GetArchitecture() {
            return PointGame.Interface;
        }
    }
}