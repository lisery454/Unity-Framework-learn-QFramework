namespace FrameworkDesign {
    public interface IController : ICanSendCommand, ICanGetSystem, ICanGetModel, ICanRegisterEvent { }
}