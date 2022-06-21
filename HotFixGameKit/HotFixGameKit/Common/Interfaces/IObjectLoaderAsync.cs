namespace HotFixGameKit
{
    public interface IObjectLoaderAsync
    {
        IAsyncHandle LoadObjectAsync(string assetName);

        IAsyncHandle LoadObjectAsync<T>(string assetName);

    }
}
