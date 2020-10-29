using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickObject : MonoBehaviour
{
    public bool attack;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani.SetBool("Attack", attack);
        Destroy(gameObject, 0.5f);
    }
}
