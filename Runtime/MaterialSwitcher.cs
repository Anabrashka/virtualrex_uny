using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Develec.Toolkit
{
    public class MaterialSwitcher : MonoBehaviour
    {
        [Header("Materials")]
        public Material[] materials;

        [Header("UI Configuration")]
        public bool useButtons = false;
        public bool usePreviousButton = false;
        public Button previousButton;
        public Button nextButton;

        [Header("Events")]
        public UnityEvent<int> OnMaterialChanged;

        private Renderer objectRenderer;
        private int currentMaterialIndex = 0;

        private void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                Debug.LogError("[MaterialSwitcher] No Renderer found on this GameObject.");
                return;
            }

            if (materials == null || materials.Length == 0)
            {
                Debug.LogError("[MaterialSwitcher] No materials assigned.");
                return;
            }

            currentMaterialIndex = 0;
            ApplyMaterial();
            SetupButtons();
            UpdateButtonStates();
        }

        private void SetupButtons()
        {
            if (!useButtons)
                return;

            if (nextButton != null)
                nextButton.onClick.AddListener(NextMaterial);

            if (usePreviousButton && previousButton != null)
                previousButton.onClick.AddListener(PreviousMaterial);
        }

        public void NextMaterial()
        {
            if (currentMaterialIndex < materials.Length - 1)
            {
                currentMaterialIndex++;
                ApplyMaterial();
                UpdateButtonStates();
            }
        }

        public void PreviousMaterial()
        {
            if (currentMaterialIndex > 0)
            {
                currentMaterialIndex--;
                ApplyMaterial();
                UpdateButtonStates();
            }
        }

        private void ApplyMaterial()
        {
            objectRenderer.material = materials[currentMaterialIndex];
            OnMaterialChanged?.Invoke(currentMaterialIndex);
        }

        private void UpdateButtonStates()
        {
            if (!useButtons)
                return;

            if (usePreviousButton && previousButton != null)
                previousButton.interactable = currentMaterialIndex > 0;

            if (nextButton != null)
                nextButton.interactable = currentMaterialIndex < materials.Length - 1;
        }
    }
}
