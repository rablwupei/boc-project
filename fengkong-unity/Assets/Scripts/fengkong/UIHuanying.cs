using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace wuyy.fk {

	public class UIHuanying : UIAbstract {

		public Image fg;

		bool _click;

		public void FgClick() {
			if (!_click) {
				_click = true;
				StartCoroutine(DoFinish());
			}
		}

		IEnumerator DoFinish() {
			if (Fengkong.isNormal) {
				var length = Fengkong.PlaySound("1");
				yield return new WaitForSeconds(length - 2f);
			}
			yield return fg.DOFade(0f, 2f).WaitForCompletion();
			Next();
		}

	}

}
