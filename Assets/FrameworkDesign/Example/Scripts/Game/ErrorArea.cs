using UnityEngine;

namespace FrameworkDesign.Example {
    public class ErrorArea : MonoBehaviour, IController {
        private void OnMouseDown() {
            this.SendCommand<MissCommand>();
        }

        public IArchitecture GetArchitecture() {
            return PointGame.Interface;
        }
    }
}