﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luancher : MonoBehaviour
{
    public IHotFixManager ILManager;
    ILRuntimeHotFixFacade hotFixFacade;

    // Start is called before the first frame update
    async void Start()
    {
        ILManager = new ILRuntimeMgr();
        await ILManager.LoadAssemblyAsync("HotFixGameKit");
        await ILManager.LoadAssemblyAsync("HotFixModuleTest");

        hotFixFacade = new ILRuntimeHotFixFacade(ILManager);
        hotFixFacade.StartUp();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
