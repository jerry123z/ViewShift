using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorPressurePlate : MonoBehaviour
{
    protected int count;
    public GameObject item;
    protected DoorActions script;
    public AudioClip pressSFX;
    protected AudioSource audioSource;

    public virtual void Start()
    {
        script = item.GetComponent<DoorActions>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //EnterAction?.Invoke();
        print("enter");
        count += 1;
        if (count == 1) { 
            script.Open();
            GetComponent<Animator>().SetBool("Pressed", true);
            audioSource.PlayOneShot(pressSFX, 0.7f);
        }
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
                audioSource.PlayOneShot(pressSFX, 0.7f);
            }
        }
    }
}
