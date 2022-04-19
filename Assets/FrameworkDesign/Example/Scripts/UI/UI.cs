using UnityEngine;

namespace FrameworkDesign.Example {
    public class UI : MonoBehaviour, IController {
        private void Start() {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
        }

        private void OnGamePass(GamePassEvent e) {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        private void OnDestroy() {
            this.UnregisterEvent<GamePassEvent>(OnGamePass);
        }

        public IArchitecture GetArchitecture() {
            return PointGame.Interface;
        }
    }
}