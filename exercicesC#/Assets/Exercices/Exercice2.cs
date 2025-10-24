using Unity.VisualScripting;
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
    public int randomColor;
    public float timePassed = 0f;

    public void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        randomColor = 0;
    }
    void Update()
    {
        GenerateRandomColor();
        ChangeToBlue();
        ChangeToRed();
        ChangeToGreen();
        ChangeToYellow();
        ChangeToWhite();
        ChangeToBlack();


    }

    void GenerateRandomColor()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 1f)
        {
            randomColor = Random.Range(0, 5);
            Debug.Log("RandomColor = " + randomColor);
            timePassed = 0f;
        }
    }



    void ChangeToBlue()
    {
        if (randomColor == 0)
            this.spriteRenderer.color = Color.blue;
    }

    void ChangeToRed()
    {
        if (randomColor == 1)
            this.spriteRenderer.color = Color.red;
    }

    void ChangeToGreen()
    {
        if (randomColor == 2)
            this.spriteRenderer.color = Color.green;
    }

    void ChangeToYellow()
    { 
    if (randomColor == 3)
    this.spriteRenderer.color = Color.yellow;
    }

    void ChangeToWhite()
    { 
    if (randomColor == 4)
    this.spriteRenderer.color = Color.white;
    }

    void ChangeToBlack()
    { 
    if (randomColor == 5)
    this.spriteRenderer.color = Color.black;
    }





    // Fin du code à compléter (ne rien modifier après cette ligne)
}
