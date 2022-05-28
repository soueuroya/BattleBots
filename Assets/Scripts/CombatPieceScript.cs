using UnityEngine;
using static TypeHelper;

public class CombatPieceScript : PieceScript
{
    public OutDirection direction;
    public ParticleSystem partSyst;
    public float charge;
    public float maxCharge;
    public void Activate()
    {
        if (partSyst != null)
        {
            partSyst.Play();
        }
    }

    public void Deactivate()
    {
        if (partSyst != null)
        {
            partSyst.Stop();
        }
    }

    protected void OnDrawGizmosSelected() // Setting up default values on editor selection.
    {
        base.OnDrawGizmosSelected();
        charge = maxCharge;
        partSyst = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        charge = maxCharge;
        partSyst = GetComponent<ParticleSystem>();
    }

    private void Awake()
    {
        charge = maxCharge;
        partSyst = GetComponent<ParticleSystem>();
    }
}
