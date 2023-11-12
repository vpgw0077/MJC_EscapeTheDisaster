using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_PGW : MonoBehaviour
{
    public enum PortalMode
    {
        leukocyteMode,
        virusMode
    }
    [SerializeField] private PortalMode portalMode = PortalMode.leukocyteMode;
    [SerializeField] private Portal_PGW otherPortal = null;

    private Interact_PGW theInteract;

    private ParticleSystem portalBorder;
    private ParticleSystem.MainModule portalParticleModule;

    private readonly Color leukocytePortalColor = Color.blue;
    private readonly Color virusPortalColor = Color.white;

    private void Awake()
    {
        theInteract = FindObjectOfType<Interact_PGW>();
        portalBorder = GetComponentInChildren<ParticleSystem>();
        portalParticleModule = portalBorder.main;
        portalParticleModule.startColor = portalMode == PortalMode.leukocyteMode ? leukocytePortalColor : virusPortalColor;

    }
    public void ChangePortal()
    {
        if (portalMode == PortalMode.leukocyteMode)
        {
            portalParticleModule.startColor = virusPortalColor;
            portalMode = PortalMode.virusMode;
        }
        else
        {
            portalParticleModule.startColor = leukocytePortalColor;
            portalMode = PortalMode.leukocyteMode;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("White") && portalMode == PortalMode.leukocyteMode)
        {

            if (ReferenceEquals(theInteract.CarriedObject, collision.gameObject))
            {
                theInteract.TryDrop();
            }
            collision.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 3F;
            ChangePortal();
            otherPortal.ChangePortal();


        }
        else if (collision.gameObject.CompareTag("VirusDNA") && portalMode == PortalMode.virusMode)
        {

            if (ReferenceEquals(theInteract.CarriedObject, collision.gameObject))
            {
                theInteract.TryDrop();
            }
            collision.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 3F;
            ChangePortal();
            otherPortal.ChangePortal();
        }
    }
}
