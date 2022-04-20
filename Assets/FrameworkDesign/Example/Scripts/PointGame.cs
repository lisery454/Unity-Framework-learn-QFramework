namespace FrameworkDesign.Example {
    public class PointGame : Architecture<PointGame> {
        protected override void Init() {
            RegisterSyStem<ICountDownSystem>(new CountDownSystem());
            RegisterSyStem<IAchievementSystem>(new AchievementSystem());
            RegisterSyStem<IScoreSystem>(new ScoreSystem());
            
            RegisterModel<IGameModel>(new GameModel());
            
            RegisterUtility<IStorage>(new PlayerPrefStorage());
        }
    }
}