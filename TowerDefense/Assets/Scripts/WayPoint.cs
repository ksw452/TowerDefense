using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

    [SerializeField]
    GameObject cube;

    List<Transform> cubs = new List<Transform>();

    Coroutine waveCoru;

  

    float speed = 5f;
    private static List<Transform> way = new List<Transform>();
    
    public static List<Transform> Way
    {
        get { 
        
        return way;
        
        }
    }


    IEnumerator CubeIncrease(Transform t,float d)
    {

        Vector3 startScale = t.localScale;

        for (float i = 0; i < d; i += speed*Time.deltaTime)
        {

           
            startScale.z = i;
            t.localScale = startScale;
            yield return null;
        }

    
    
    }


    public void WayStart()
    {

        StopCoroutine(waveCoru);
        for (int i = 0; i < cubs.Count; i++)
        {

            cubs[i].gameObject.SetActive(false);
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        way.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
          
            way.Add (this.transform.GetChild(i));
        }
        
    }


    public IEnumerator GuidePlay()
    {
        for (int i = 0; i < way.Count - 1; i++)
        {
         
            Transform tempCube = GameObject.Instantiate(cube).transform;


           
            tempCube.position = way[i].position;
            tempCube.LookAt(way[i + 1]);

            float distance = Vector3.Distance(tempCube.position, way[i + 1].transform.position);
            cubs.Add(tempCube);
            yield return StartCoroutine(CubeIncrease(tempCube, distance));
        }


        while (true)
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < cubs.Count; i++)
            {

                cubs[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < cubs.Count; i++)
            {
                cubs[i].gameObject.SetActive(true);
                yield return StartCoroutine(CubeIncrease(cubs[i], cubs[i].localScale.z));

            }

        }
  

    


}



private void Start()
{

       waveCoru =  StartCoroutine(GuidePlay());



       

}


}
