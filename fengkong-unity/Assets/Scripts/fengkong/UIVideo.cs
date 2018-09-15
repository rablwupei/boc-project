using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

namespace wuyy.fk {

	public class UIVideo : UIAbstract {

		public CanvasGroup tipsRoot;
		public CanvasGroup loaderRoot;
		public CanvasGroup logoRoot;
		public CanvasGroup otherRoot;

		public Image tips;
		public Image wenjianjia;
		public Image leftImage;
		public Image rightImage;
		public Text text;
		public Text title;
		public Slider slider;

		public VideoPlayer player;

		public override void Init () {
			base.Init ();

			tipsRoot.alpha = 1f;
			leftImage.fillAmount = 0.12f;
			rightImage.fillAmount = 0f;
			title.SetAlpha(0);
			wenjianjia.SetAlpha(0);
			tips.SetAlpha(0);
			text.SetAlpha(0);

			loaderRoot.alpha = 0f;
			logoRoot.alpha = 0f;
			slider.value = 0f;
			otherRoot.alpha = 0f;

			ReplaceText();

			StartCoroutine(DoInitTips());
			StartCoroutine(DoPlay());
		}

		IEnumerator DoPlay() {
			player.SetTargetAudioSource(0, Fengkong.instance.audioVideo);
			player.controlledAudioTrackCount = 1;
			player.url = GetRootPath() + "/video.mp4";
			player.Prepare();
			while (!player.isPrepared) {
				yield return null;
			}
			player.Play();
		}

		public static string GetRootPath() {
			var path = Application.dataPath;
			if (Application.platform == RuntimePlatform.OSXPlayer) {
				path += "/../..";
			} else if (Application.platform == RuntimePlatform.WindowsPlayer) {
				path += "/..";
			}
			return path;
		}

		IEnumerator DoInitTips() {
			yield return tips.DOFade(1f, 1f).WaitForCompletion();

			leftImage.DOFillAmount(0.63f, 2f);
			yield return rightImage.DOFillAmount(1, 2f).WaitForCompletion();
			otherRoot.DOFade(1f, 1f);
			yield return wenjianjia.DOFade(1f, 1f).WaitForCompletion();

			StartCoroutine(DoInitLoading());

			yield return title.DOFade(1f, 1f).WaitForCompletion();
			yield return text.DOFade(1f, 1f).WaitForCompletion();

			while (true) {
				yield return new WaitForSeconds(15f);
				yield return ReplaceTextAnim();
			}
		}

		IEnumerator DoInitLoading() {
			yield return loaderRoot.DOFade(1f, 1f).WaitForCompletion();
			var length = Fengkong.isNormal ? 10f : 2f;
			yield return slider.DOValue(1f, length).WaitForCompletion();
			yield return loaderRoot.DOFade(0f, 1f).WaitForCompletion();
			yield return logoRoot.DOFade(1f, 1f).WaitForCompletion();

			yield return new WaitForSeconds(1f);

			while (Fengkong.isNormal && player.isPlaying) {
				yield return null;
			}

			yield return tipsRoot.DOFade(0f, 1f).WaitForCompletion();

			Next();
		}

		IEnumerator ReplaceTextAnim() {
			title.DOFade(0f, 1f).WaitForCompletion();
			yield return text.DOFade(0f, 1f).WaitForCompletion();

			ReplaceText();

			title.DOFade(1f, 1f).WaitForCompletion();
			yield return text.DOFade(1f, 1f).WaitForCompletion();
		}

		int _index;
		void ReplaceText() {
			title.text = typeStr1[_index];
			text.text = typeStr2[_index];
			_index++;
			_index = _index % typeStr1.Count;
		}

		public void ButtonTiaoguo() {
			Next();
		}

		static List<string> typeStr1 = new List<string>() {
			"RFID技术", 
			"NB-IoT技术", 
			"知识图谱", 
			"基于语义识别的智能搜索", 
		};
		static List<string> typeStr2 = new List<string>() {
			"RFID即无线射频识别，是集编码、载体、识别通讯等多种技术于一体的综合技术。",
			"NB-IoT是窄带物联网的简称，是运营商构建于蜂窝网络充分利用现有GSM、LTE等现有网络衍生出来的新型通讯模式。",
			"是由一些相互连接的实体和它们的属性构成，用来描述真实世界中存在的各种实体和概念，以及它们之间的关联关系。",
			"基于语料库构建、词语级语义分析、句子级语义分析、篇章级语义分析、深度学习等技术，实现搜索的快速响应、信息整合、可视化展示。",
		};

	}

}
