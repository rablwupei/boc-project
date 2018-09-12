using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wuyy {

	public class UIGuocheng : MonoBehaviour {

		public GameObject uiOptionRoot;
		public RectTransform uiOptionPanel;

		public GameObject yinliangGo;

		public UIGuochengLeftPanelClick leftPanel;

		public void MoveLeftPanel() {
			leftPanel.OnBackClick();
		}

		[System.Serializable]
		public class Menu {
			public BaibianType type;
			public GameObject go;
			public AudioClip audioClip;
		}
		public List<Menu> menus;

		[System.Serializable]
		public class MenuItem {
			public MenuItemType type;
			public Button button;
			public Animation anim;
			public AnimationClip clip;
			public AudioClip audio;
		}
		public List<MenuItem> menuItems;

		void Start() {
			foreach (var item in menuItems) {
				if (item.button) {
					item.button.onClick.AddListener(delegate {
						CaidanClick(item.type);
					});
				}
			}
		}

		void OnEnable() {
			yinliangGo.SetActive(false);
			uiOptionRoot.SetActive(false);
		}

		void OnDisable() {
			if (Baibian.instance) {
				Baibian.instance.StopSound();
			}
		}

		public void OnCloseMenu() {
			if (_closeCallback != null) {
				_closeCallback();
				_closeCallback = null;
			}
		}

		System.Action _closeCallback;

		public void OpenMenu(Vector3 pos, BaibianType type, System.Action closeCallback) {
			foreach (var item in menus) {
				if (item.go) {
					if (item.type == type) {
						uiOptionRoot.SetActive(true);
						uiOptionPanel.anchoredPosition = pos;
						item.go.SetActive(true);
						_closeCallback = closeCallback;
					} else {
						item.go.SetActive(false);
					}
				}
			}
		}

		public void ButtonClick(int type) {
			Baibian.instance.Change(type);
		}

		public void ButtonShouye() {
			Baibian.instance.Change(BaibianType.none);
		}

		public void ButtonZhonghang() {
			Baibian.instance.OpenUrl(WebType.中行);
		}

		public void CaidanClick(MenuItemType type) {
			if (type == MenuItemType.专家连线) {
				uiOptionRoot.SetActive(false);
				Baibian.instance.OpenUrl(WebType.专家连线);
				if (_closeCallback != null) {
					_closeCallback();
					_closeCallback = null;
				}
			} else if (type == MenuItemType.宝宝存钱罐) {
				uiOptionRoot.SetActive(false);
				Baibian.instance.uiDuihuakuang.Show(Baibian.instance.baibianType, type, _closeCallback);
				_closeCallback = null;
			} else if (type != MenuItemType.Empty) {
				foreach (var item in menuItems) {
					if (item.button && item.type == type) {
						var anim = item.anim;
						var name = item.clip.name;
						anim.Play(name);
						var state = anim[name];
						Baibian.instance.PlaySound(item.audio);
						Baibian.instance.AddCoverTouchClick(delegate{
							if (anim.isPlaying) {
								return false;
							}
							state.enabled = true;
							state.weight = 1f;
							state.normalizedTime = 0f;
							anim.Sample();
							state.enabled = false;
							state.weight = 0f;
							if (_closeCallback != null) {
								_closeCallback();
								_closeCallback = null;
							}
							uiOptionRoot.SetActive(false);
							return true;
						});
					}
				}
			}
		}

	}

	public enum MenuItemType {
		Empty = 0,
		专家连线,
		国际汇款,
		海外开户,
		留学贷款,
		中银惠投,
		消费金融,
		外币现金预约,
		账单分期,
		常春树,
		大额存款,
		宝宝存钱罐,
	}

}
