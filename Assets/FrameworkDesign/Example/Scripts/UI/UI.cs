using UnityEngine;

namespace FrameworkDesign.Example {
    public class UI : MonoBehaviour {
        private void Start() {
            GamePassEvent.Register(OnGamePass);
        }

        private void OnGamePass() {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        private void OnDestroy() {
            GamePassEvent.Unregister(OnGamePass);
        }
    }
}