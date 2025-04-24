using UnityEngine;

//Importar Paquete de Animation Rigging.
using UnityEngine.Animations.Rigging;

public class ToggleRig : MonoBehaviour
{
    public Rig neckRig;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ToggleRigOnOff(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ToggleRigOnOff(false);
        }
    }

    public void ToggleRigOnOff(bool toogle)
    {
        if (toogle)
        {
            neckRig.weight = 1;
        }
        else
        {
            neckRig.weight = 0;
        }
    }
}
