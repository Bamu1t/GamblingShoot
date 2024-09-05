using Photon.Pun;
using System.Collections;
using UnityEngine;

public class RollDice : MonoBehaviourPunCallbacks
{
    public Animator animator;
    public string rollAnimationName; // Le nom de l'animation de roulement du dé
    public float rollDuration = 1.0f; // Durée totale de l'animation de roulement

    private PhotonView view;

    private void Start()
    {
        // Assurez-vous que le PhotonView est correctement récupéré
        view = GetComponent<PhotonView>();

        if (view == null)
        {
            Debug.LogError("PhotonView n'est pas attaché à cet objet.");
        }

        // Vérifiez si le composant Animator est assigné
        if (animator == null)
        {
            Debug.LogError("Animator n'est pas assigné dans l'inspecteur.");
        }

        if (!animator.enabled)
        {
            animator.enabled = true;
        }
    }

    private void Update()
    {
        // Le joueur qui possède l'objet peut lancer le dé
        if (view.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Espace pressée !");
            DiceRoll();
        }
    }

    // Lancer le dé, seul le propriétaire peut déclencher le lancer
    public void DiceRoll()
    {
        if (view.IsMine) // Le joueur local (propriétaire) déclenche le lancer
        {
            int randomResult = Random.Range(1, 7); // Le joueur propriétaire décide du résultat
            photonView.RPC("RPC_PlayDiceRollAnimation", RpcTarget.All, randomResult); // Synchroniser le résultat avec tous les joueurs
        }
    }

    // RPC pour synchroniser le lancement du dé et l'animation avec le résultat
    [PunRPC]
    private void RPC_PlayDiceRollAnimation(int result)
    {
        StartCoroutine(PlayDiceRollAnimation(result));
    }

    // Coroutine pour jouer l'animation de lancer du dé
    private IEnumerator PlayDiceRollAnimation(int result)
    {
        if (animator == null)
        {
            Debug.LogError("Animator est nul, impossible de jouer l'animation.");
            yield break;
        }

        animator.speed = 1; // Réinitialiser la vitesse de l'animation
        animator.Play(rollAnimationName, 0, 0); // Lancer l'animation du début

        // Attendre que l'animation se termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null; // Attendre une frame
        }

        // Positionner l'animation sur la frame correspondant au résultat
        float targetFrame = (result - 1) / 6.0f;
        animator.Play(rollAnimationName, 0, targetFrame);
        animator.speed = 0; // Arrêter l'animation à cette frame

        Debug.Log("Le résultat du dé est : " + result);
    }
}
