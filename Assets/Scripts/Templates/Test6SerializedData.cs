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
            
public class Test6SerializedData : ScriptableObject
{
    public List<Test6Template> dataList = new List<Test6Template>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            Test6Template ht  = (Test6Template)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class Test6Template
{
    public int id;
	public string name;
	public int descId;
}
        