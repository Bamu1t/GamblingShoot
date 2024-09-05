using Photon.Pun;
using System.Collections;
using UnityEngine;

public class RollDice : MonoBehaviourPunCallbacks
{
    public Animator animator;
    public string rollAnimationName; // Le nom de l'animation de roulement du d�
    public float rollDuration = 1.0f; // Dur�e totale de l'animation de roulement

    private PhotonView view;

    private void Start()
    {
        // Assurez-vous que le PhotonView est correctement r�cup�r�
        view = GetComponent<PhotonView>();

        if (view == null)
        {
            Debug.LogError("PhotonView n'est pas attach� � cet objet.");
        }

        // V�rifiez si le composant Animator est assign�
        if (animator == null)
        {
            Debug.LogError("Animator n'est pas assign� dans l'inspecteur.");
        }

        if (!animator.enabled)
        {
            animator.enabled = true;
        }
    }

    private void Update()
    {
        // Le joueur qui poss�de l'objet peut lancer le d�
        if (view.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Espace press�e !");
            DiceRoll();
        }
    }

    // Lancer le d�, seul le propri�taire peut d�clencher le lancer
    public void DiceRoll()
    {
        if (view.IsMine) // Le joueur local (propri�taire) d�clenche le lancer
        {
            int randomResult = Random.Range(1, 7); // Le joueur propri�taire d�cide du r�sultat
            photonView.RPC("RPC_PlayDiceRollAnimation", RpcTarget.All, randomResult); // Synchroniser le r�sultat avec tous les joueurs
        }
    }

    // RPC pour synchroniser le lancement du d� et l'animation avec le r�sultat
    [PunRPC]
    private void RPC_PlayDiceRollAnimation(int result)
    {
        StartCoroutine(PlayDiceRollAnimation(result));
    }

    // Coroutine pour jouer l'animation de lancer du d�
    private IEnumerator PlayDiceRollAnimation(int result)
    {
        if (animator == null)
        {
            Debug.LogError("Animator est nul, impossible de jouer l'animation.");
            yield break;
        }

        animator.speed = 1; // R�initialiser la vitesse de l'animation
        animator.Play(rollAnimationName, 0, 0); // Lancer l'animation du d�but

        // Attendre que l'animation se termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null; // Attendre une frame
        }

        // Positionner l'animation sur la frame correspondant au r�sultat
        float targetFrame = (result - 1) / 6.0f;
        animator.Play(rollAnimationName, 0, targetFrame);
        animator.speed = 0; // Arr�ter l'animation � cette frame

        Debug.Log("Le r�sultat du d� est : " + result);
    }
}
