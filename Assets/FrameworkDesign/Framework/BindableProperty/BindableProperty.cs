using System;

namespace FrameworkDesign {
    public class BindableProperty<T> where T : IEquatable<T> {
        private T mValue;

        public T Value {
            get => mValue;

            set {
                if (!value.Equals(mValue)) {
                    mValue = value;

                    OnValueChanged?.Invoke(value);
                }
            }
        }

        public Action<T> OnValueChanged;
    }
}