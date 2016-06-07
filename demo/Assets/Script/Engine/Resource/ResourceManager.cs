using UnityEngine;
using System.Collections;

public class ResourceManager : Manager
{
    private static ResourceManager instance;
    public static ResourceManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ResourceManager();
        }
        return instance;
    }

    public delegate void GetAssetCallBack(Object obj);

    private string documentPath;
    private string packagetPath;
    private ResourceCache cache;

    public override void Init()
    {
        cache = new ResourceCache();
        cache.Init();
    }

    public override void Disable()
    {
        
    }
    public override void Update()
    {
        cache.Update();
    }

    public void GetResource(string path, GetAssetCallBack callBack)
    {
        Object obj = cache.GetResource(path);
        callBack(obj);
    }
    public void DeleteResource(Object obj,string path)
    {
        cache.DeleteResource(obj, path);           
    }
}
