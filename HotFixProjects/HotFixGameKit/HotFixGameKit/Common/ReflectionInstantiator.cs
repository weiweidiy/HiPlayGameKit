using System;

namespace HotFixGameKit
{
    /// <summary>
    /// 反射实例化
    /// </summary>
    public class ReflectionInstantiator : IReflectionInstantiator
    {
        public object Instantiate(string classFullName, object[] parameters = null)
        {

            return CreateObject(classFullName, parameters);
        }

        object CreateObject(string classFullName, object[] parameters = null)
        {

            Type type = Type.GetType(classFullName);

            object obj = null;

            if (parameters == null)
            {
                obj = Activator.CreateInstance(type);
            }
            else
            {
                obj = Activator.CreateInstance(type, parameters);
            }

            return obj;
        }
    }
}
