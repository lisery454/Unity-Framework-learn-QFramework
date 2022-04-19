namespace FrameworkDesign.Example {
    public class KillEnemyCommand : AbstractCommand {
        protected override void OnExecute() {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;

            if (gameModel.KillCount.Value == 9) {
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}