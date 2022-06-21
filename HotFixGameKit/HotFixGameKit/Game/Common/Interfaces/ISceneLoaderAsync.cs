using UnityEngine.SceneManagement;

namespace HotFixGameKit.Game
{

    public interface ISceneLoaderAsync 
    {
        IAsyncHandle LoadSceneAsync(string key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);

        IAsyncHandle UnLoadSceneAsync(IAsyncHandle handle);
    }
}
