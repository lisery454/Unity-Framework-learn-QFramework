using UnityEngine;

namespace FrameworkDesign.Example {
    public class Game : MonoBehaviour, IController {
        private void Awake() {
            this.RegisterEvent<GameStartEvent>(OnGameStart);

            this.RegisterEvent<CountDownEndEvent>(e => { transform.Find("Enemies").gameObject.SetActive(false); })
                .UnregisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<GamePassEvent>(e => { transform.Find("Enemies").gameObject.SetActive(false); })
                .UnregisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGameStart(GameStartEvent e) {
            var enemyRoot = transform.Find("Enemies");
            enemyRoot.gameObject.SetActive(true);

            foreach (Transform childTrans in enemyRoot) {
                childTrans.gameObject.SetActive(true);
            }
        }

        private void OnDestroy() {
            this.UnregisterEvent<GameStartEvent>(OnGameStart);
        }

        public IArchitecture GetArchitecture() {
            return PointGame.Interface;
        }
    }
}