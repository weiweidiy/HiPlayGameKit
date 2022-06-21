using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILRuntimeHotFixFacade : IHotFixFacade
{
    ILRuntimeMgr _hotFixMgr;



    public ILRuntimeHotFixFacade(ILRuntimeMgr hotFixMgr)
    {
        _hotFixMgr = hotFixMgr;
    }

    public void StartUp(string classFullName, string methodName)
    {
        //_hotFixMgr.appdomain.Invoke("UnityHotFix.Main", "Start", null, null);

        //预先获得IMethod，可以减低每次调用查找方法耗用的时间
        IType type = _hotFixMgr.appdomain.LoadedTypes[classFullName];
        IMethod method = type.GetMethod(methodName, 0);
        var main = ((ILType)type).Instantiate();
        using (var ctx = _hotFixMgr.appdomain.BeginInvoke(method))
        {
            ctx.PushObject(main);
            ctx.Invoke();
            //int id = ctx.ReadInteger();
        }
    }
}