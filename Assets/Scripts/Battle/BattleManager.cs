using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : BaseSingletonMono<BattleManager> {

	[System.NonSerialized]
	public List<OperatorController> ControllerList = new List<OperatorController>();
	public OperatorController ActiveController { get; set; }
	public OperatorController NonActiveController { get; set; }

	public BattleContext BattleContext { get; private set; }

	protected override void Awake() {
		base.Awake();

		BattleContext = new BattleContext();
		ActiveController = null;
		NonActiveController = null;
	}

	private void Start() {
		StartCoroutine(Fade_In_Out.Instance.FadeIn(1));
	}

	void Update() {
		// コンテキストのステート滞在中の処理
		BattleContext.ExecuteUpdate();
	}

	// 勝手に鉛筆を投げる
	public void ForceThrowPencil(OperatorController oc) {
		oc.GetComponentInChildren<Throw_ball>().ThrowPencil();
	}

	// 両者の投擲フェイズ開始
	public void StartPencilsOutcomeDetection() {
		ControllerList.ForEach(oc => {
			oc.OperatorModel.pencil.StartOutcomeDetection();
		});
	}

	// どちらかの出目が0かチェック
	public bool CheckOutcomesContainZero() {
		var playerOutcome = OperatorManager.Instance.PlayerController.OperatorModel.pencil.Outcome;
		var cpuOutcome = OperatorManager.Instance.ComputerController.OperatorModel.pencil.Outcome;

		return playerOutcome == 0 || cpuOutcome == 0;
	}

	// 出目が同値かチェック
	public bool CheckEachOutcomeSame() {
		var playerOutcome = OperatorManager.Instance.PlayerController.OperatorModel.pencil.Outcome;
		var cpuOutcome = OperatorManager.Instance.ComputerController.OperatorModel.pencil.Outcome;

		return playerOutcome == cpuOutcome;
	}

	// 出目が異なる値かチェック
	//public bool CheckEachOutcomeDifferent() {

	//	var playerOutcome = OperatorManager.Instance.PlayerController.OperatorModel.pencil.Outcome;
	//	var cpuOutcome = OperatorManager.Instance.ComputerController.OperatorModel.pencil.Outcome;

	//	return playerOutcome != cpuOutcome;
	//}

	public void StartBattle() {
		StartCoroutine(StartBattleCoroutine());
	}

	public IEnumerator StartBattleCoroutine() {
		while (BattleContext.CurrentState != BattleContext.stateWait) {
			yield return null;
		}

		BattleContext.ChangeState(BattleContext.stateFight);
	}

	// 攻守交代
	public void SwitchAvtiveController() {
		ActiveController.StopThrow();

		if (ActiveController == ControllerList[0]) {
			var tmp = ActiveController;
			ActiveController = ControllerList[1];
			NonActiveController = tmp;
		}
		else if (ActiveController == ControllerList[1]) {
			var tmp = ActiveController;
			ActiveController = ControllerList[0];
			NonActiveController = tmp;
		}

		ActiveController.StartThrow();
	}

	public void GameFinish() {
		BattleContext.ChangeState(BattleContext.stateResult);

		foreach (var oc in ControllerList) {
			oc.OperatorContext.ToResult();
		}
	}

	public void StartThrowActiveController() {
		ActiveController.StartThrow();
	}
}
