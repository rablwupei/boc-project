using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIChubujiancha : UIAbstract {

		public override void Init() {
			base.Init();

			percentTouch.SetActive(true);
			percentTouch.SetPercent(40);
			percentTouch.SetJielun(1);
			StartCoroutine(DoPlaySound());
		}

		IEnumerator DoPlaySound() {
			Fengkong.PlaySound("4-1");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.PlaySound("5");
			while (audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.PlaySound("6");
			while (audioSound.isPlaying) {
				yield return null;
			}
			percentTouch.SetChubujiancha(delegate {
				if (!_doYesOrDoNo) {
					_doYesOrDoNo = true;
					StartCoroutine(DoYes());
				}
			}, delegate {
				if (!_doYesOrDoNo) {
					_doYesOrDoNo = true;
					StartCoroutine(DoNo());
				}
			});
		}


		bool _doYesOrDoNo;
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
