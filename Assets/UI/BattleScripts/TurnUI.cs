using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : BaseSingletonMono<TurnUI> {

    [SerializeField]
    private Text turnText;
    [SerializeField]
    private GameObject pausePos;
    [SerializeField]
    private GameObject exitPos;
    [SerializeField]
    private GameObject startPos;

    //二点間の距離を入れる
    private float distance_two;
    public float speed = 1.0F;

    private Vector3 vector = Vector3.zero;

   
    // Use this for initialization
    void Start () {
        turnText.color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	//void Update () {
 //       if (Input.GetKeyDown(KeyCode.Space))
 //       {
 //           ChangeText();
 //       }
	//}

    public void ChangeText(OperatorController oc)
    {
        if(oc == OperatorManager.Instance.PlayerController) {
            turnText.text = "自分のターンです";
            turnText.color = new Color(0f, 1f, 0f, 1f);
        }
        else if(oc == OperatorManager.Instance.ComputerController) {
            turnText.text = "相手のターンです";
            turnText.color = new Color(1f, 0f, 0f, 1f);
        }
        distance_two = Vector3.Distance(startPos.transform.position, exitPos.transform.position);

        StartCoroutine(TextDisplay());
    }

    IEnumerator TextDisplay()
    {
        
        float elapsedTime = 0;
        while (true)
        {

            if (Vector3.Distance(this.transform.position, pausePos.transform.position) >= 0.01)
            {
                // 現在の位置
                //float present_Location = (Time.time * speed) / distance_two;

                elapsedTime += Time.deltaTime * speed;

                // オブジェクトの移動
                transform.position = Vector3.Lerp(startPos.transform.position, pausePos.transform.position, elapsedTime);

                yield return null;
            }
            else
            {
                Debug.Log("条件達成又は失敗");
                break;
            }

        }
        yield return new WaitForSeconds(1);
        elapsedTime = 0;
        while (true)
        {
            if (Vector3.Distance(this.transform.position, exitPos.transform.position) >= 0.01)
            {
                // 現在の位置
                //float present_Location = (Time.time * speed) / distance_two;

                elapsedTime += Time.deltaTime * speed;

                // オブジェクトの移動
                transform.position = Vector3.Lerp(pausePos.transform.position, exitPos.transform.position, elapsedTime);

                yield return null;
            }
            else
            {
                Debug.Log("条件達成又は失敗");
                break;
            }

        }
        turnText.color = new Color(1f, 1f, 1f, 0f);
        transform.position = startPos.transform.position;

    }
}
