using UnityEngine;

namespace FrameworkDesign.Example {
    public interface IScoreSystem : ISystem { }

    public class ScoreSystem : AbstractSystem, IScoreSystem {
        protected override void OnInit() {
            var gameModel = this.GetModel<IGameModel>();
            this.RegisterEvent<GamePassEvent>(e => {
                
                var countDownSystem = this.GetSystem<ICountDownSystem>();
                var timeScore = countDownSystem.CurrentRemainSeconds * 10;
                gameModel.Score.Value += timeScore;

                if (gameModel.Score.Value > gameModel.BestScore.Value) {
                    gameModel.BestScore.Value = gameModel.Score.Value;
                    Debug.Log("新记录 : " + gameModel.BestScore.Value);
                }
            });

            this.RegisterEvent<MissEvent>(e => {
                gameModel.Score.Value -= 5;
                Debug.Log("得分-5, 当前分数:" + gameModel.Score.Value);
            });

            this.RegisterEvent<KillEnemyEvent>(e => {
                gameModel.Score.Value += 10;
                Debug.Log("得分+10, 当前分数:" + gameModel.Score.Value);
            });
        }
    }
}