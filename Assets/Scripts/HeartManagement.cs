using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManagement : MonoBehaviour {

    public int lifes = 5;
   
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;

    public List<GameObject> hearts_a;
    public List<GameObject> hearts_b;
    public List<GameObject> hearts_c;
    public List<GameObject> hearts_d;

    Canvas can;
    bool lineBreak = false;

    // Use this for initialization
    void Start () {

        int hearts = 0;
        can = GetComponent<Canvas>();
        

        GameObject tmp_a = a;
        GameObject tmp_b = b;
        GameObject tmp_c = c;
        GameObject tmp_d = d;


        if (lifes > 5)
            lineBreak = true;
        

        for (int i = lifes; i > 0; i--)
        {
            
            if (hearts == 0)
            {
                hearts += 1;
                var createImage = Instantiate(a) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                hearts_a.Add(createImage);
                createImage = Instantiate(b) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                hearts_b.Add(createImage);
                createImage = Instantiate(c) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                hearts_c.Add(createImage);
                createImage = Instantiate(d) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                hearts_d.Add(createImage);
                continue;
            }
            if(hearts == 5)
            {
                hearts += 1;
                var createImage = Instantiate(a, new Vector3(a.transform.position.x, a.transform.position.y-21, a.transform.position.z), a.transform.rotation) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                tmp_a = Instantiate(a, new Vector3(a.transform.position.x, a.transform.position.y-21, a.transform.position.z), a.transform.rotation);
                hearts_a.Add(createImage);
                createImage = Instantiate(b, new Vector3(b.transform.position.x, b.transform.position.y - 21, b.transform.position.z), b.transform.rotation) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                tmp_b = Instantiate(b, new Vector3(b.transform.position.x, b.transform.position.y - 21, b.transform.position.z), b.transform.rotation);
                hearts_b.Add(createImage);
                createImage = Instantiate(c, new Vector3(c.transform.position.x, c.transform.position.y + 21, c.transform.position.z), c.transform.rotation) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                tmp_c = Instantiate(c, new Vector3(c.transform.position.x, c.transform.position.y + 21, c.transform.position.z), c.transform.rotation);
                hearts_c.Add(createImage);
                createImage = Instantiate(d, new Vector3(d.transform.position.x, d.transform.position.y + 21, d.transform.position.z), d.transform.rotation) as GameObject;
                createImage.transform.SetParent(can.transform, false);
                tmp_d = Instantiate(d, new Vector3(d.transform.position.x, d.transform.position.y + 21, d.transform.position.z), d.transform.rotation);
                hearts_d.Add(createImage);
                continue;

             }
            
           if(hearts > 0 & hearts < 5)
            {
                hearts += 1;
                tmp_a = Instantiate(tmp_a, new Vector3(tmp_a.transform.position.x +21, tmp_a.transform.position.y, tmp_a.transform.position.z), tmp_a.transform.rotation);
                var image = Instantiate(tmp_a, new Vector3(tmp_a.transform.position.x, tmp_a.transform.position.y, tmp_a.transform.position.z), tmp_a.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_a.Add(image);
                tmp_b = Instantiate(tmp_b, new Vector3(tmp_b.transform.position.x - 21, tmp_b.transform.position.y, tmp_b.transform.position.z), tmp_b.transform.rotation);
                image = Instantiate(tmp_b, new Vector3(tmp_b.transform.position.x, tmp_b.transform.position.y, tmp_b.transform.position.z), tmp_b.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_b.Add(image);
                tmp_c = Instantiate(tmp_c, new Vector3(tmp_c.transform.position.x + 21, tmp_c.transform.position.y, tmp_c.transform.position.z), tmp_c.transform.rotation);
                image = Instantiate(tmp_c, new Vector3(tmp_c.transform.position.x, tmp_c.transform.position.y, tmp_c.transform.position.z), tmp_c.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_c.Add(image);
                tmp_d = Instantiate(tmp_d, new Vector3(tmp_d.transform.position.x - 21, tmp_d.transform.position.y, tmp_d.transform.position.z), tmp_d.transform.rotation);
                image = Instantiate(tmp_d, new Vector3(tmp_d.transform.position.x, tmp_d.transform.position.y, tmp_d.transform.position.z), tmp_d.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_d.Add(image);
            }
            if (hearts > 5)
            {
                hearts += 1;
                tmp_a = Instantiate(tmp_a, new Vector3(tmp_a.transform.position.x + 21, tmp_a.transform.position.y, tmp_a.transform.position.z), tmp_a.transform.rotation);
                var image = Instantiate(tmp_a, new Vector3(tmp_a.transform.position.x, tmp_a.transform.position.y, tmp_a.transform.position.z), tmp_a.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_a.Add(image);
                tmp_b = Instantiate(tmp_b, new Vector3(tmp_b.transform.position.x - 21, tmp_b.transform.position.y, tmp_b.transform.position.z), tmp_b.transform.rotation);
                image = Instantiate(tmp_b, new Vector3(tmp_b.transform.position.x, tmp_b.transform.position.y, tmp_b.transform.position.z), tmp_b.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_b.Add(image);
                tmp_c = Instantiate(tmp_c, new Vector3(tmp_c.transform.position.x + 21, tmp_c.transform.position.y, tmp_c.transform.position.z), tmp_c.transform.rotation);
                image = Instantiate(tmp_c, new Vector3(tmp_c.transform.position.x, tmp_c.transform.position.y, tmp_c.transform.position.z), tmp_c.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_c.Add(image);
                tmp_d = Instantiate(tmp_d, new Vector3(tmp_d.transform.position.x - 21, tmp_d.transform.position.y, tmp_d.transform.position.z), tmp_d.transform.rotation);
                image = Instantiate(tmp_d, new Vector3(tmp_d.transform.position.x, tmp_d.transform.position.y, tmp_d.transform.position.z), tmp_d.transform.rotation) as GameObject;
                image.transform.SetParent(can.transform, false);
                hearts_d.Add(image);
            }

        }

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("[1]") & hearts_a.Count !=0)
        {
            GameObject del = hearts_a[hearts_a.Count-1];
            Destroy(del);
            hearts_a.RemoveAt(hearts_a.Count-1);
        }
        if (Input.GetKeyDown("[2]") & hearts_b.Count != 0)
        {
            GameObject del = hearts_b[hearts_b.Count - 1];
            Destroy(del);
            hearts_b.RemoveAt(hearts_b.Count - 1);
        }
        if (Input.GetKeyDown("[3]") & hearts_c.Count != 0)
        {
            GameObject del = hearts_c[hearts_c.Count - 1];
            Destroy(del);
            hearts_c.RemoveAt(hearts_c.Count - 1);
        }
        if (Input.GetKeyDown("[4]") & hearts_d.Count != 0)
        {
            GameObject del = hearts_d[hearts_d.Count - 1];
            Destroy(del);
            hearts_d.RemoveAt(hearts_d.Count - 1);
        }

    }
}

