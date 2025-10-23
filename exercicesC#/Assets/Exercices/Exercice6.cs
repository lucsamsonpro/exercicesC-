using System.Collections.Generic;
using UnityEngine;

public class Exercice6 : MonoBehaviour
{
    /* 
     * NE PAS SUPPRIMER L'ÉNONCÉ DE L'EXERCICE
     * Exercice 6 : Mon voisin
     * Le script doit cibler/trouver l'objet le plus proche de l'objet qui possède ce script. 
     * Il faut colorer l'objet le plus proche en rouge.
     * Il y a donc plusieurs objets sur la scène et seul le plus proche est coloré en rouge.
     * Il faut tout faire par script, il ne faut pas modifier les objets directement dans l'éditeur.
     */

    [SerializeField] private List<GameObject> prefabForScene;
    [SerializeField] private List<float> distance;
    private bool prefabIsTcheck = false;
    public float savefloat = 100f;
    public int index;

    private void Update()
    {
        if (!prefabIsTcheck)
        {
            for (int i = 0; i < prefabForScene.Count; i++)
            {
                float value = distance[i];
                if (value < savefloat)
                {
                    savefloat = value;
                    index = i;

                }

            }
            prefabForScene[index].GetComponent<MeshRenderer>().material.color = Color.red;
            prefabIsTcheck = true;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        prefabForScene.Add(other.gameObject);
        float _dist = Vector3.Distance(transform.position, other.transform.position);
        distance.Add(_dist);
    }
    // Fin du code à compléter (ne rien modifier après cette ligne)
}