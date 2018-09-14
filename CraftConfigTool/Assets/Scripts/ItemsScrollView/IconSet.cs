using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class IconSet : ScriptableObject {
    public Sprite[] sprites;
    public Sprite DefaultIcon;
    Dictionary<string, Sprite> Cache = new Dictionary<string, Sprite>();
    public Sprite Get(string name)
    {
        if(Cache.Count == 0)
            for(int i = 0; i < sprites.Length; i++)
            {
                if(!Cache.ContainsKey(sprites[i].name))
                    Cache.Add(sprites[i].name, sprites[i]);
                else
                    Debug.Log("Duplecate name: "+ sprites[i].name);
            }
        return Cache.ContainsKey(name)?Cache[name] : DefaultIcon;
    }
}
