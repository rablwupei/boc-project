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
		public CanvasGroup zhongyinwuyun;
		public CanvasGroup yingjiyuan;

		public void SetActive(bool value) {
			gameObject.SetActive(value);
			if (!value) {
				SetPercent(0, 0);
			}
		}

		public void SetJielun(string index) {
			ResetJielunPanel(jielun, delegate {
				var trans = jielun.transform.Find(index);
				if (trans) {
					trans.gameObject.SetActive(true);
				}
			});
		}


		void ResetJielunPanel(CanvasGroup group, Action callback = null) {
			StartCoroutine(DoResetJielunPanel(group, callback));
		}

		CanvasGroup _group;

		IEnumerator DoResetJielunPanel(CanvasGroup group, Action callback = null) {
			if (_group != group) {
				if (_group) {
					yield return _group.DOFade(0f, 0.5f).WaitForCompletion();
				}
				_group = group;
				group.gameObject.SetActive(true);
				group.alpha = 0f;
				yield return group.DOFade(1f, 0.5f).WaitForCompletion();
			}
			if (callback != null) {
				callback();
			}
		}


		void SetButtons(CanvasGroup group, params UnityAction[] actions) {
			var buttons = group.GetComponentsInChildren<Button>();
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
			ResetJielunPanel(group, delegate {
				for (int i = 0; i < buttons.Length; i++) {
					buttons[i].enabled = true;
				}
			});
		}


		public void SetChubujiancha(params UnityAction[] actions) {
			SetButtons(chubujiancha, actions);
		}

		public void SetZhongyinwuyun(UnityAction callback) {
			SetButtons(zhongyinwuyun, callback);
		}

		public void SetYingjiyuan(UnityAction callback) {
			SetButtons(yingjiyuan, callback);
		}

		int _numStart;
		int _numEnd;
		int _num;
		float _numTime;
		float _numTimeMax;

		public void SetPercent(int percent, float time = 1f) {
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
			if (_num != _numEnd) {
				_num = (int)Mathf.Lerp(_numStart, _numEnd, _numTime / _numTimeMax);
				if (_numStart < _numEnd) {
					_num = Mathf.Min(_num, _numEnd);
				} else {
					_num = Mathf.Max(_num, _numEnd);
				}
				percent.text = _num + "%";
				_numTime += Time.deltaTime;
			}
		}

		public void TiaoguoClick() {
			Fengkong.StopSound();
			Fengkong.instance.ResetAndNext();
		}

	}

}