using UnityEngine;
namespace Skill
{
    public interface  ISkillObject
    {
        string name { get; set; }
        Transform owner{get;set;}
        void OnEnter();
        void Update(float deltaTime);
        void OnExit();
    }
}
