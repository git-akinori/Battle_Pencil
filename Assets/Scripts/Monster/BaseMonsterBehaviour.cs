using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    MonsterModel monsterModel;
    public MonsterModel MonsterModel { get { return monsterModel; } }
    public MonsterContext MonsterContext { private set; get; }

    public Animator _Animator { get; private set; }

    private OperatorModel operatorModel;

    void Awake()
    {
        MonsterContext = new MonsterContext();
        _Animator = GetComponent<Animator>();
    }

    void Start()
    {
        monsterModel.maxHP = monsterModel.hp;

        operatorModel = GetComponentInParent<OperatorController>().OperatorModel;
        operatorModel.monsterBehaviour = this;
		operatorModel.monsterUI.Init();
	}

    public void Damage(int damage)
    {
        StartCoroutine(operatorModel.monsterUI.DamageCoroutine(damage));
    }
}
