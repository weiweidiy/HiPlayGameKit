namespace HotFixGameKit
{
    public interface IReflectionInstantiator
    {
        object Instantiate(string classFullName, object[] parameters = null);
    }
}
