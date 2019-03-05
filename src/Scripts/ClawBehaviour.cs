using UnityEngine;
using System.Collections;

public class ClawBehaviour : MonoBehaviour {

    public Quaternion metaRot;

    private float fVelocidad = 8.0f;
    private float fAngulo = 25; //está en grados (0-359)

    private int estado;
    private bool bCerrado;
    private Vector3 restriccion;

    public bool bMovLock = false;

    // Use this for initialization
    void Start () {
        restriccion = new Vector3(1, 0, 0);

        estado = 0;
        bCerrado = false;
    }

    private void capturarRotate(bool bCerrado) {
        metaRot = new Quaternion(gameObject.transform.rotation.x,
                                 gameObject.transform.rotation.y,
                                 gameObject.transform.rotation.z,
                                 gameObject.transform.rotation.w);

        metaRot *= Quaternion.Euler(restriccion * fAngulo * ((bCerrado)? -1 : 1));
    }

    // Update is called once per frame
    void Update () {
        if (estado == 0 && Input.GetKeyDown(KeyCode.Space)) {
            if (bCerrado) estado = 2;
            else {
                estado = 1;
            }
            capturarRotate(bCerrado);
        }

        //Debug.Log("estado=" + estado);

        //Debug.Log("bMovLock=" + bMovLock);

        if (!bMovLock) {
            switch (estado)
            {
                case 1:
                    moverClaw(metaRot);
                    break;
                case 2:
                    moverClaw(metaRot);
                    break;
            }
        }

    }

    public void moverClaw(Quaternion rot_inst) {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot_inst, Time.deltaTime * fVelocidad);

        float fDif = Quaternion.Angle(gameObject.transform.rotation, rot_inst);

        //Debug.Log("fDif=" + fDif.ToString("0.00000"));

        if (fDif < 0.5)
        {
            gameObject.transform.rotation = rot_inst;
            estado = 0;
            bCerrado = !bCerrado;
        }
    }

}
