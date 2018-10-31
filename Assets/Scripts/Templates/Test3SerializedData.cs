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
            
public class Test3SerializedData : ScriptableObject
{
    public List<Test3Template> dataList = new List<Test3Template>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            Test3Template ht  = (Test3Template)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class Test3Template
{
    public int id;
	public string name;
	public int descId;
}
        