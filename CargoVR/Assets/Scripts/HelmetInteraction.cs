using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HelmetInteraction : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnHelmetGrabbed);
    }

    private void OnHelmetGrabbed(SelectEnterEventArgs args)
    {
        if (VRButton.instance != null)
        {
            VRButton.instance.task3 = true;
        }
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnHelmetGrabbed);
        }
    }
}
