using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIJindu50 : UIAbstract {

		Animation _anim;
		float _time;
		float _timeMax;

		public override void Init () {
			_anim = GetComponent<Animation>();
			_anim.enabled = false;
			base.Init();

			StartCoroutine(DoPlay());
		}


		void Update() {
			if (_time < _timeMax) {
				_time += Time.deltaTime;
				_time = Mathf.Min(_time, _timeMax);
				_anim[_anim.clip.name].time = _time;
				_anim.Sample();
			}
		}

		IEnumerator DoPlay() {
			_timeMax = 176f/60;

			Fengkong.PlaySound("9");
			percentTouch.SetPercent(50);
			percentTouch.SetJielun("2");
			while (audioSound.isPlaying) {
				yield return null;
			}

			_timeMax = 354f/60;

			Fengkong.PlaySound("10");
			percentTouch.SetPercent(60);
			percentTouch.SetJielun("3");
			while (audioSound.isPlaying) {
				yield return null;
			}

			yield return new WaitForSeconds(2f);
			Next();
		}

	}

}
