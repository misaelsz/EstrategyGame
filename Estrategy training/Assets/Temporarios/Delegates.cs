using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour {

    public delegate void MeuDelegate();

    private MeuDelegate Aplicacao;

     void Start()
    {
        Aplicacao = MeuMetodo;
        Aplicacao();
    }

    void MeuMetodo() {
        print("Exemplificando o delegate");
    }
}
