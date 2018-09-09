using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace wuyy.fk {

	public class UIHuanying : UIAbstract {

		public Image fg;

		public void FgClick(Button button) {
			button.enabled = false;
			StartCoroutine(DoFinish());
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
