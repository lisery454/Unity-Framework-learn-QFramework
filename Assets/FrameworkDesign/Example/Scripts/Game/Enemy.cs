using UnityEngine;

namespace FrameworkDesign.Example {
    public class Enemy : MonoBehaviour, IController {
        private void OnMouseDown() {
            Destroy(gameObject);
            this.SendCommand<KillEnemyCommand>();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture() {
            return PointGame.Interface;
        }
    }
}