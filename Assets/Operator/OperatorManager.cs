using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorManager : BaseSingletonMono<OperatorManager> {

	public OperatorController PlayerController { get; set; }
	public OperatorController ComputerController { get; set; }

	private IEnumerator Start() {

		while (PlayerController == null || ComputerController == null) {
			yield return null;
		}

		while (PlayerController.OperatorModel == null || ComputerController.OperatorModel == null) {
			yield return null;
		}

		while (PlayerController.OperatorModel.monsterBehaviour == null
			|| ComputerController.OperatorModel.monsterBehaviour == null) {
			yield return null;
		}

		//while (PlayerController.OperatorModel.monsterBehaviour.MonsterModel == null
		//	|| ComputerController.OperatorModel.monsterBehaviour.MonsterModel == null) {
		//	yield return null;
		//}

		MonsterContext context;

		while (true) {
			if (PlayerController.OperatorModel.monsterBehaviour.MonsterModel.hp <= 0) {
				DataManager.instance.isPlayerWinner = false;
				context = PlayerController.OperatorModel.monsterBehaviour.MonsterContext;
				break;
			}
			else if (ComputerController.OperatorModel.monsterBehaviour.MonsterModel.hp <= 0) {
				DataManager.instance.isPlayerWinner = true;
				context = ComputerController.OperatorModel.monsterBehaviour.MonsterContext;
				break;
			}
			yield return null;
		}

		//context.ChangeState(context.stateDeath);

		//BattleManager.Instance.GameFinish();
	}
}
