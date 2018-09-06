//#define OPEN_WEB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if OPEN_WEB
using ZenFulcrum.EmbeddedBrowser;
#endif

namespace wuyy {

	public enum WebType {
		中行,
		专家连线,
	}

	public class UIWeb : MonoBehaviour {
		Action _closeCallback;

		static UIWeb Create() {
			var obj = Instantiate(Baibian.instance.prefabUIWeb);
			obj.transform.SetParent(Baibian.instance.canvasTrans, false);
			return obj;
		}

		public static bool Show(WebType type, Action closeCallback) {
			string url = null;
			switch (type) {
			case WebType.中行:
				url = "http://www.boc.cn/";
				break;
			case WebType.专家连线:
				url = "https://cc.egoonet.com:8080/zhweb/index.html";
				break;
			}
			var value = false;
#if OPEN_WEB
			if (!string.IsNullOrEmpty(url)) {
				var uiWeb = Create();
				uiWeb._closeCallback = closeCallback;
				uiWeb._Open(url);
				value = true;
			}
#else
            value = true;
            System.Diagnostics.Process.Start("chrome.exe", "--kiosk " + url);
            System.Diagnostics.Process.Start(GetRootPath() + "/chrome_close.exe");
#endif
            return value;
		}

        static string GetRootPath() {
            var path = Application.dataPath;
            if (Application.platform == RuntimePlatform.OSXPlayer) {
                path += "/../..";
            } else if (Application.platform == RuntimePlatform.WindowsPlayer) {
                path += "/..";
            }
            return path;
        }

		public void Hide() {
			if (_closeCallback != null) {
				_closeCallback();
				_closeCallback = null;
			}
			Destroy(gameObject);
		}

		#if OPEN_WEB
		Browser _browser;
		Browser browser {
			get {
				if (_browser == null) {
					_browser = GetComponentInChildren<Browser>();
				}
				return _browser;
			}
		}

		void _Open(string url) {
			browser.LoadURL(url, true);
		}

		#endif

	}

}
