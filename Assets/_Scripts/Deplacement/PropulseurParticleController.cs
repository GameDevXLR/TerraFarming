using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulseurParticleController : MonoBehaviour {

    public ParticleSystem rightPropulseur;
    public ParticleSystem leftPropulseur;

    public bool resetProp;
	
	// Update is called once per frame
	void Update () {
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
        BurstAPropulseur(rightPropulseur);
        BurstAPropulseur(leftPropulseur);
    }


    private void BurstAPropulseur(ParticleSystem propulseur)
    {
        var emissionPropulseur = propulseur.emission;
        emissionPropulseur.enabled = true;

        emissionPropulseur.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(propulseur.time + 0.1f, 100, 200) });
    }
}
