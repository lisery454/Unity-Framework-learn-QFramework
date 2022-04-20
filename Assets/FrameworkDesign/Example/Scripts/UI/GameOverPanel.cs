using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example {
    public class GameOverPanel : MonoBehaviour, IController {
        private void Start() {
            transform.Find("BtnBack").GetComponent<Button>()
                .onClick.AddListener(() => {
                    gameObject.SetActive(false);
                    this.SendCommand<NewGameCommand>();
                });
        }

        public IArchitecture GetArchitecture() {
            return PointGame.Interface;
        }
    }
}