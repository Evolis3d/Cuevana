using System.Collections;
using UnityEngine;

public class safezone : MonoBehaviour
{
    public GameObject prefabRescued;
    private float _freqspawn=2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("nave"))
        {
            var naveComp = other.transform.GetComponent<control_nave>();
            if (naveComp.landed)
            {
                StartCoroutine(SpawnRescued(other.transform,_freqspawn));
            }
        }
    }

    IEnumerator SpawnRescued(Transform parent, float delay=1f)
    {
        yield return new WaitForSeconds(delay);
        if (prefabRescued) Instantiate(prefabRescued, parent.position, Quaternion.identity);
    }
}
