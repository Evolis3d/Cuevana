using UnityEngine;

public class shieldable : MonoBehaviour
{
    public GameObject prefabShield;
    public bool shieldOn=false;

    private GameObject shield;
    private SpriteRenderer ShieldRend;
    Color modo1 = Color.cyan * new Color(0,1,1,0.25f);
    Color modo2 = Color.red * new Color(1,0,0,0.25f);
    Color modo3 = Color.green * new Color(0,1,0,0.25f);
    
    private int currentMode = 1;

    private void Start()
    {
        shield = Instantiate(prefabShield, transform.position, Quaternion.identity);
        ShieldRend = shield.GetComponent<SpriteRenderer>();
        shield.transform.SetParent(transform);
        SetMode(0);

        ToggleShield(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shield"))
        {
            if (shieldOn!=true) ToggleShield(true);
        } else if (Input.GetButtonUp("Shield"))
        {
            if (shieldOn != false) ToggleShield(false);
        }
        //switches between shield modes
        //if (Input.GetButtonDown())
        
    }

    private void SetMode(int mode)
    {
        mode = mode % 3;
        switch (mode)
        {
            case 0:
                ShieldRend.color = modo1;
                break;
            case 1:
                ShieldRend.color = modo2;
                break;
            case 2:
                ShieldRend.color = modo3;
                break;
        }
    }

    private void ToggleShield(bool onOff)
    {
        shieldOn = onOff;
        ShieldRend.enabled = onOff;
        var sComp = shield.GetComponent<CircleCollider2D>();
        sComp.enabled = onOff;
    }

}
