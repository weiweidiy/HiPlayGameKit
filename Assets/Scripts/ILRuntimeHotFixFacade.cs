using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILRuntimeHotFixFacade : IHotFixFacade
{
    IHotFixManager _hotFixMgr;

    const string classFullName = "HotFixGameKit.Main";
    const string methodName = "Startup";

    public ILRuntimeHotFixFacade(IHotFixManager hotFixMgr)
    {
        _hotFixMgr = hotFixMgr;
    }

    public void StartUp()
    {
        //_hotFixMgr.appdomain.Invoke("UnityHotFix.Main", "Start", null, null);

        //预先获得IMethod，可以减低每次调用查找方法耗用的时间
        //IType type = _hotFixMgr.appdomain.LoadedTypes[classFullName];
        IType type = _hotFixMgr.GetLoadedType(classFullName);
        IMethod method = type.GetMethod(methodName, 0);
        var main = ((ILType)type).Instantiate();
        using (var ctx = _hotFixMgr.BeginInvoke(method))
        {
            ctx.PushObject(main);
            ctx.Invoke();
            //int id = ctx.ReadInteger();
        }
    }
}