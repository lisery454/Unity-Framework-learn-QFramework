using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example {
    public class GameStartPanel : MonoBehaviour, IController {
        public GameObject Enemies;

        private void Start() {
            transform.Find("BtnStart").GetComponent<Button>()
                .onClick.AddListener(() => {
                    gameObject.SetActive(false);
                    this.SendCommand<StartGameCommand>();
                });
        }

        IArchitecture IBelongToArchitecture.GetArchitecture() {
            return PointGame.Interface;
        }
    }
}