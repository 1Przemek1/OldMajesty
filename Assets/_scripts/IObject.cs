using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IObject : MonoBehaviour {

    public List<AudioClip> selectSounds;
    public Texture objectImage;

    public string name;

    private int currentSelectSoundPos = 0;

    public void objectSelected()
    {
        transform.FindChild("selectionCircle").gameObject.SetActive(true);
    }

    public void objectDeselected()
    {
        transform.FindChild("selectionCircle").gameObject.SetActive(false);
    }

    public void playSelectSound()
    {
        playSound(selectSounds[currentSelectSoundPos]);
        currentSelectSoundPos++;
        if (currentSelectSoundPos >= selectSounds.Count)
            currentSelectSoundPos = 0;
    }

    public Texture getObjectIcon()
    {
        return objectImage;
    }

    public string getObjectName()
    {
        return name;
    }

    protected void playSound(AudioClip sound)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
    }
}
