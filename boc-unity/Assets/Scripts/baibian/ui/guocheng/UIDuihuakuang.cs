using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace wuyy {

	public class UIDuihuakuang : MonoBehaviour {

		public Text text;

		public GameObject gege;
		public GameObject jiejie;
		public GameObject jiejieZunqianguan;

		Tweener _textTweener;

		public void BgClick() {
			if (_textTweener != null && _textTweener.IsPlaying()) {
				_textTweener.Complete();
				_textTweener = null;
			} else {
				gameObject.SetActive(false);
				_textTweener = null;
				if (_closeCallback != null) {
					_closeCallback();
					_closeCallback = null;
				}
			}
		}

		System.Action _closeCallback;

		public void Show(BaibianType baibianType, MenuItemType type, System.Action closeCallback) {
			_closeCallback = closeCallback;
			int value = (int)baibianType;
			if (type == MenuItemType.宝宝存钱罐) {
				gege.SetActive(false);
				jiejie.SetActive(false);
				jiejieZunqianguan.SetActive(true);
			} else {
				gege.SetActive(value % 2 != 0);
				jiejie.SetActive(value % 2 == 0);
				jiejieZunqianguan.SetActive(false);
			}
			gameObject.SetActive(true);
			if (menuStrs.ContainsKey(type)) {
				text.text = "";
				text.fontSize = 36;
				var str = menuStrs[type];
				_textTweener = text.DOText(str, 3f);
				_textTweener.SetEase(Ease.Linear);
			} else {
				text.text = "";
			}
		}

		public void Show(BaibianType baibianType, System.Action closeCallback) {
			_closeCallback = closeCallback;
			int value = (int)baibianType;
			gege.SetActive(value % 2 != 0);
			jiejie.SetActive(value % 2 == 0);
			gameObject.SetActive(true);
			if (typeStr.ContainsKey(baibianType)) {
				text.text = "";
				text.fontSize = 36;
				var str = typeStr[baibianType];
				_textTweener = text.DOText(str, str.Length * 5f / typeStr[BaibianType.tongnian].Length);
				_textTweener.SetEase(Ease.Linear);
			} else {
				text.text = "";
			}
		}

		static Dictionary<MenuItemType, string> menuStrs = new Dictionary<MenuItemType, string>() {
			{MenuItemType.国际汇款,    "国际汇款：是将国内账户的钱汇往海外，中银全球智汇产品，为汇款⼈提供优先处理、追踪反馈、费⽤透明、信息完整传递等智能服务，带您进入国际汇款全新时代。"},
			{MenuItemType.海外开户,    "海外开户见证：实现为将要远赴海外的境内客户，预先开立海外账户。中国银行众多海外分支机构，可以更好服务您海外账户的使用。"},
			{MenuItemType.留学贷款,    "留学贷款：是提供海外留学支付学杂费、交通费、⽣活费的个人贷款，支持美元、日元、欧元、英镑、澳元、加元等多种货币。"},
			{MenuItemType.中银惠投,    "中银慧投：依托人工智能和大数据技术，提供优质、专业和便捷的个性化资产配置服务。"},
			{MenuItemType.消费金融,    "消费金融：当您有家装、购车、教育等需求时，可申请专项消费额度，减轻生活负担，提前实现美好生活。"},
			{MenuItemType.外币现金预约, "外币现⾦预约：提供25个币种外币现钞服务，更有小票面现钞和外币零钱包可供选择。"},
			{MenuItemType.账单分期,     "账单分期：将最近已出账单中满足条件的交易，设置为分期付款，可享⼿续费优惠，缓解短期资金压力。"},
			{MenuItemType.常春树,       "常青树借记IC卡：⾯向养老客户推出的专属借记卡产品，具有惠办卡、惠用卡、惠健康、惠消费、惠旅游等多项专属优惠和服务。"},
			{MenuItemType.大额存款,     "大额存单：保本保息，利率最高上浮50%，部分靠档计息，可提前支取，流动性好。"},
			{MenuItemType.宝宝存钱罐,    "宝宝存钱罐：用电子零花钱奖励孩子，培养孩子的金钱观；可通过转账或者支付的方式使用零花钱。"},
		};

		static Dictionary<BaibianType, string> typeStr = new Dictionary<BaibianType, string>() {
			{BaibianType.tongnian,    "亲爱的小朋友，六周岁生日快乐！马上就要上学了，爸爸给你建了“教育基金”，购买了大额存单，妈妈还开通了宝宝存钱罐，祝小可爱学业有成，每天开心哦。"},
			{BaibianType.liuxue,    "还有半年就要去美国留学了，中国银行提供的存款证明、留学签证、学费购汇、海外开户见证、国际汇款等一站式服务，可以让你轻松搞定留学准备，还有更多惊喜等着你，如果资金短缺，留学贷款可以缓解财务压力，还想进一步了解留学国家情况的话，我们的“全球专家”可以更全面地答疑解惑，祝你留学顺利，学有所成。"},
			{BaibianType.chengjia,    "哇，听说你马上就要结婚了，祝福你哦，不过结婚开销一定很大吧，中国银行的消费分期、账单分期产品，可以满足你的大额消费需求，新婚海外旅行，别忘了选择中国银行的存款证明、外币兑换、外币现金预约、全球通信用卡，在国外买买买，回到国内记得来找中国银行代办退税哦。"},
			{BaibianType.caifu,    "时间过得真快，孩子都快上初中了吧，如果您想换房换车，中国银行的个人贷款，消费金融可以满足大额融资需求。家庭财富增值不可忽略，中国银行最新推出的中银慧投产品，提供个性化资产配置服务，为您的资产保值增值，建议重点关注一下啊。"},
			{BaibianType.wannian,    "马上就要退休了，五险一金和养老保险已经帮您办理好了，中国银行面向老年客户推出的常青树借记IC卡，提供多项专属服务，让您安享退休生活。"},
		};
		

	}


}
