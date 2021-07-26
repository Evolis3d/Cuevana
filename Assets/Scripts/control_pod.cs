using UnityEngine;

public class control_pod : MonoBehaviour
{
    [Header("escapePod")] 
    public GameObject prefabPod;

    [Header("Control Principal")] 
    public control_nave cComp;

    // Update is called once per frame
    void Update()
    {
        //escapePod
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameMode.Lives++;
            
            if (prefabPod) Instantiate(prefabPod, transform.position + new Vector3(0,0.5f,0), transform.rotation);
            //pierdo el control de la nave...
            Destroy(gameObject,3f);
            GameMode.PrisonersKilled += GameMode.PrisonersAboard;
            GameMode.PrisonersAboard = 0;
            cComp.enabled = false;
        }
    }

}
