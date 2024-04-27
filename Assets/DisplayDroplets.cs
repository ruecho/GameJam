using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DisplayDroplets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dropletsParent;
    public float scalex=0.5f;
    public float scaley = 0.5f;
    public float offsetx = 0.5f;
    public float offsety = 0.5f;
    Material dropletMaterial;
    private void Start()
    {
        dropletMaterial= GetComponent<SpriteRenderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        Assert.IsTrue(dropletMaterial!=null);
        //Debug.Log(dropletMaterial.GetFloat("_ax"));
        //dropletMaterial.SetFloat("_ax", 0.5f);
        //dropletMaterial.SetFloat("_ay", 0.5f);
        

        List<Vector4> tab = new();
        var xs = new float[4] { 4f,   4f,  4f, 4f};
        var ys = new float[4] { 4f,   4f,  4f, 4f};
        {
            int i = 0;
            foreach (Transform c in dropletsParent.GetComponentInChildren<Transform>())
            {
                Vector2 d = c.position-transform.position;
                if (Mathf.Abs(d.x)<12&& Mathf.Abs(d.y) < 8)
                {
                    if (c.gameObject.activeInHierarchy)
                    {
                        xs[i] = d.x * scalex + offsetx;
                        ys[i] = d.y * scaley + offsety;
                        i += 1;
                    }
                }
            }
        }

        for (int i = 0; i<4; i++)
        {
            var x = xs[i];
            var y = ys[i];
            tab.Add(new Vector4(x, 0f, y, 0f));
            tab.Add(new Vector4(0f, 0f, 0f, 0f));
        }
        /*
        tab.Add(new Vector4(1f,0f, 0.5f, 0f) * 0.9f);
        tab.Add(new Vector4(0f,0f, 0f, 0f) * 0.9f);

        tab.Add(new Vector4(2f, 0f, 0.0f, 0f) * 0.9f);
        tab.Add(new Vector4(0f, 0f, 0f, 0f) * 0.9f);
        tab.Add(new Vector4(1f, 0f, 0.0f, 0f) * 0.9f);
        tab.Add(new Vector4(0f, 0f, 0f, 0f) * 0.9f);
        */
        dropletMaterial.SetVectorArray("my_tab", tab);
        
        //Assert.IsTrue(dropletMaterial.GetFloat("_ax") == 0.5f);
        GetComponent<SpriteRenderer>().material = dropletMaterial;
        //Debug.Log("SET");


    }
}
