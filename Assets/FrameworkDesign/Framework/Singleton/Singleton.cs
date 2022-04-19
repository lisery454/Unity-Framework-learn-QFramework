using System;
using System.Reflection;


namespace FrameworkDesign {
    public class Singleton<T> where T : Singleton<T> {
        private static T mInstance;

        public static T Instance {
            get {
                if (mInstance == null) {
                    var type = typeof(T);
                    var cotrs = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    var cotr = Array.Find(cotrs, c => c.GetParameters().Length == 0);

                    if (cotr == null) {
                        throw new Exception("Non Public Constructor not Found in " + type.Name);
                    }

                    mInstance = cotr.Invoke(null) as T;
                }
                
                return mInstance;
            }
        }
    }
}