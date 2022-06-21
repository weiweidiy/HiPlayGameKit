using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luancher : MonoBehaviour
{

    //AddressableDownLoadMgr _downloadMgr = new AddressableDownLoadMgr();

    ILRuntimeMgr hotFixMgr;

    ILRuntimeHotFixFacade hotFixFacade;


    const string _scriptsAddress = "HotFixGameKit";
    // Start is called before the first frame update
    async void Start()
    {
        hotFixMgr = new ILRuntimeMgr(_scriptsAddress);
        hotFixFacade = new ILRuntimeHotFixFacade(hotFixMgr);

        await hotFixMgr.InitializeAsync();
        hotFixFacade.StartUp("HotFixGameKit.Main", "Startup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
