using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ResourceCacheItem
{
    public Object obj;
    public int retainTime;
}

public class ResourceCache 
{
    private ResourceLoader loader;
    private Dictionary<string, List<ResourceCacheItem>> cacheDict;

    public void Init()
    {
        cacheDict = new Dictionary<string, List<ResourceCacheItem>>();
        loader = new ResourceLoader();
        loader.Init();
    }
    public void Disable()
    {
 
    }
    public Object GetResource(string path)
    {
        List<ResourceCacheItem> objectList;
        if (cacheDict.TryGetValue(path, out objectList))
        {
            if (objectList.Count > 0)
            {
                ResourceCacheItem obj = objectList[objectList.Count - 1];
                objectList.RemoveAt(objectList.Count - 1);
                return obj.obj;
            }
        }
        return loader.GetResource(path);
    }
    public void DeleteResource(Object obj,string path)
    {
        ResourceCacheItem item = new ResourceCacheItem();
        item.obj = obj;
        item.retainTime = 2;
        List<ResourceCacheItem> objectList;
        if (cacheDict.TryGetValue(path, out objectList))
        {
            objectList.Add(item);
        }
        else
        {
            objectList = new List<ResourceCacheItem>();
            objectList.Add(item);
            cacheDict.Add(path, objectList);
        }
    }

    public void Update()
    {
        List<string> delList = new List<string>();
        foreach (KeyValuePair<string, List<ResourceCacheItem>> kv in cacheDict)
        {
            for (int i = kv.Value.Count; i >=0; i--)
            {
                ResourceCacheItem item = kv.Value[i];
                item.retainTime--;
                if (item.retainTime <= 0)
                {
                    kv.Value.RemoveAt(i);
                    loader.DeleteResource(kv.Key);
                    if (kv.Value.Count == 0)
                    {
                        delList.Add(kv.Key);
                    }
                }
            }
        }
        for (int i = 0; i < delList.Count; i++)
        {
            cacheDict.Remove(delList[i]);
        }
    }
}
