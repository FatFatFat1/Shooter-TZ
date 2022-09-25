using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
    public int Scope = 0;
    [SerializeField] private GameObject scopeBoard;

    void OnCollisionEnter2D(Collision2D collision)
    {
        scopeBoard.GetComponent<Text>().text = "SCORE - " + Scope;
    }
}
