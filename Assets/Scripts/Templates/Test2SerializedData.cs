/**
----------------------------------------
                星际战争模板表
----------------------------------------
**/
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using System.Reflection;  
            
public class Test2SerializedData : ScriptableObject
{
    public List<Test2Template> dataList = new List<Test2Template>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            Test2Template ht  = (Test2Template)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class Test2Template
{
    public int id;
	public string name;
	public int descId;
}
        