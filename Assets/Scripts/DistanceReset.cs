using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceReset : MonoBehaviour {
    public GameObject StandardPos;
    Vector3 Apos;
    [Header("限界距離")]
    [SerializeField]
    float marginalDistance;
    Rigidbody rb;
    private void Start()
    {
     Apos = GetComponent<Pencil>().InitPencilPos;
        
    }
    void Update()
    {
        Vector3 Bpos =this.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);
        //Debug.Log("Distance : " + dis);
        if (marginalDistance <= dis)
        {
            Debug.Log("リセットォ！");
            rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = OperatorManager.Instance.PlayerController.OperatorModel.pencil.InitPencilPos;
           
        }
    }
}
