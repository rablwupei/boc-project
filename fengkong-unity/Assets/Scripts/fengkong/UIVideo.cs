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

			player.SetTargetAudioSource(0, Fengkong.instance.audioSound);
			player.Play();

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

			ReplaceText();

			StartCoroutine(DoInitTips());
		}

		IEnumerator DoInitTips() {
			yield return tips.DOFade(1f, 1f).WaitForCompletion();

			leftImage.DOFillAmount(0.63f, 2f);
			yield return rightImage.DOFillAmount(1, 2f).WaitForCompletion();
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
			yield return slider.DOValue(1f, 10f).WaitForCompletion();
			yield return loaderRoot.DOFade(0f, 1f).WaitForCompletion();
			yield return logoRoot.DOFade(1f, 1f).WaitForCompletion();

			yield return new WaitForSeconds(1f);

			yield return tipsRoot.DOFade(0f, 1f).WaitForCompletion();

			while (Fengkong.isNormal && player.isPlaying) {
				yield return null;
			}
			Next();
		}

		IEnumerator ReplaceTextAnim() {
			title.DOFade(0f, 1f).WaitForCompletion();
			yield return text.DOFade(0f, 1f).WaitForCompletion();

			ReplaceText();

			title.DOFade(1f, 1f).WaitForCompletion();
			yield return text.DOFade(1f, 1f).WaitForCompletion();
		}

		void ReplaceText() {
			var keys = new List<string>(typeStr.Keys);
			var index = Random.Range(0, keys.Count);
			var key = keys[index];
			title.text = key;
			text.text = typeStr[key];
		}

		static Dictionary<string, string> typeStr = new Dictionary<string, string>() {
			{"tongnian",    "亲爱的小朋友，六周岁生日快乐！马上就要上学了，爸爸给你建了“教育基金”，购买了大额存单，妈妈还开通了宝宝存钱罐，祝小可爱学业有成，每天开心哦。"},
			{"liuxue",    "还有半年就要去美国留学了，中国银行提供的存款证明、留学签证、学费购汇、海外开户见证、国际汇款等一站式服务，可以让你轻松搞定留学准备，还有更多惊喜等着你，如果资金短缺，留学贷款可以缓解财务压力，还想进一步了解留学国家情况的话，我们的“全球专家”可以更全面地答疑解惑，祝你留学顺利，学有所成。"},
			{"chengjia",    "哇，听说你马上就要结婚了，祝福你哦，不过结婚开销一定很大吧，中国银行的消费分期、账单分期产品，可以满足你的大额消费需求，新婚海外旅行，别忘了选择中国银行的存款证明、外币兑换、外币现金预约、全球通信用卡，在国外买买买，回到国内记得来找中国银行代办退税哦。"},
			{"caifu",    "时间过得真快，孩子都快上初中了吧，如果您想换房换车，中国银行的个人贷款，消费金融可以满足大额融资需求。家庭财富增值不可忽略，中国银行最新推出的中银慧投产品，提供个性化资产配置服务，为您的资产保值增值，建议重点关注一下啊。"},
			{"wannian",    "马上就要退休了，五险一金和养老保险已经帮您办理好了，中国银行面向老年客户推出的常青树借记IC卡，提供多项专属服务，让您安享退休生活。"},
		};

	}

}
