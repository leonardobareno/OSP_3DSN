using UnityEngine;
using System.Collections;
using SpaceNavigatorDriver;

public class GrupoClawController : MonoBehaviour {

    public GameObject Camera_GameObject_inst;

    private GameObject[] dientes;

    public void Awake()
    {
        SpaceNavigator.SetTranslationSensitivity(1);
        SpaceNavigator.SetRotationSensitivity(1);

        dientes = GameObject.FindGameObjectsWithTag("SingleClaw");
    }

    // Update is called once per frame
    void Update () {

        //Matriz de rotación
        Matrix4x4 m = Matrix4x4.Rotate(Camera_GameObject_inst.transform.rotation);
        Vector3 v3_nuevaTranslacion = m.MultiplyPoint3x4(SpaceNavigator.Translation);

        //Debug.Log("SN transl=" + v3_nuevaTranslacion);
        gameObject.transform.position += v3_nuevaTranslacion;

        //gameObject.transform.RotateAroundLocal(Vector3.up, )
        //Debug.Log("SN rot=" + SpaceNavigator.RotationInLocalCoordSys(Camera_GameObject_inst.transform).eulerAngles);

        //Camera_GameObject_inst.transform.rotation
        Quaternion delta_rot = SpaceNavigator.RotationInLocalCoordSys(Camera_GameObject_inst.transform);
        gameObject.transform.rotation = delta_rot * gameObject.transform.rotation;

        //Debug.Log("transl mag=" + v3_nuevaTranslacion.magnitude.ToString("0.000000"));
        //Debug.Log("rot mag=" + delta_rot.eulerAngles.magnitude.ToString("0.000000"));
        Debug.Log("Length dientes=" + dientes.Length);

        foreach (GameObject diente_inst in dientes) {

            //diente_inst.metaRotAbierto = delta_rot * diente_inst.metaRotAbierto;
            diente_inst.GetComponent<ClawBehaviour>().bMovLock = false;
            if (v3_nuevaTranslacion.magnitude > 0.001 || delta_rot.eulerAngles.magnitude > 0.001) diente_inst.GetComponent<ClawBehaviour>().bMovLock = true;
        }

        //Rango de traslación: -0.13 a 0.20
        //Up es Y
        /*Debug.Log(SpaceNavigator.Translation.x.ToString("0.00000") + " | " +
                  SpaceNavigator.Translation.y.ToString("0.00000") + " |  " +
                  SpaceNavigator.Translation.z.ToString("0.00000") );*/
	}
}
