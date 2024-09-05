using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// Property listener base.
    /// </summary>
    public class PropertyListenerBase : MonoBehaviour
    {
        public Graphic UpdateIndicator;

        private float Duration = 1f;

        public void OnValueChanged()
        {
            if (UpdateIndicator != null)
            {
                StartCoroutine(FadeOut(UpdateIndicator));
            }
            else
            {
                Debug.LogWarning("UpdateIndicator n'est pas assigné.");
            }
        }

        private IEnumerator FadeOut(Graphic image)
        {
            float elapsedTime = 0.0f;
            Color c = image.color;
            while (elapsedTime < Duration)
            {
                yield return new WaitForSeconds(0.1f); // Utilisez une instruction d'attente valide
                elapsedTime += Time.deltaTime;
                c.a = 1.0f - Mathf.Clamp01(elapsedTime / Duration);
                image.color = c;
            }
        }
    }
}
