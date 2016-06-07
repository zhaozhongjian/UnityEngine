using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour 
{
    private ResourceManager manager;

    private WaitForSeconds wait = new WaitForSeconds(1);

	void Start () 
    {
        Init();
        StartGame();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    private void Init()
    {
        manager = ResourceManager.GetInstance();
        manager.Init();

        StartCoroutine(UpdataManager());
    }

    IEnumerator UpdataManager()
    {
        while (true)
        {
            manager.Update();
            yield return wait;
        }
    }
    private void StartGame()
    {
        manager.GetResource("prefab/Test",GetPrefabDone);
    }

    private void GetPrefabDone(Object obj)
    {
        
    }

}
