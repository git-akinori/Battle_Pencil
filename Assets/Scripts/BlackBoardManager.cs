using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardManager : BaseSingletonMono<BlackBoardManager> {
	
	[SerializeField]
	List<Sprite> standingSprite = new List<Sprite>();
	public List<Sprite> StandingSprite { get { return standingSprite; } }

	[System.Serializable]
	public enum ECanvasType {
		Title, Battle, Result
	}

	[System.Serializable]
	public class CanvasContainer {
		public ECanvasType type;
		public GameObject prefCanvas;
	}

	[SerializeField]
	List<CanvasContainer> canvasList = new List<CanvasContainer>();

	List<GameObject> activeCanvasList = new List<GameObject>();

	protected void Awake() {
		base.Awake();

		ChangeCanvas(ECanvasType.Title);
	}

	// Canvas切り替え
	public void ChangeCanvas(ECanvasType canvasType, bool willDestroyActive = true) {

		if (willDestroyActive && activeCanvasList.Count > 0) {
			activeCanvasList.ForEach(canvas => Destroy(canvas));
		}

		canvasList.ForEach(cc => {
			if(canvasType == cc.type)
				activeCanvasList.Add(Instantiate(cc.prefCanvas));
		});
	}
}
