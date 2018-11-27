using System.Collections.Generic;
using UnityEngine;
using SWEngine;

public class GamePool : ISingleton<GamePool>
{
    private Dictionary<string,List<Object>> m_ObjectMap = new Dictionary<string,List<Object>>();
    private Dictionary<string,Object> m_ModelMap = new Dictionary<string, Object>();

    public void CacheObject(string path,int count)
    {
        Object obj = null;
        if(m_ModelMap.ContainsKey(path))
        {
            obj = m_ModelMap[path];
        }
        else
        {
            obj = Resources.Load(path);//TODO:改成assetbundle形式
            m_ModelMap.Add(path,obj);
        }
        if(obj == null)
        {
            throw new System.Exception("something is wrong!");
        }
        if(!m_ObjectMap.ContainsKey(path))
        {
            m_ObjectMap.Add(path,new List<Object>());
        }
        for(int i = 0; i < count; i++)
        {
            Object o = GameObject.Instantiate(obj,Vector3.zero,Quaternion.identity);
            m_ObjectMap[path].Add(o);
        }
        
    }

    public T GetObjectSync<T>(string path) where T : Object
    {
        if(m_ObjectMap.ContainsKey(path))
        {
            if(m_ObjectMap[path].Count > 0)
            {
                Object obj = m_ObjectMap[path][0];
                m_ObjectMap[path].RemoveAt(0);
                ClearEmpty(path);
                return obj as T;
            }
        }
        CacheObject(path,1);
        return GetObjectSync<T>(path);
    }

    public void Cleanup(string path,bool includeModel = false)
    {
        if(m_ObjectMap.ContainsKey(path))
        {
            List<Object> list = m_ObjectMap[path];
            for(int i = 0; i < list.Count; i++)
            {
                GameObject.DestroyImmediate(list[i]);
            }
            m_ObjectMap.Remove(path);
            if(includeModel)
            {
                m_ModelMap.Remove(path);
            }
            Resources.UnloadUnusedAssets();
        }
    }

    private void ClearEmpty(string path)
    {
        if(m_ObjectMap.ContainsKey(path) && m_ObjectMap[path].Count == 0)
        {
            m_ObjectMap.Remove(path);
        }
    }

}