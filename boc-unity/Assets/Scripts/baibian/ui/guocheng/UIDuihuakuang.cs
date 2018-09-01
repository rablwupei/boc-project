using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wuyy {

	public class UIDuihuakuang : MonoBehaviour {

		public Text text;

		public void BgClick() {
			gameObject.SetActive(false);
		}

		public void Show(MenuItemType type) {
			gameObject.SetActive(true);
			if (strs.ContainsKey(type)) {
				text.text = strs[type];
			} else {
				text.text = "";
			}
		}

		static Dictionary<MenuItemType, string> strs = new Dictionary<MenuItemType, string>() {
			{MenuItemType.国际汇款, "国际汇款：是将国内账户的钱汇往海外，中银全球智汇产品，为汇款⼈提供优先处理、追踪反馈、费⽤透明、信息完整传递等智能服务，带您进⼊国际汇款全新时代。"},
			{MenuItemType.海外开户, "海外开户见证：实现为将要远赴海外的境内客户，预先开⽴海外账户。中国银⾏众多海外分⽀机构，可以更好服务您海外账户的使⽤。"},
			{MenuItemType.留学贷款, "留学贷款：是提供海外留学⽀付学杂费、交通费、⽣活费的个⼈贷款，⽀持美元、⽇元、欧元、英镑、澳元、加元等多种货币。"},
			{MenuItemType.中银惠投, "中银慧投：依托⼈⼯智能和⼤数据技术，提供优质、专业和便捷的个性化资产配置服务。"},
			{MenuItemType.消费金融, "消费⾦融：当您有家装、购车、教育等需求时，可申请专项消费额度，减轻⽣活负担，提前实现美好⽣活。"},
			{MenuItemType.外币现金预约, "外币现⾦预约：提供25个币种外币现钞服务，更有⼩票⾯现钞和外币零钱包可供选择。"},
			{MenuItemType.账单分期, "账单分期：将最近已出账单中满⾜条件的交易，设置为分期付款，可享⼿续费优惠，缓解短期资⾦压⼒。"},
			{MenuItemType.常春树, "常青树借记IC卡：⾯向养⽼客户推出的专属借记卡产品，具有惠办卡、惠⽤卡、惠健康、惠消费、惠旅游等多项专属优惠和服务。"},
			{MenuItemType.大额存款, "⼤额存单：保本保息，利率最⾼上浮50%，部分靠档计息，可提前⽀取，流动性好。"},
			{MenuItemType.宝宝存钱罐, "宝宝存钱罐：⽤电⼦零花钱奖励孩⼦，培养孩⼦的⾦钱观；可通过转账或者⽀付的⽅式使⽤零花钱。"},
		};

	}


}
