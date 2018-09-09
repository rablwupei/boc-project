using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Events;

namespace wuyy.fk {

	public class UIPercentTouch : MonoBehaviour {

		public Text percent;

		public CanvasGroup jielun;
		public CanvasGroup chubujiancha;

		public void SetActive(bool value) {
			gameObject.SetActive(value);
			if (!value) {
				SetPercent(0, 0);
			}
		}

		public void SetJielun(int index) {
			ResetJielunPanel(jielun);
		}

		void ResetJielunPanel(CanvasGroup group, Action callback = null) {
			StartCoroutine(DoResetJielunPanel(group, callback));
		}

		CanvasGroup _group;

		IEnumerator DoResetJielunPanel(CanvasGroup group, Action callback = null) {
			if (_group) {
				yield return _group.DOFade(0f, 0.5f).WaitForCompletion();
			}
			_group = group;
			group.gameObject.SetActive(true);
			group.alpha = 0f;
			yield return group.DOFade(1f, 0.5f).WaitForCompletion();
			if (callback != null) {
				callback();
			}
		}

		public void SetChubujiancha(params UnityAction[] actions) {
			var buttons = chubujiancha.GetComponentsInChildren<Button>();
			for (int i = 0; i < buttons.Length && i < actions.Length; i++) {
				var button = buttons[i];
				var action = actions[i];
				button.enabled = false;
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(delegate {
					button.enabled = false;
					if (action != null) {
						action();
					}
				});
			}
			ResetJielunPanel(chubujiancha, delegate {
				for (int i = 0; i < buttons.Length; i++) {
					buttons[i].enabled = true;
				}
			});
		}

		int _numStart;
		int _numEnd;
		int _num;
		float _numTime;
		float _numTimeMax;

		public void SetPercent(int percent, float time = 2f) {
			if (time <= 0f) {
				_num = percent;
				_numStart = percent;
				_numEnd = percent;
				_numTime = 0f;
				_numTimeMax = 0f;
			} else {
				_numStart = _num;
				_numEnd = percent;
				_numTime = 0f;
				_numTimeMax = time;
			}
		}

		void Update() {
			if (_num < _numEnd) {
				_num = (int)Mathf.Lerp(_numStart, _numEnd, _numTime / _numTimeMax);
				_num = Mathf.Min(_num, _numEnd);
				percent.text = _num + "%";
				_numTime += Time.deltaTime;
			}
		}

	}

}