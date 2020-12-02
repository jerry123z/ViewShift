using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class FloatingPlate : MonoBehaviour
{
    protected int count;
    public GameObject item;
    private FloatingForwardAction script;
    public AudioClip pressSFX;
    private AudioSource audioSource;

    private void Start()
    {
        script = item.GetComponent<FloatingForwardAction>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //EnterAction?.Invoke();
        print("enter");
        count += 1;
        script.FloatingForward();
        GetComponent<Animator>().SetBool("Pressed", true);
        audioSource.PlayOneShot(pressSFX, 0.7f);
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
                GetComponent<Animator>().SetBool("Pressed", false);
                audioSource.PlayOneShot(pressSFX, 0.7f);
            }
        }
    }
}
