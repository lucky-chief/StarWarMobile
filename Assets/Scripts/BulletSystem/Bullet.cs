namespace  SWEngine
{
    
//弹道类型
public enum BulletPath
{
    linear,//直线
    parabola,//抛物线
}

//结算类型
public enum SettlementType
{
    collider,//碰撞
    time,//定时
    always,//一直
}
public class Bullet : SWObject
{
    public int bulletId;
    public float speed;
    public BulletPath flyPath;
    public string bornEff;
    public string dieEff;
    public float lifeTime;
    public float[] chgAttrValues;
    public int[] chgAttrTypes;

    public int id
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
}