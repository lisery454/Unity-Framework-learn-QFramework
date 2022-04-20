namespace FrameworkDesign.Example {
    public class KillEnemyCommand : AbstractCommand {
        protected override void OnExecute() {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;

            if (UnityEngine.Random.Range(0, 10) < 3) {
                gameModel.Gold.Value += UnityEngine.Random.Range(1, 3);
            }

            this.SendEvent<KillEnemyEvent>();

            if (gameModel.KillCount.Value == 9) {
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}