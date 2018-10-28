using UnityEngine;
using System.Collections.Generic;

public class TimerManager : MonoSingleton<TimerManager>
{
    public int frameCount{get;private set;}
    public Dictionary<long,IObject> updatableMap = new Dictionary<long, IObject>();
#region 生命周期函数
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        
    }
#endregion
#region 共有方法
public void RegisterFixedUpdatable(IObject updater,UpdaterGroup group = UpdaterGroup.AllGroup)
{

}

#endregion
}