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
            
public class TestSerializedData : ScriptableObject
{
    public List<TestTemplate> dataList = new List<TestTemplate>();
    
#if UNITY_EDITOR
     public void Put(List<System.Object> data)
     {
        for(int j = 0; j < data.Count; j++)
        {
            TestTemplate ht  = (TestTemplate)data[j];
            dataList.Add(ht);
        }
    }
#endif
}
            
[System.Serializable]
public class TestTemplate
{
    public int id;
	public string name;
	public int descId;
}
        