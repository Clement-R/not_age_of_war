using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesManager : MonoBehaviour {
	public int baseMoney = 200;
	public int money;

	// TODO : MOVE TO ANOTHER PLACE
	public Side side;
	public Text moneyText;

	void Start () {
		pkm.EventManager.EventManager.StartListening("UnitDie", OnUnitDie);
		money = baseMoney;
		UpdateUI();
	}

	private void OnUnitDie(dynamic o) {
		if (o.side != side) {
			GainMoney(o.reward);
		}
	}

	private void GainMoney(int amount) {
		money += amount;
		UpdateUI();
	}

	public void LoseMoney(int amount) {
		money -= amount;
		money = money <= 0 ? 0 : money;
		UpdateUI();
	}

	private void UpdateUI() {
		moneyText.text = "" + money;
	}
}
