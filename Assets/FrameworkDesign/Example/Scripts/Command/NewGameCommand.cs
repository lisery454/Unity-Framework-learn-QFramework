namespace FrameworkDesign.Example {
    public class NewGameCommand : AbstractCommand {
        protected override void OnExecute() {
            this.SendEvent<NewGameEvent>();
        }
    }
}