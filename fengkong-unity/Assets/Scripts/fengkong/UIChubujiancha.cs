using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIChubujiancha : UIAbstract {

		Animation _anim;
		float _time;
		float _timeMax;

		public override void Init() {
			_anim = GetComponent<Animation>();
			_anim.enabled = false;
			base.Init();

			percentTouch.SetActive(true);
			StartCoroutine(DoPlaySound());
		}


		void Update() {
			if (_time < _timeMax) {
				_time += Time.deltaTime;
				_time = Mathf.Min(_time, _timeMax);
				_anim[_anim.clip.name].time = _time;
				_anim.Sample();
			}
		}

		IEnumerator DoPlaySound() {
			_timeMax = 227f/60;

			percentTouch.SetPercent(30);
			percentTouch.SetJielun("0");
			Fengkong.PlaySound("4-1");
			while (audioSound.isPlaying) {
				yield return null;
			}
			percentTouch.SetPercent(40);
			percentTouch.SetJielun("1");
			Fengkong.PlaySound("5");
			while (audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.PlaySound("6");
			yield return new WaitForSeconds(5f);
			_timeMax = float.MaxValue;
			percentTouch.SetChubujiancha(delegate {
				StartCoroutine(DoYes());
			}, delegate {
				StartCoroutine(DoNo());
			});
		}


		IEnumerator DoYes() {
			Fengkong.PlaySound("8-1");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Next();
		}

		IEnumerator DoNo() {
			Fengkong.PlaySound("7");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.instance.ResetAndNext();
		}

	}


}
