using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.UI
{
    public class GameLetterRandomGenerator : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text letterText;
        [SerializeField]
        private float letterGenerationFrequency = 0.2f;
        [SerializeField]
        private float sendLetterGenerationDelay = 3f;

        private float deltaTime;
        private event Action OnSymulationFinished;

        public void StartLetterGeneration(Action onSymulationFinished, string supportedLetters)
		{
            OnSymulationFinished = onSymulationFinished;
            StartCoroutine(LetterGenerationCoroutine(supportedLetters));
		}

        public void StopLetterGeneration(char letterFromServer)
		{
            letterText.SetText(letterFromServer.ToString());
        }

        private IEnumerator LetterGenerationCoroutine(string supportedLetters)
		{
            float nextTimeOfGeneration = 0;
            deltaTime = 0;

            while (true)
			{
                nextTimeOfGeneration -= Time.deltaTime;
				if (nextTimeOfGeneration <= 0)
				{
                    letterText.SetText(GetRandomLetter(supportedLetters).ToString());
                    nextTimeOfGeneration = letterGenerationFrequency;
                }
                if(deltaTime >= sendLetterGenerationDelay)
				{
                    OnSymulationFinished?.Invoke();
                }
                yield return null;
			}
        }

        private char GetRandomLetter(string supportedLetters)
		{
            return supportedLetters[Random.Range(0, supportedLetters.Length)];
        }
    }
}