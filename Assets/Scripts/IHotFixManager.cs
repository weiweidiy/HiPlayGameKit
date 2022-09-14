using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHotFixManager
{
    Task LoadAssemblyAsync(string address);

    void LoadAssembly(string address);

    IType GetLoadedType(string fullClassName);

    InvocationContext BeginInvoke(IMethod methodName);

}
