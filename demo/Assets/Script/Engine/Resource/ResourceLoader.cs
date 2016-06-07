using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceLoader 
{
    private class ResourceItem
    {
        public int retainCount;
        public Object obj;
    }

    private Dictionary<string, ResourceItem> assetDict;

    public void Init()
    {
        assetDict = new Dictionary<string, ResourceItem>();
    }
    public void Disable()
    {
 
    }
    public Object GetResource(string path)
    {
        ResourceItem item;
        if (!assetDict.TryGetValue(path, out item))
        {
            Object asset = Resources.Load(path);
            item = new ResourceItem();
            item.obj = asset;
            item.retainCount = 0;
        }
        Object obj = GameObject.Instantiate(item.obj);
        item.retainCount++;
        return obj;
    }
    public void DeleteResource(string path)
    {
        ResourceItem item;
        if (assetDict.TryGetValue(path, out item))
        {
            item.retainCount--;
            if (item.retainCount <= 0)
            {
                Resources.UnloadAsset(item.obj);
            }
            assetDict.Remove(path);
        }
    }
}
