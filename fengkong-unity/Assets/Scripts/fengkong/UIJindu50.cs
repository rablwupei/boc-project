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
			_timeMax = 199f/60;

			Fengkong.PlaySound("9");
			percentTouch.SetPercent(50);
			percentTouch.SetJielun("2");
			while (audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.PlaySound("10");
			percentTouch.SetPercent(60);
			percentTouch.SetJielun("3");

			yield return new WaitForSeconds(4f);
			_timeMax = 321f/60;
			yield return new WaitForSeconds(3f);

			percentTouch.SetPercent(75);
			percentTouch.SetJielun("4");

			yield return new WaitForSeconds(4f);
			_timeMax = 589f/60f;

			while (audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.PlaySound("11");
			percentTouch.SetJielun("5");
			yield return new WaitForSeconds(4.5f);
			_timeMax = float.MaxValue;
			percentTouch.SetZhongyinwuyun(delegate {
				Next();	
			});
		}

	}

}
