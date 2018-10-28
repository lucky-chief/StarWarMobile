using UnityEngine;
using Skill;

public class FaceToTarget : SkillAction
{
    private float duration;
    private Vector3 target;

    public FaceToTarget(float duration,Vector3 target)
    {
        this.duration = duration;
        this.target = target;
    }

    public override void OnEnter()
    {
        
    }

    public override void Update(float deltaTime)
    {

    }

}