using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class UIGuocheng : MonoBehaviour {

		[System.Serializable]
		public class Menu {
			public BaibianType type;
			public GameObject go;
		}

		public GameObject uiOptionRoot;
		public RectTransform uiOptionPanel;

		public List<Menu> menus;
		Menu _oldMenu;

		public void OpenMenu(Vector3 pos, BaibianType type) {
			if (_oldMenu != null) {
				_oldMenu.go.SetActive(false);
				_oldMenu = null;
			}
			foreach (var item in menus) {
				if (item.type == type) {
					uiOptionRoot.SetActive(true);
					uiOptionPanel.anchoredPosition = pos;
					item.go.SetActive(true);
					_oldMenu = item;
					break;
				}
			}
		}

		public void ButtonClick(int type) {
			Baibian.instance.Change(type);
		}

		public void ButtonShouye() {
			Baibian.instance.Change(BaibianType.none);
		}

	}

}
