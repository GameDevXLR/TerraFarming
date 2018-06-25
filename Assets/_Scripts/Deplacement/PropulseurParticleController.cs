using UnityEngine;

public class PropulseurParticleController : MonoBehaviour
{
    public ParticleSystem rightPropulseur;
    public ParticleSystem leftPropulseur;
    public GameObject rightPropulseurObj;
    public GameObject leftPropulseurObj;

    public bool resetProp;

    // Update is called once per frame
    private void Update()
    {
        if (resetProp)
        {
            var emissionRightPropulseur = rightPropulseur.emission;
            emissionRightPropulseur.enabled = true;

            emissionRightPropulseur.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(rightPropulseur.time + 0.1f, 100, 200) });

            var emissionLeftPropulseur = leftPropulseur.emission;
            emissionLeftPropulseur.enabled = true;

            emissionLeftPropulseur.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(leftPropulseur.time + 0.1f, 100, 200) });

            resetProp = false;
        }
    }

    public void Burst()
    {
        if (!rightPropulseurObj.activeInHierarchy)
        {
            rightPropulseurObj.SetActive(true);
        }
        if (!leftPropulseur.gameObject.activeInHierarchy)
        {
            leftPropulseurObj.SetActive(true);
        }
        BurstAPropulseur(rightPropulseur);
        BurstAPropulseur(leftPropulseur);
    }

    private void BurstPropulseurRight()
    {
        if (!rightPropulseurObj.activeInHierarchy)
        {
            rightPropulseurObj.SetActive(true);
        }
        BurstAPropulseur(rightPropulseur);
    }

    private void BurstPropulseurLeft()
    {
        if (!leftPropulseurObj.activeInHierarchy)
        {
            leftPropulseurObj.SetActive(true);
        }
        BurstAPropulseur(leftPropulseur);
    }

    private void BurstAPropulseur(ParticleSystem propulseur)
    {
        var emissionPropulseur = propulseur.emission;
        emissionPropulseur.enabled = true;

        emissionPropulseur.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(propulseur.time + 0.1f, 100, 200) });
    }

    public void StopPropulseur()
    {
        rightPropulseurObj.SetActive(false);
        leftPropulseurObj.SetActive(false);
    }
}