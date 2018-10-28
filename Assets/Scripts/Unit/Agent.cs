using UnityEngine;

public class Agent : MonoBehaviour,IObject
{
    private TemplateUnitAttributes attrTpl;
    private GameUnitAttribute attr;
    private Transform2D transform2D;

    public int id
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public virtual void Init(){
        transform2D = new Transform2D(transform);
    }

    public virtual void OnUpdate(float deltaTime){}
    
    void Awake()
    {
        Init();
    }

}