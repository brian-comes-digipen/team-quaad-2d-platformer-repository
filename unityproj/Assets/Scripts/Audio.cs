using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip pickup;
    public AudioClip damage;
    public AudioClip died;

    private AudioSource source;

    // Start is called before the first frame update
    private void Start()
    {
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void PlayJump()
    {
        source.clip = jump;
        source.Play();
    }

    public void PlayPickup()
    {
        source.clip = pickup;
        source.Play();
    }

    public void PlayDamage()
    {
        source.clip = damage;
        source.Play();
    }

    public void PlayDied()
    {
        source.clip = died;
        source.Play();
    }


}
