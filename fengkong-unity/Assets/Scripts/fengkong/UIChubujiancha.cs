using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIChubujiancha : UIAbstract {

		public override void Init() {
			base.Init();
			StartCoroutine(DoPlaySound());
		}

		IEnumerator DoPlaySound() {
			Fengkong.PlaySound("4-1");
			while (Fengkong.instance.audioSound.isPlaying) {
				yield return null;
			}
			Fengkong.PlaySound("5");
		}

		public void TouchNext() {
			Fengkong.instance.Next();
		}

	}


}
