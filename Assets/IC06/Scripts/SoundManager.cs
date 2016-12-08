using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
namespace Loop
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource characterSource;             //Drag a reference to the audio source which will play the sound effects.
        public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
        public AudioMixer mixer;
        public AudioMixerSnapshot[] snapshots;
        public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
        public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
        public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
        public float[] weights;

        void Awake()
        {
            //Check if there is already an instance of SoundManager
            if (instance == null)
                //if not, set it to this.
                instance = this;
            //If instance already exists:
            else if (instance != this)
                //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
                Destroy(gameObject);

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        /*
        //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;

            //Play the clip.
            efxSource.Play();
        }*/

        public void StopCharacterSound()
        {
            //Play the clip.
            characterSource.loop = false;
        }

        public void ChangeMusicPitch(bool day)
        {
            if (day)
            {
                weights[0] = 1f;
                weights[1] = 0f;
                mixer.TransitionToSnapshots(snapshots, weights, 0.5f);
            }
            else
            {
                weights[0] = 0f;
                weights[1] = 1f;
                mixer.TransitionToSnapshots(snapshots, weights, 0.5f);
            }
        }


        public void PlayCharacterSound(bool loop, params AudioClip[] clips)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            characterSource.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            characterSource.clip = clips[randomIndex];

            characterSource.loop = loop;

            if (!characterSource.isPlaying || !loop) { characterSource.Play(); }
                //Play the clip.
                
        }
    }
}