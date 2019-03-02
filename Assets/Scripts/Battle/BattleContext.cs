using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleContext {
	public readonly IState<BattleContext> stateInit = new BattleStateInit();
	public readonly IState<BattleContext> stateWait = new BattleStateWait();
	public readonly IState<BattleContext> stateFight = new BattleStateFight();
	public readonly IState<BattleContext> stateResult = new BattleStateResult();

	public IState<BattleContext> CurrentState { get; private set; }

	public bool isDone = false;

    public bool isEnd = false;

	/// <summary>
	/// 生成時
	/// </summary>
	public BattleContext() {
		CurrentState = stateInit;
		CurrentState.ExecuteEntry(this);
	}

	/// <summary>
	/// 滞在中のステートの ExecuteUpdate() を実行
	/// </summary>
	public void ExecuteUpdate() {
		CurrentState.ExecuteUpdate(this);
	}

	/// <summary>
	/// ステート遷移
	/// </summary>
	/// <param name="state">遷移先のステート</param>
	public void ChangeState(IState<BattleContext> state) {
		CurrentState.ExecuteExit(this);
		CurrentState = state;
		isDone = false;
		CurrentState.ExecuteEntry(this);
	}
}
