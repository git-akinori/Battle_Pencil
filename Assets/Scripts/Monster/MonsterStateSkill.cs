using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///モンスターのスキルステート
/// </summary>>

public class MonsterStateSkill : IState<MonsterContext> {

    public void ExecuteEntry(MonsterContext context) {
        Debug.Log("[Entry] Monster State : Skill");
        var nonActive = BattleManager.Instance.NonActiveController.OperatorModel;
        var active = BattleManager.Instance.ActiveController.OperatorModel;

        //active.monsterBehaviour.MonsterModel.isAttack = true;
        
        // ディフェンス用のアニメーション遷移
        if (active.monsterBehaviour.MonsterModel.type == Type.DEFENCE) {
            if (nonActive.monsterBehaviour.MonsterModel.isAttack) {
                active.monsterBehaviour._Animator.SetTrigger("SkillTrigger");
            }
            else {
                BattleManager.Instance.BattleContext.isDone = true;
                // ミスの時
                context.ChangeState(context.stateIdle);
            }
        }
        else
            active.monsterBehaviour._Animator.SetTrigger("SkillTrigger");
    }

    public void ExecuteUpdate(MonsterContext context) {
        var nonActive = BattleManager.Instance.NonActiveController.OperatorModel;
        var active = BattleManager.Instance.ActiveController.OperatorModel;

        // 魔法少女のスキル発動か否か
        if (active.monsterBehaviour.MonsterModel.type == Type.HEAL)
        {
            if (active.monsterBehaviour._Animator.GetCurrentAnimatorStateInfo(0).IsName("SkillState") &&
                active.monsterBehaviour._Animator.IsInTransition(0))
            {
                context.ChangeState(context.stateIdle);
            }
        }
        else {
            if (nonActive.monsterBehaviour._Animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") &&
                    nonActive.monsterBehaviour._Animator.IsInTransition(0)) {
                //BattleManager.Instance.BattleContext.isDone = true;
                context.ChangeState(context.stateIdle);
            }
        }
    }

    public void ExecuteExit(MonsterContext context) {
        //var nonActive = BattleManager.Instance.NonActiveController.OperatorModel;
        //var active = BattleManager.Instance.ActiveController.OperatorModel;

        //if (active.monsterBehaviour.MonsterModel.type == Type.ATTACK) {
        //    nonActive.monsterBehaviour.Damage(active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);
            
        //    nonActive.monsterBehaviour.MonsterModel.counterPower =
        //        active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power;
        //}
        //else if (active.monsterBehaviour.MonsterModel.type == Type.DEFENCE) {
        //    if (nonActive.monsterBehaviour.MonsterModel.isAttack) {
        //        nonActive.monsterBehaviour.Damage(active.monsterBehaviour.MonsterModel.counterPower * 2);
                
        //        nonActive.monsterBehaviour.MonsterModel.counterPower = active.monsterBehaviour.MonsterModel.counterPower * 2;
        //    }
        //    else {
        //        Debug.Log("MISS");
        //        active.monsterBehaviour.MonsterModel.isAttack = false;
        //    }
        //}
        //else if (active.monsterBehaviour.MonsterModel.type == Type.HEAL) {
        //    active.monsterBehaviour.Damage(-active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);

        //    active.monsterBehaviour.MonsterModel.isAttack = false;

        //    Debug.Log("ActiveMonsterのHP : " + active.monsterBehaviour.MonsterModel.hp);
        //}

        //Debug.Log("NonActiveMonsterのHP : " + nonActive.monsterBehaviour.MonsterModel.hp);
        //Debug.Log("[Exit] Monster State : Skill");
    }


}
