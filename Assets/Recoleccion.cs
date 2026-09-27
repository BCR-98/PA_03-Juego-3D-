using UnityEngine;
using TMPro;

public class Recoleccion : MonoBehaviour
{
    [Header("Ajustes de Teclas")]
    public KeyCode teclaGuardar = KeyCode.E;   
    public KeyCode teclaSoltar = KeyCode.G;    
    public float distanciaInteraccion = 4.5f;  

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoContador; 

    [Header("Plantilla de la Piedra")]
    [Tooltip("Arrastra aquí el Prefab (cubo azul) de tu piedra")]
    public GameObject prefabPiedra; 

    
    private int piedrasGuardadas = 0;

    void Start()
    {
        ActualizarInterfaz();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(teclaGuardar))
        {
            IntentarGuardar();
        }

        
        if (Input.GetKeyDown(teclaSoltar))
        {
            IntentarSoltar();
        }
    }

    void IntentarGuardar()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit golpe;

        Debug.DrawRay(transform.position, transform.forward * distanciaInteraccion, Color.green, 1.0f);

        if (Physics.Raycast(rayo, out golpe, distanciaInteraccion))
        {
            if (golpe.collider.CompareTag("Recurso"))
            {
                Recurso objetoRecurso = golpe.collider.GetComponent<Recurso>();

                if (objetoRecurso != null)
                {
                    objetoRecurso.Recolectar(); 
                    piedrasGuardadas += 1;      
                    ActualizarInterfaz();
                }
            }
        }
    }

    void IntentarSoltar()
    {
        if (piedrasGuardadas > 0)
        {
            
            Vector3 puntoAparicion = transform.position + (transform.forward * 2f);

            
            Instantiate(prefabPiedra, puntoAparicion, Quaternion.identity);

            piedrasGuardadas -= 1; 
            ActualizarInterfaz();
        }
    }

    void ActualizarInterfaz()
    {
        if (textoContador != null)
        {
            textoContador.text = "Piedras: " + piedrasGuardadas;
        }
    }
}