using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessScript : MonoBehaviour
{

    public PostProcessVolume postProcessVolume;
    public Vignette vignette;
    public ChromaticAberration chromatic;
    public Bloom bloom;

    public PlayerMovement playerMovement;

    public GameObject textParticle;
    public GameObject textParticle2;
    public GameObject textParticle3;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        postProcessVolume = GetComponent<PostProcessVolume>();

        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out chromatic);
        postProcessVolume.profile.TryGetSettings(out bloom);

        
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void FixedUpdate()
    {


        ChangeVignetteSettings();

        ChangeChromaticSetting();

        ChangeBloomSetting();

        InsultTexts();

        InsultTexts2();

        NearDeath();
        
    }

    // IN GAME EFFECT CHANGES

    void ChangeVignetteSettings()
    {


        if (playerMovement.playerSanity < 9000 && playerMovement.playerSanity > 8000 && vignette.intensity.value < 0.3f)
        {
            vignette.intensity.value += 0.8f * 0.001f;
        }

        if (playerMovement.playerSanity >= 9000)
        {
            vignette.intensity.value = 0;
        }
    }

    void ChangeChromaticSetting()
    {
        if (playerMovement.playerSanity < 8000 && playerMovement.playerSanity > 6000 && chromatic.intensity.value < 1)
        {
            chromatic.intensity.value += 1 * 0.001f;
        }

        if (playerMovement.playerSanity >= 8000)
        {
            chromatic.intensity.value = 0;
        }
    }

    void ChangeBloomSetting()
    {
        if (playerMovement.playerSanity < 6000 && playerMovement.playerSanity > 4000 && bloom.intensity.value < 1.5f)
        {
            bloom.intensity.value += 1 * 0.01f;
        }

        if (playerMovement.playerSanity >= 6000)
        {
            bloom.intensity.value = 0;
        }
    }

    void InsultTexts()
    {
        if (playerMovement.playerSanity < 6000 && playerMovement.playerSanity > 4000)
        {
            textParticle2.gameObject.SetActive(true);
        }

        if (playerMovement.playerSanity >= 6000)
        {
            textParticle2.gameObject.SetActive(false);
        }
    }

    void InsultTexts2()
    {
        if (playerMovement.playerSanity < 4000 && playerMovement.playerSanity > 3000)
        {
            textParticle3.gameObject.SetActive(true);
            textParticle2.gameObject.SetActive(false);
        }

        if (playerMovement.playerSanity >= 4000)
        {
            textParticle3.gameObject.SetActive(false);
        }
    }

    void NearDeath()
    {

    }

}
