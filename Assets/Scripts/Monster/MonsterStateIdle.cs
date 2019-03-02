using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モンスターの待機ステート
/// モンスターの待機アニメーション
/// サイコロの出目が決まったら次のステートへ
/// </summary>
public class MonsterStateIdle : IState<MonsterContext> {

	public void ExecuteEntry(MonsterContext context) {
        Debug.Log("[Entry] Monster State : Idol");   
    }

    public void ExecuteUpdate(MonsterContext context) {
        var active = BattleManager.Instance.ActiveController.OperatorModel;

        if(!BattleManager.Instance.BattleContext.isEnd)
            active.monsterUI.SkillDecision();

        if(active.monsterUI.IsDecision)
        // ActiveControllerの中のスキルタイプによって行動変更
        if (active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].skillType == SkillType.ATTACK) {
            context.ChangeState(context.stateAttack);
        }else if(active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].skillType == SkillType.SKILL) {
            context.ChangeState(context.stateSkill);
        }else if(active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].skillType == SkillType.MISS) {
            Debug.Log("MISS");
            active.monsterBehaviour.MonsterModel.isAttack = false;
            BattleManager.Instance.BattleContext.isDone = true;
            context.ChangeState(context.stateIdle);
                
        }
    }

	public void ExecuteExit(MonsterContext context) {
        Debug.Log("[Exit] Monster State : Idol");

        BattleManager.Instance.BattleContext.isEnd = true;
        BattleManager.Instance.ActiveController.OperatorModel.monsterUI.IsDecision = false;
    }
}
