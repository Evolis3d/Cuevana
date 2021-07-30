using UnityEngine;

public class exitable : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Enter/Exit Vehicle") <= -1)
        {
            Debug.Log("Sale del vehiculo!");
            Debug.Break();
        }else if (Input.GetAxisRaw("Enter/Exit Vehicle") >= 1)
        {
            Debug.Log("Entra al vehiculo!");
            Debug.Break();
        }
    }
}
