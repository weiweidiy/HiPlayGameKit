using HotFixGameKit;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
//using ILRuntime.Runtime.Generated;
//using UnityEngine.ResourceManagement;




public class ILRuntimeMgr : IHotFixManager
{
    public AppDomain appdomain;

    bool isDebug = true;

    public ILRuntimeMgr()
    {
        appdomain = new AppDomain();

        InitializeILRuntime();

        //注册适配器
        RegisterAdaptor();
    }

    /// <summary>
    /// 读取字节文本文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    async Task<byte[]> GetBytes(string path)
    {
        TextAsset asset = await Addressables.LoadAssetAsync<TextAsset>(path).Task;
        return asset.bytes;
    }

    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public async Task LoadAssembly(string address)
    {
        var dll = await GetBytes(address);
        byte[] pdb = null;
        if (isDebug)
        {
            string pdbpath = address + "_pdb";
            pdb = await GetBytes(pdbpath);
        }

        MemoryStream fs = new MemoryStream(dll);
        {
            if (pdb != null)
            {
                MemoryStream pdbStream = new MemoryStream(pdb);
                appdomain.LoadAssembly(fs, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            else
            {
                appdomain.LoadAssembly(fs);
            }
        }
    }

    /// <summary>
    /// 获取已经加载的类
    /// </summary>
    /// <param name="fullClassName"></param>
    /// <returns></returns>
    public IType GetLoadedType(string fullClassName)
    {
        return appdomain.LoadedTypes[fullClassName];
    }

    /// <summary>
    /// 调用方法
    /// </summary>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public InvocationContext BeginInvoke(IMethod methodName)
    {
        return appdomain.BeginInvoke(methodName);
    }

    //public async Task InitializeAsync()
    //{
    //    await LoadAssembly(DefaultDllAdress);



    //    //dll = await GetBytes(DefaultDllAdress);

    //    //if (isDebug)
    //    //{
    //    //    string pdbpath = DefaultDllAdress + "_pdb";
    //    //    pdb = await GetBytes(pdbpath);
    //    //    appdomain.DebugService.StartDebugService(56000);
    //    //}



    //    //MemoryStream fs = new MemoryStream(dll);
    //    //{
    //    //    if (pdb != null)
    //    //    {
    //    //        MemoryStream pdbStream = new MemoryStream(pdb);
    //    //        appdomain.LoadAssembly(fs, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
    //    //    }
    //    //    else
    //    //    {
    //    //        appdomain.LoadAssembly(fs);
    //    //    }
    //    //}




    //    //else
    //    //{
    //    //    //ILR DLL 直接加载
    //    //    string dllpath = HotFixName + ".dll";
    //    //    UnityWebRequest dllrequest = UnityWebRequest.Get(dllpath);
    //    //    yield return dllrequest.SendWebRequest();
    //    //    if (!string.IsNullOrEmpty(dllrequest.error))
    //    //        UnityEngine.Debug.LogError(dllrequest.error + " URL:" + dllpath);
    //    //    byte[] dllfileByte = dllrequest.downloadHandler.data;
    //    //    dllrequest.Dispose();
    //    //    dll = dllfileByte;
    //    //    if (isDebug)
    //    //    {
    //    //        string pdbpath = HotFixName + ".pdb";
    //    //        UnityWebRequest pdbrequest = UnityWebRequest.Get(pdbpath);
    //    //        yield return pdbrequest.SendWebRequest();
    //    //        if (!string.IsNullOrEmpty(pdbrequest.error))
    //    //            UnityEngine.Debug.LogError(pdbrequest.error + " URL:" + pdbpath);
    //    //        byte[] pdbfileByte = pdbrequest.downloadHandler.data;
    //    //        pdbrequest.Dispose();
    //    //        pdb = pdbfileByte;
    //    //    }
    //    //}

    //}

    unsafe void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的

        var mi = typeof(Debug).GetMethod("Log", new System.Type[] { typeof(object) });
        //appdomain.RegisterCLRMethodRedirection(mi, Log_11);

        //SetupCLRRedirection();
        //SetupCLRRedirection2();

        //CLRBinding();
    }



    void RegisterAdaptor()
    {
        appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdapter());
        appdomain.RegisterCrossBindingAdaptor(new EventArgsAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new SimpleCommandAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new MediatorAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new ProxyAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new IDisposableAdapter());

        //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.GameObject>>();
        //appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        //{
        //    return new UnityEngine.Events.UnityAction(() =>
        //    {
        //        ((Action)act)();
        //    });
        //});
        appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();
        appdomain.DelegateManager.RegisterMethodDelegate<System.Object, System.EventArgs>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler>((act) =>
        {
            return new System.EventHandler((sender, e) =>
            {
                ((Action<System.Object, System.EventArgs>)act)(sender, e);
            });
        });

        appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance>();

        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((Action)act)();
            });
        });


        //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.AsyncOperation>();
        //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();
        //appdomain.DelegateManager.RegisterMethodDelegate<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();

        //appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>>((act) =>
        //{
        //    return new System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>((sender, e) =>
        //    {
        //        ((Action<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>)act)(sender, e);
        //    });
        //});

        //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>();
        //appdomain.DelegateManager.RegisterMethodDelegate<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.GameObject>>();

        //appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.GameObject>>>((act) =>
        //{
        //    return new System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.GameObject>>((sender, e) =>
        //    {
        //        ((Action<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.GameObject>>)act)(sender, e);
        //    });
        //});

        //appdomain.DelegateManager.RegisterMethodDelegate<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>();
        //appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>>((act) =>
        //{
        //    return new System.EventHandler<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>((sender, e) =>
        //    {
        //        ((Action<System.Object, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>)act)(sender, e);
        //    });
        //});





    }

    void CLRBinding()
    {
        //CLRBindings.Initialize(appdomain);
    }


    /// <summary>
    /// Debug 重定向
    /// </summary>
    /// <param name="__intp"></param>
    /// <param name="__esp"></param>
    /// <param name="__mStack"></param>
    /// <param name="__method"></param>
    /// <param name="isNewObj"></param>
    /// <returns></returns>
    unsafe StackObject* Log_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        //ILRuntime的调用约定为被调用者清理堆栈，因此执行这个函数后需要将参数从堆栈清理干净，并把返回值放在栈顶，具体请看ILRuntime实现原理文档
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        //这个是最后方法返回后esp栈指针的值，应该返回清理完参数并指向返回值，这里是只需要返回清理完参数的值即可
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);
        //取Log方法的参数，如果有两个参数的话，第一个参数是esp - 2,第二个参数是esp -1, 因为Mono的bug，直接-2值会错误，所以要调用ILIntepreter.Minus
        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

        //这里是将栈指针上的值转换成object，如果是基础类型可直接通过ptr->Value和ptr->ValueLow访问到值，具体请看ILRuntime实现原理文档
        object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
        //所有非基础类型都得调用Free来释放托管堆栈
        __intp.Free(ptr_of_this_method);

        //在真实调用Debug.Log前，我们先获取DLL内的堆栈
        var stacktrace = __domain.DebugService.GetStackTrace(__intp);


        //我们在输出信息后面加上DLL堆栈
        UnityEngine.Debug.Log(message + "\n" + stacktrace);

        return __ret;
    }







    //unsafe void SetupCLRRedirection()
    //{
    //    //这里面的通常应该写在InitializeILRuntime，这里为了演示写这里
    //    //var arr = typeof(DiContainer).GetMethods();
    //    //foreach (var i in arr)
    //    //{
    //    //    if (i.Name == "Bind" && i.GetGenericArguments().Length == 1)
    //    //    {
    //    //        appdomain.RegisterCLRMethodRedirection(i, ContainerBind);
    //    //    }
    //    //}

    //    var mi = typeof(DiContainer).GetMethod("Bind", new System.Type[] { typeof(Type[]) });
    //    appdomain.RegisterCLRMethodRedirection(mi, ContainerBind);
    //}

    //unsafe StackObject* ContainerBind(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    //{
    //    Debug.Log("ContainerBind");
    //    //CLR重定向的说明请看相关文档和教程，这里不多做解释
    //    ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

    //    var ptr = ILIntepreter.Minus(__esp, 2); //__esp - 1;
    //    //成员方法的第一个参数为this
    //    DiContainer instance = StackObject.ToObject(ptr, __domain, __mStack) as DiContainer;


    //    StackObject* ptr_of_this_method;
    //    //这个是最后方法返回后esp栈指针的值，应该返回清理完参数并指向返回值，这里是只需要返回清理完参数的值即可
    //    StackObject* __ret = ILIntepreter.Minus(__esp, 1);
    //    //取Log方法的参数，如果有两个参数的话，第一个参数是esp - 2,第二个参数是esp -1, 因为Mono的bug，直接-2值会错误，所以要调用ILIntepreter.Minus
    //    ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

    //    //这里是将栈指针上的值转换成object，如果是基础类型可直接通过ptr->Value和ptr->ValueLow访问到值，具体请看ILRuntime实现原理文档
    //    var so = StackObject.ToObject(ptr_of_this_method, __domain, __mStack);
    //    Type[] args = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack)) as Type[];

    //    if (instance == null)
    //        throw new System.NullReferenceException();
    //    __intp.Free(ptr);

    //    object res = null;
    //    //List<Type> types = new List<Type>();
    //    //foreach (var arg in args)
    //    //{
    //    //    ILTypeInstance ins = appdomain.Instantiate(arg.FullName);
    //    //    var obj = ins.CLRInstance;
    //    //    IType type = appdomain.LoadedTypes[arg.FullName];
    //    //    Type t = type.TypeForCLR;
    //    //    types.Add(obj.GetType());
    //    //}
    //    res = instance.Bind(args);

    //    return ILIntepreter.PushObject(ptr, __mStack, res);


    //    //var genericArgument = __method.GenericArguments;
    //    ////AddComponent应该有且只有1个泛型参数
    //    //if (genericArgument != null && genericArgument.Length == 1)
    //    //{
    //    //    var type = genericArgument[0];
    //    //    object res;
    //    //    if (type is CLRType)
    //    //    {
    //    //        //Unity主工程的类不需要任何特殊处理，直接调用Unity接口
    //    //        instance.Bind(type.TypeForCLR);
    //    //    }
    //    //    else
    //    //    {
    //    //        //热更DLL内的类型比较麻烦。首先我们得自己手动创建实例
    //    //        var ilInstance = new ILTypeInstance(type as ILType, false);//手动创建实例是因为默认方式会new MonoBehaviour，这在Unity里不允许


    //    //        Debug.Log("类型全名：" + type.FullName);

    //    //        IType iType = appdomain.LoadedTypes[type.FullName];
    //    //        Type t = iType.ReflectionType;

    //    //        instance.Bind(t);
    //    //    }
    //    //}

    //    //return __esp;
    //}

    //unsafe void SetupCLRRedirection2()
    //{
    //    //这里面的通常应该写在InitializeILRuntime，这里为了演示写这里
    //    //var arr = typeof(DiContainer).GetMethods();
    //    //foreach (var i in arr)
    //    //{
    //    //    if (i.Name == "Resolve" && i.GetGenericArguments().Length == 1)
    //    //    {
    //    //        appdomain.RegisterCLRMethodRedirection(i, ContainerResolve);
    //    //    }
    //    //}

    //    var mi = typeof(DiContainer).GetMethod("Resolve", new System.Type[] { typeof(Type) });
    //    appdomain.RegisterCLRMethodRedirection(mi, ContainerResolve);
    //}

    //unsafe StackObject* ContainerResolve(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    //{
    //    Debug.Log("ContainerResolve");
    //    //CLR重定向的说明请看相关文档和教程，这里不多做解释
    //    ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

    //    var ptr = ILIntepreter.Minus(__esp, 2); //__esp - 1;
    //    //成员方法的第一个参数为this
    //    DiContainer instance = StackObject.ToObject(ptr, __domain, __mStack) as DiContainer;


    //    StackObject* ptr_of_this_method;
    //    //这个是最后方法返回后esp栈指针的值，应该返回清理完参数并指向返回值，这里是只需要返回清理完参数的值即可
    //    StackObject* __ret = ILIntepreter.Minus(__esp, 1);
    //    //取Log方法的参数，如果有两个参数的话，第一个参数是esp - 2,第二个参数是esp -1, 因为Mono的bug，直接-2值会错误，所以要调用ILIntepreter.Minus
    //    ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

    //    //这里是将栈指针上的值转换成object，如果是基础类型可直接通过ptr->Value和ptr->ValueLow访问到值，具体请看ILRuntime实现原理文档
    //    Type arg = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack)) as Type;

    //    if (instance == null)
    //        throw new System.NullReferenceException();
    //    __intp.Free(ptr);

    //    object res = null;
    //    //foreach (var arg in args)
    //    //{
    //    //IType type = appdomain.LoadedTypes[arg.FullName];
    //    //Type t = type.ReflectionType;

    //    //}
    //    //ILTypeInstance ins = appdomain.Instantiate(arg.FullName);
    //    //var obj = ins.CLRInstance;

    //    //IType type = appdomain.LoadedTypes[arg.FullName];
    //    //Type t = type.TypeForCLR;
    //    res = instance.Resolve(arg);
    //    //res = instance.Resolve(typeof(ManiTest));
    //    return ILIntepreter.PushObject(ptr, __mStack, res);


    //    ////CLR重定向的说明请看相关文档和教程，这里不多做解释
    //    //ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

    //    //var ptr = __esp - 1;
    //    ////成员方法的第一个参数为this
    //    //DiContainer instance = StackObject.ToObject(ptr, __domain, __mStack) as DiContainer;
    //    //if (instance == null)
    //    //    throw new System.NullReferenceException();
    //    //__intp.Free(ptr);

    //    //var genericArgument = __method.GenericArguments;
    //    ////AddComponent应该有且只有1个泛型参数
    //    //if (genericArgument != null && genericArgument.Length == 1)
    //    //{
    //    //    var type = genericArgument[0];
    //    //    object res = null;
    //    //    if (type is CLRType)
    //    //    {
    //    //        //Unity主工程的类不需要任何特殊处理，直接调用Unity接口
    //    //        res = instance.Resolve(type.TypeForCLR);
    //    //    }
    //    //    else
    //    //    {
    //    //        //因为所有DLL里面的MonoBehaviour实际都是这个Component，所以我们只能全取出来遍历查找
    //    //        Debug.Log("resolve类型全名：" + type.FullName);

    //    //        IType iType = appdomain.LoadedTypes[type.FullName];
    //    //        Type t = iType.ReflectionType;

    //    //        res = instance.Resolve(t);

    //    //    }

    //    //    return ILIntepreter.PushObject(ptr, __mStack, res);
    //    //}

    //    //return __esp;
    //}
}
