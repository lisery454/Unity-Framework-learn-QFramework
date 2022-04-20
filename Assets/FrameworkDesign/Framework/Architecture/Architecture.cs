using System;
using System.Collections.Generic;

namespace FrameworkDesign {
    public interface IArchitecture {
        T GetUtility<T>() where T : class, IUtility;

        T GetModel<T>() where T : class, IModel;

        T GetSystem<T>() where T : class, ISystem;

        void RegisterModel<T>(T model) where T : IModel;

        void RegisterUtility<T>(T utility) where T : IUtility;

        void RegisterSyStem<T>(T system) where T : ISystem;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        void SendEvent<T>() where T : new();

        void SendEvent<T>(T e);

        IUnregister Register<T>(Action<T> onEvent);

        void Unregister<T>(Action<T> onEvent);
    }

    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new() {
        private static T mArchitecture;

        private readonly IOCContainer mContainer = new IOCContainer();

        private bool mInited;

        private readonly List<IModel> mModels = new List<IModel>();
        private readonly List<ISystem> mSystems = new List<ISystem>();

        public static Action<T> OnRegisterPatch = architecture => { };

        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();

        public static IArchitecture Interface {
            get {
                if (mArchitecture == null) MakeSureArchitecture();
                return mArchitecture;
            }
        }

        private static void MakeSureArchitecture() {
            if (mArchitecture == null) {
                mArchitecture = new T();
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                foreach (var model in mArchitecture.mModels) {
                    model.Init();
                }

                mArchitecture.mModels.Clear();

                foreach (var system in mArchitecture.mSystems) {
                    system.Init();
                }

                mArchitecture.mSystems.Clear();

                mArchitecture.mInited = true;
            }
        }

        //用来给子类写注册方法
        protected abstract void Init();

        public void RegisterModel<T0>(T0 model) where T0 : IModel {
            model.SetArchitecture(mArchitecture);
            mContainer.Register(model);

            if (!mInited)
                mModels.Add(model);
            else
                model.Init();
        }

        public void RegisterUtility<T0>(T0 utility) where T0 : IUtility {
            mContainer.Register(utility);
        }

        public void RegisterSyStem<T0>(T0 system) where T0 : ISystem {
            system.SetArchitecture(mArchitecture);
            mContainer.Register(system);

            if (!mInited)
                mSystems.Add(system);
            else
                system.Init();
        }

        public T0 GetModel<T0>() where T0 : class, IModel {
            return mContainer.Get<T0>();
        }

        public T0 GetUtility<T0>() where T0 : class, IUtility {
            return mContainer.Get<T0>();
        }

        public T0 GetSystem<T0>() where T0 : class, ISystem {
            return mContainer.Get<T0>();
        }

        public void SendCommand<T0>() where T0 : ICommand, new() {
            var command = new T0();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<T0>(T0 command) where T0 : ICommand {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendEvent<T0>() where T0 : new() {
            mTypeEventSystem.Send<T0>();
        }

        public void SendEvent<T0>(T0 e) {
            mTypeEventSystem.Send(e);
        }

        public IUnregister Register<T0>(Action<T0> onEvent) {
            return mTypeEventSystem.Register(onEvent);
        }

        public void Unregister<T0>(Action<T0> onEvent) {
            mTypeEventSystem.Unregister(onEvent);
        }
    }
}