namespace SWEngine
{
    public abstract class SWObject
    {
        public virtual void Init(){}
        public virtual void OnEnable(){}
        public virtual void OnDisable(){}
        public virtual void Dispose(){}
        public virtual void Reset(){}   
    }
}