using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SceneTransitionTest : MonoBehaviour {

	[SerializeField]
	Button titleButton;

	[SerializeField]
	Button battleButton;

	[SerializeField]
	Button resultButton;

	void Start() {
		titleButton.gameObject.SetActive(false);
		battleButton.gameObject.SetActive(true);
		resultButton.gameObject.SetActive(false);

		titleButton.OnClickAsObservable().Subscribe(_ => {
			titleButton.gameObject.SetActive(false);
			battleButton.gameObject.SetActive(true);
			BlackBoardManager.Instance.ChangeCanvas(BlackBoardManager.ECanvasType.Title);
			MySceneManager.Instance.ChangeScene(MySceneManager.ESceneType.Title);
			PositionManager.Instance.ChangePosition(0, 0);
		});

		battleButton.OnClickAsObservable().Subscribe(_ => {
			DataManager.Instance.SetPlayerPencil(2);
			battleButton.gameObject.SetActive(false);

			Fade_In_Out.Instance.StartFade(1, 3, () => {
				BlackBoardManager.Instance.ChangeCanvas(BlackBoardManager.ECanvasType.Battle);
				MySceneManager.Instance.ChangeScene(MySceneManager.ESceneType.Battle);
				PositionManager.Instance.ChangePosition(0, 1);
				resultButton.gameObject.SetActive(true);
			}, () => {
				BattleManager.Instance.StartBattle();
			});
		});

		resultButton.OnClickAsObservable().Subscribe(_ => {
			BlackBoardManager.Instance.ChangeCanvas(BlackBoardManager.ECanvasType.Result);
			resultButton.gameObject.SetActive(false);
			titleButton.gameObject.SetActive(true);
		});
	}
}
