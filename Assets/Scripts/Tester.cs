using UnityEngine;

public class Tester : MonoBehaviour
{
    public string code = "Hello world";
    
    public void Start()
    {
        Dumpiler.Compile(code);
    }
}