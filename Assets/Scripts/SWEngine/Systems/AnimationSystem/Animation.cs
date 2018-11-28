using System.Collections;
using System.Collections.Generic;
using DragonBones;

namespace SWEngine
{
	public enum  SoldierAnimationState
	{
		WalkUp,WalkDown,WalkLeft,WalkRight,
		WalkBLU,WalkBRU,WalkBLD,WalkBRD,
		AttackUp,AttackDown,AttackLeft,AttackRight,
		AttackBLU,AttackBRU,AttackBLD,AttackBRD,
		Die1,Die2,Die3
	}

	public struct AnimationState
	{
		public string animationName;
		public bool flipX;
	}

	public class Animation : SWObject{
		private UnityArmatureComponent _dbComp;
		private Dictionary<SoldierAnimationState,AnimationState> animations =
		new Dictionary<SoldierAnimationState, AnimationState>(){
			{SoldierAnimationState.WalkUp,new AnimationState{animationName = "walk1",flipX = false}},
			{SoldierAnimationState.WalkDown,new AnimationState{animationName = "walk2",flipX = false}},
			{SoldierAnimationState.WalkLeft,new AnimationState{animationName = "walk4",flipX = false}},
			{SoldierAnimationState.WalkRight,new AnimationState{animationName = "walk4",flipX = true}},
			{SoldierAnimationState.WalkBLU,new AnimationState{animationName = "walk3",flipX = false}},
			{SoldierAnimationState.WalkBRU,new AnimationState{animationName = "walk3",flipX = true}},
			{SoldierAnimationState.WalkBLD,new AnimationState{animationName = "walk5",flipX = false}},
			{SoldierAnimationState.WalkBRD,new AnimationState{animationName = "walk5",flipX = true}},

			{SoldierAnimationState.AttackUp,new AnimationState{animationName = "shoot1",flipX = false}},
			{SoldierAnimationState.AttackDown,new AnimationState{animationName = "shoot2",flipX = false}},
			{SoldierAnimationState.AttackLeft,new AnimationState{animationName = "shoot4",flipX = false}},
			{SoldierAnimationState.AttackRight,new AnimationState{animationName = "shoot4",flipX = true}},
			{SoldierAnimationState.AttackBLU,new AnimationState{animationName = "shoot3",flipX = false}},
			{SoldierAnimationState.AttackBRU,new AnimationState{animationName = "shoot3",flipX = true}},
			{SoldierAnimationState.AttackBLD,new AnimationState{animationName = "shoot5",flipX = false}},
			{SoldierAnimationState.AttackBRD,new AnimationState{animationName = "shoot5",flipX = true}},

			{SoldierAnimationState.Die1,new AnimationState{animationName = "die1",flipX = false}},
			{SoldierAnimationState.Die2,new AnimationState{animationName = "die2",flipX = false}},
			{SoldierAnimationState.Die3,new AnimationState{animationName = "die3",flipX = false}},

		};

		public DragonBones.Animation DBAnimation
		{
			get{
				if(_dbComp != null)
					return _dbComp.animation;
				return null;
			}
		}

		public Animation(UnityArmatureComponent dbComp)
		{
			_dbComp = dbComp;
		}

		public void PlayAnimation(SoldierAnimationState animationState,bool loop)
		{
			if(_dbComp != null)
			{
				AnimationState st = animations[animationState];
				_dbComp.armature.flipX = st.flipX;
				_dbComp.animation.Play(st.animationName,loop ? 0 : 1);
			}
		}

	}
}
