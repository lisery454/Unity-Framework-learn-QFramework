using System;

namespace FrameworkDesign {
    public class BindableProperty<T> where T : IEquatable<T> {
        private T mValue;

        public T Value {
            get => mValue;

            set {
                if (!value.Equals(mValue)) {
                    mValue = value;

                    mOnValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> mOnValueChanged;

        public IUnregister RegisterOnValueChanged(Action<T> onValueChanged) {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnregister<T>() {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnregisterOnValueChanged(Action<T> onValueChanged) {
            mOnValueChanged -= onValueChanged;
        }
    }

    public class BindablePropertyUnregister<T> : IUnregister where T : IEquatable<T> {
        public BindableProperty<T> BindableProperty { get; set; }
        public Action<T> OnValueChanged { get; set; }

        public void Unregister() {
            BindableProperty.UnregisterOnValueChanged(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }
}