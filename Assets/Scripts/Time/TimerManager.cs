using UnityEngine;
using System.Collections.Generic;
using SWEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    public int frameCount{get;private set;}
    public Dictionary<long,SWObject> updatableMap = new Dictionary<long, SWObject>();
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
public void RegisterFixedUpdatable(SWObject updater,UpdaterGroup group = UpdaterGroup.AllGroup)
{

}

#endregion
}