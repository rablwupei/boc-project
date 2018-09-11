using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIJindu90 : UIAbstract {

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
			Fengkong.PlaySound("12");
			while (audioSound.isPlaying) {
				yield return null;
			}

			_timeMax = 107f/60;
			Fengkong.PlaySound("13");
			yield return new WaitForSeconds(6f);
			percentTouch.SetJielun("6");
			while (audioSound.isPlaying) {
				yield return null;
			}

			_timeMax = 294f/60;
			Fengkong.PlaySound("14-1");
			yield return new WaitForSeconds(4f);
			percentTouch.SetPercent(90);
			yield return new WaitForSeconds(1f);
			_timeMax = 388f/60;
			percentTouch.SetYingjiyuan(delegate {
				StartCoroutine(DoPlay2());
			});
		}

		IEnumerator DoPlay2() {
			Fengkong.PlaySound("15-1");
			yield return new WaitForSeconds(3f);
			_timeMax = 475f/60;
			while (audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.PlaySound("16");
			while (audioSound.isPlaying) {
				yield return null;
			}

			percentTouch.SetPercent(0);
			Fengkong.PlaySound("17");
			yield return new WaitForSeconds(1.5f);
			_timeMax = float.MaxValue;
			percentTouch.SetChubujiancha(delegate {
				StartCoroutine(DoYes());
			}, delegate {
				StartCoroutine(DoNo());
			});
		}


		IEnumerator DoYes() {
			Fengkong.PlaySound("18");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.instance.ResetAndNext();
		}

		IEnumerator DoNo() {
			Fengkong.PlaySound("18");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.instance.ResetAndNext();
		}


	}


}
