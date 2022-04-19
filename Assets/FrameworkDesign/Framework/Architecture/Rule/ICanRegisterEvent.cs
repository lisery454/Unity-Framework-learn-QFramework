using System;

namespace FrameworkDesign {
    public interface ICanRegisterEvent : IBelongToArchitecture { }

    public static class CanRegisterEventExtension {
        public static IUnregister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) {
            return self.GetArchitecture().Register(onEvent);
        }

        public static void UnregisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) {
            self.GetArchitecture().Unregister(onEvent);
        }
    }
}