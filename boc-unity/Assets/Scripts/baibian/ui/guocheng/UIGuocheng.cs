﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wuyy {

	public class UIGuocheng : MonoBehaviour {

		public GameObject uiOptionRoot;
		public RectTransform uiOptionPanel;

		public GameObject yinliangGo;

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
		}
		public List<MenuItem> menuItems;

		void Start() {
			foreach (var item in menuItems) {
				item.button.onClick.AddListener(delegate {
					CaidanClick(item.type);
				});
			}
		}

		void OnEnable() {
			yinliangGo.SetActive(false);
			uiOptionRoot.SetActive(false);
		}

		void OnDisable() {
			Baibian.instance.StopSound();
		}

		public void OpenMenu(Vector3 pos, BaibianType type) {
			foreach (var item in menus) {
				if (item.type == type) {
					uiOptionRoot.SetActive(true);
					uiOptionPanel.anchoredPosition = pos;
					item.go.SetActive(true);
				} else {
					item.go.SetActive(false);
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
			uiOptionRoot.SetActive(false);
			if (type == MenuItemType.专家连线) {
				Baibian.instance.OpenUrl(WebType.专家连线);
			} else if (type != MenuItemType.Empty) {
				Baibian.instance.uiDuihuakuang.Show(Baibian.instance.baibianType, type);
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
