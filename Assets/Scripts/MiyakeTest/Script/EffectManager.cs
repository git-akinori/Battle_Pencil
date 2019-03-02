using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseSingletonMono<EffectManager> {

    
    string nextBgmName;

   
    //Effect保存用
    Dictionary<string, GameObject> effects;
 
    

    private void Start()
    {
        effects = new Dictionary<string, GameObject>();
       
        object[] effectList = Resources.LoadAll("Effect");
       
        foreach (GameObject effect in effectList)
        {
            effects[effect.name] = effect;
            Debug.Log("effectName" + effect.name);
        }
    }

     public void CreateEffect(string effectName,Vector3 createPosition,float destryTime)
    {
        if (!effects.ContainsKey(effectName))
        {
            Debug.Log("その名前のエフェクはありません。");
          }

        var effect = Instantiate(effects[effectName]as GameObject);
        effect.transform.position = createPosition;
        Destroy(effect,destryTime);
     }

    public void CreateMahou(string effectName, Transform parent, Vector3 createPosition, float destryTime)
    {
        var effect = Instantiate(effects[effectName] as GameObject);
      //  effect.transform.rotation = parent.rotation;
        effect.transform.position = createPosition;
        effect.GetComponent<Rigidbody>().AddForce(parent.transform.forward*120);// transform.position+=parent.transform.forward;
        Destroy(effect, destryTime);



    }


}
