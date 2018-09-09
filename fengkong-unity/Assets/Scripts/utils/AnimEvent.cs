using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class AnimEvent : MonoBehaviour {

		public void PlaySound(string name) {
			Fengkong.PlaySound(name);
		}

		public void PlaySoundLoop(string name) {
			Fengkong.PlaySound(name, true);
		}

		public void SetPercent(int percent) {
			Fengkong.instance.percentTouch.SetPercent(percent);
		}

		public void SetJielun(string index) {
			Fengkong.instance.percentTouch.SetJielun(index);
		}

		public void AnimPause() {
			var anim = GetComponent<Animation>();
			anim[anim.clip.name].normalizedSpeed = 0f;
		}

		public void AnimPauseWithValue(float value) {
			var anim = GetComponent<Animation>();
			anim[anim.clip.name].normalizedSpeed = 0f;
			anim[anim.clip.name].time = value / 60f;
			anim.Sample();
		}

	}


}
