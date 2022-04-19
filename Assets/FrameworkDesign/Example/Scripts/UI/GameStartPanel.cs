using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example {
    public class GameStartPanel : MonoBehaviour {
        public GameObject Enemies;

        private void Start() {
            transform.Find("BtnStart").GetComponent<Button>()
                .onClick.AddListener(() => {
                    gameObject.SetActive(false);
                    
                    GameStartEvent.Trigger();
                });
        }
    }
}