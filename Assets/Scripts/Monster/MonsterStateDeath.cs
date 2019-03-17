using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モンスターの待機ステート
/// モンスターのHPがゼロになったら
/// </summary>
public class MonsterStateDeath : IState<MonsterContext> {

	public void ExecuteEntry(MonsterContext context) {
        var nonActive = BattleManager.Instance.NonActiveController.OperatorModel;

        nonActive.monsterBehaviour._Animator.SetTrigger("DeathTrigger");
    }

    public void ExecuteUpdate(MonsterContext context) {

	}

	public void ExecuteExit(MonsterContext context) {

	}
}
