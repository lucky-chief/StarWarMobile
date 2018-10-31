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
            
public class Test4SerializedData : ScriptableObject
{
    public List<Test4Template> dataList = new List<Test4Template>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            Test4Template ht  = (Test4Template)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class Test4Template
{
    public int id;
	public string name;
	public int descId;
}
        