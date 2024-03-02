using System;
using UnityEngine;

namespace Platformer
{
    public class SoulsUI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI soulsText;

        private void Start()
        {
            UpdateSouls();
        }

        public void UpdateSouls()
        {
            StartCoroutine(UpdateSoulsNextFrame());
        }

        System.Collections.IEnumerator UpdateSoulsNextFrame()
        {
            yield return null;
            soulsText.text = GameManager.Instance.VirginsSoulsCollected.ToString();
        }
    }
}