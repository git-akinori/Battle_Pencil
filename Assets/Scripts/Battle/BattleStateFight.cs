using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトルの戦闘ステート
/// </summary>
public class BattleStateFight : IState<BattleContext> {

    int count = 600;

	public void ExecuteEntry(BattleContext context) {
		Debug.LogWarning("[Entry] Battle State : Fight");

		BattleManager.Instance.StartThrowActiveController();
        TurnUI.Instance.ChangeText(BattleManager.Instance.ActiveController);
        
		count = 600;

        context.isEnd = false;
	}

	public void ExecuteUpdate(BattleContext context) {

        if (BattleManager.Instance.ActiveController.OperatorModel.pencil.Outcome != 0)
        {
            BattleManager.Instance.ActiveController.OperatorModel.monsterBehaviour.MonsterContext.ExecuteUpdate();
        }

		// 行動終了時
		if (context.isDone) {
            context.ChangeState(context.stateFight);
		}

		count--;
	}

	public void ExecuteExit(BattleContext context) {
        BattleManager.Instance.SwitchAvtiveController();

        Debug.LogWarning("[Exit] Battle State : Fight");
	}
}
