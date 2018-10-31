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
            
public class Test5SerializedData : ScriptableObject
{
    public List<Test5Template> dataList = new List<Test5Template>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            Test5Template ht  = (Test5Template)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class Test5Template
{
    public int id;
	public string name;
	public int descId;
}
        