using CookedOutPuzzle.PuzzlePieceSettings;
using System.Collections.Generic;
using UnityEngine;

namespace CookedOutPuzzle.ExitPlace
{
    public class ExitPlaceController : MonoBehaviour
    {
        [SerializeField] List<Material> listOfColors = new List<Material>();

        // type of colors
        public enum PuzzleColorType
        {
            colorOrange,
            colorPurple,
            colorGreen
        }

        public PuzzleColorType selectedColorType;

        // type of forms
        public enum PuzzleFormType
        {
            typeHorizontal,
            typeVertical,
            typeSquare
        }

        public PuzzleFormType selectedFormType;

        Renderer meshRenderer;

        private void Awake() => meshRenderer = GetComponent<Renderer>();

        void Start() => SelectStartColor();

        public void SelectStartColor()
        {
            if (selectedColorType == PuzzleColorType.colorOrange)
                meshRenderer.material.color = listOfColors[0].color;

            else if (selectedColorType == PuzzleColorType.colorPurple)
                meshRenderer.material.color = listOfColors[1].color;

            else if (selectedColorType == PuzzleColorType.colorGreen)
                meshRenderer.material.color = listOfColors[2].color;
        }

        public PuzzleColorType GetColorType() => selectedColorType;
        public PuzzleFormType GetFormType() => selectedFormType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PuzzlePieceProperties>() != null
                && GetColorType().ToString() == other.GetComponent<PuzzlePieceProperties>().GetColorType().ToString()
                && GetFormType().ToString() == other.GetComponent<PuzzlePieceProperties>().GetFormType().ToString())
            {
                Debug.Log($"{GetColorType().ToString()} the same");
                Debug.Log($"{GetFormType().ToString()} the same");
            }
        }
    }
}