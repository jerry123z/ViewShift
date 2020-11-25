using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public int count;
    public GameObject item;
    public DoorActions script;

    public AudioClip push;
    private AudioSource audioSource;

    public virtual void Start()
    {
        script = item.GetComponent<DoorActions>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        //EnterAction?.Invoke();
        // print("enter");
        // audioSource.PlayOneShot(push, 0.7f);
        print("enter");
        count += 1;
        script.Open();
    }

    void OnTriggerExit(Collider other)
    {
        // print("exit");
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
