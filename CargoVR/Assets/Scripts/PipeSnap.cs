using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PipeSnap : MonoBehaviour
{
    public enum Type { Straight, Round, Cross, Tshape, Null }

    public Type type;

    private int typeIndex;
    private XRSocketInteractor interactor;
    private Transform gfx;
    public bool correctPiece;
    public Vector3 correctDesire;

    private void Start()
    {
        interactor = GetComponent<XRSocketInteractor>();
        gfx = transform.GetChild(0);

        switch (type)
        {
            case Type.Straight: typeIndex = 0; break;
            case Type.Round:    typeIndex = 1; break;
            case Type.Cross:    typeIndex = 2; break;
            case Type.Tshape:   typeIndex = 3; break;
            case Type.Null:     typeIndex = 4; break;
        }

        if (typeIndex == 4) correctPiece = true;
    }

    public void OnSnapPipe() // Invoked in Inspector
    {
        if (interactor.GetOldestInteractableSelected() != null && interactor.GetOldestInteractableSelected().transform.TryGetComponent(out Pipe pipe))
        {
            if (pipe.typeIndex == typeIndex)
            {
                // Snap to correct desire
                gfx.transform.rotation = Quaternion.Euler(correctDesire);
                correctPiece = true;
                WallSnap.instance.snappedCorrect[WallSnap.instance.snappedCount++] = true;
                print("Snapped correct");
            }
            else
            {
                // Snap to pipe desire as to not look weird
                gfx.transform.rotation = pipe.SnapToDesire();
                correctPiece = false;
                WallSnap.instance.snappedCount++;
                print("Snapped desire");
            }
        }
    }


    public void RemovePipe()
    {
        if (typeIndex == 4) correctPiece = true;
        WallSnap.instance.snappedCorrect[--WallSnap.instance.snappedCount] = false;
    }
}
