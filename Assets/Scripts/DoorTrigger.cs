using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    private int count;
    public GameObject item;
    private DoorActions script;

    private void Start()
    {
        script = item.GetComponent<DoorActions>();
        count = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        //EnterAction?.Invoke();
        count += 1;
        script.Open();
    }
    void OnTriggerExit(Collider other)
    {
        //ExitAction?.Invoke();
        if (count > 0)
        {
            count -= 1;
            if (count == 0){
                script.Close();
            }
        }
    }
}
