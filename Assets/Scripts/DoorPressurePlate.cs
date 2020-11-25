using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorPressurePlate : MonoBehaviour
{
    protected int count;
    public GameObject item;
    private DoorActions script;

    private void Start()
    {
        script = item.GetComponent<DoorActions>();
        count = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        //EnterAction?.Invoke();
        print("enter");
        count += 1;
        script.Open();
        GetComponent<Animator>().SetBool("Pressed", true);
    }

    void OnCollisionExit(Collision collision)
    {
        print("exit");
        //ExitAction?.Invoke();
        if (count > 0)
        {
            count -= 1;
            if (count == 0)
            {
                script.Close();
                GetComponent<Animator>().SetBool("Pressed", false);
            }
        }
    }
}
