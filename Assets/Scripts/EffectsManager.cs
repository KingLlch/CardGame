using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    private static EffectsManager _instance;

    public static EffectsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EffectsManager>();
            }

            return _instance;
        }
    }

    public ParticleSystem[] DamageParticle;
    public ParticleSystem[] DamageBurstParticle;

    public ParticleSystem[] BoostParticle;
    public ParticleSystem[] BoostBurstParticle;

    private Coroutine DestroyCoroutin;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void EndTurnBoost(Transform start, Transform end)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!BoostParticle[i].isPlaying)
            {
                BoostParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                BoostParticle[i].Play();
                BoostParticle[i].transform.DOMove(new Vector3(end.position.x, end.position.y, -50), 0.2f);

                BoostBurstParticle[i].transform.position = new Vector3(end.position.x, end.position.y, -50);
                BoostBurstParticle[i].Play();
                break;
            }
        }
    }

    public void EndTurnDamage(Transform start, Transform end)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!DamageParticle[i].isPlaying)
            {
                DamageParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                DamageParticle[i].Play();
                DamageParticle[i].transform.DOMove(new Vector3(end.position.x, end.position.y, -50), 0.2f);

                DamageBurstParticle[i].transform.position = new Vector3(end.position.x, end.position.y, -50);
                DamageBurstParticle[i].Play();
                break;
            }
        }
    }

    public void Boost(Transform start, Transform end)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!BoostParticle[i].isPlaying)
            {
                BoostParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                BoostParticle[i].Play();
                BoostParticle[i].transform.DOMove(new Vector3(end.position.x, end.position.y, -50), 0.2f);

                BoostBurstParticle[i].transform.position = new Vector3(end.position.x, end.position.y, -50);
                BoostBurstParticle[i].Play();
                break;
            }
        }
    }

    public void SelfBoost(Transform start)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!BoostParticle[i].isPlaying)
            {
                BoostBurstParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                BoostBurstParticle[i].Play();
                break;
            }
        }
    }

    public void Damage(Transform start, Transform end)
    {
        for (int i = 0; i < 9; i++)
        {
            if (DamageParticle[i].isPlaying == false)
            {
                DamageParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                DamageParticle[i].Play();
                DamageParticle[i].transform.DOMove(new Vector3(end.position.x, end.position.y, -50), 0.2f);

                DamageBurstParticle[i].transform.position = new Vector3(end.position.x, end.position.y, -50);
                DamageBurstParticle[i].Play();
                break;
            }
        }
    }

    public void SelfDamage(Transform start)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!DamageParticle[i].isPlaying)
            {
                DamageBurstParticle[i].transform.position = new Vector3(start.position.x, start.position.y, -50);
                DamageBurstParticle[i].Play();
                break;
            }
        }
    }

    public void Destroy(CardInfoScript card)
    {
        card.PointObject.SetActive(false);
        card.CardComponents.SetActive(false);
        card.DestroyGameObject.SetActive(true);

        Material DestroyMaterial = new Material(card.DestroyImage.material);
        card.DestroyImage.material = DestroyMaterial;
        DestroyMaterial.SetFloat("_Trashold",0);
        DestroyCoroutin = StartCoroutine(DestroyCoroutine(card));
    }

    private IEnumerator DestroyCoroutine(CardInfoScript card)
    {
        float trashold = 0;

        while (trashold <= 1)
        {
            trashold += 0.05f;
            card.DestroyImage.material.SetFloat("_Trashold", trashold);
            yield return new WaitForSeconds(0.05f);
        }

        StopCoroutine(DestroyCoroutin);
    }
}
