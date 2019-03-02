using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour {
    [SerializeField]
    Transform skillPos;
    [SerializeField]
    Transform AtackPosition;
    [SerializeField]
    float destryTime;

    [SerializeField]
    float ballDestryTime=1f;

    public void CreateMahouBall(string effectName)
    {
        EffectManager.Instance.CreateMahou(effectName,transform, AtackPosition.position, ballDestryTime);

    }

    public void CreateSkillEffect(string effectName)
    {
        EffectManager.Instance.CreateEffect(effectName, skillPos.position, destryTime);

    }

    public void CreateAtackEffect(string effectName)
    {
        EffectManager.Instance.CreateEffect(effectName,AtackPosition.position,destryTime);
    }
  
    public void CreateE(string name)
    {
        EffectManager.Instance.CreateEffect(name, transform.position, destryTime);

    }

    public void OnSe(string Name)
    {
        SoundManager.Instance.PlayeSE(Name, GetComponent<AudioSource>());
    }
    
    public void DamageTrigger()
    {
        BattleManager.Instance.NonActiveController.OperatorModel.monsterBehaviour._Animator.SetTrigger("DamageTrigger");
    }

    public void AttackDamageAnim()
    {
        var nonActive = BattleManager.Instance.NonActiveController.OperatorModel;
        var active = BattleManager.Instance.ActiveController.OperatorModel;

        active.monsterBehaviour.MonsterModel.isAttack = true;

        if (active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].skillType == SkillType.ATTACK) {
            nonActive.monsterBehaviour.Damage(active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);
            nonActive.monsterBehaviour.MonsterModel.counterPower =
                active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power;
        }
        else if (active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].skillType == SkillType.SKILL) {
            if (active.monsterBehaviour.MonsterModel.type == Type.ATTACK) {
                nonActive.monsterBehaviour.Damage(active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);

                nonActive.monsterBehaviour.MonsterModel.counterPower =
                    active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power;
            }
            else if (active.monsterBehaviour.MonsterModel.type == Type.DEFENCE) {
                if (nonActive.monsterBehaviour.MonsterModel.isAttack) {
                    nonActive.monsterBehaviour.Damage(active.monsterBehaviour.MonsterModel.counterPower * 2);
                    nonActive.monsterBehaviour.MonsterModel.counterPower = active.monsterBehaviour.MonsterModel.counterPower * 2;
                }
                else {
                    Debug.Log("MISS");
                    active.monsterBehaviour.MonsterModel.isAttack = false;
                }
            }
            //else if (active.monsterBehaviour.MonsterModel.type == Type.HEAL) {
            //    active.monsterBehaviour.Damage(-active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);
            //    active.monsterBehaviour.MonsterModel.isAttack = false;
            //}
        }
    }

    public void Heal()
    {
        var active = BattleManager.Instance.ActiveController.OperatorModel;

        active.monsterBehaviour.Damage(-active.monsterBehaviour.MonsterModel.skillList[active.pencil.Outcome - 1].power);
        active.monsterBehaviour.MonsterModel.isAttack = false;
    }
 }
