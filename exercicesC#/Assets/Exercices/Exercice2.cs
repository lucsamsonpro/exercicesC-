using UnityEngine;

public class Exercice2 : MonoBehaviour
{
    /* 
     * NE PAS SUPPRIMER L'ÉNONCÉ DE L'EXERCICE
     * Exercice 2 : Couleur aléatoire
     * Lorsque ce script est attaché à un objet, l'objet doit changer de couleur aléatoirement chaque seconde.
     */

    // Code à compléter (ne rien modifier avant cette ligne)

    public SpriteRenderer spriteRenderer;
    public Color color;

    public void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.color = Color.blue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(this.spriteRenderer.color = Color.blue)
            { 
                this.spriteRenderer.color = Color.red
             }
            else
            { this.spriteRenderer.color = Color.blue;}
        }
    }

    // Fin du code à compléter (ne rien modifier après cette ligne)
}
