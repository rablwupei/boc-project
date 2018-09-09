using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace wuyy.fk {

	public class UIJingbao : UIAbstract {

		public CanvasGroup[] hideList;
		public Animation animWait;
		public Button nextButton;

		public override void Init () {
			base.Init();

			nextButton.enabled = false;
			nextButton.onClick.AddListener(HideClick);
			StartCoroutine(DoWait());
		}

		IEnumerator DoWait() {
			while (animWait.isPlaying) {
				yield return null;
			}
			while (Fengkong.instance.audioSound.isPlaying && Fengkong.isNormal) {
				yield return null;
			}
			nextButton.enabled = true;
		}

		public void HideClick() {
			nextButton.enabled = false;
			StartCoroutine(DoHide());
		}

		IEnumerator DoHide() {
			var length = Fengkong.PlaySound("3-1");
			var fadeLength = 2f;

			if (Fengkong.isNormal) {
				yield return new WaitForSeconds(length - fadeLength);
			}

			Tween tween = null;
			foreach (var item in hideList) {
				tween = item.DOFade(0f, fadeLength);
			}
			if (tween != null) {
				yield return tween.WaitForCompletion();
			}

			while (Fengkong.instance.audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.instance.Next();
		}
		
	}


}
