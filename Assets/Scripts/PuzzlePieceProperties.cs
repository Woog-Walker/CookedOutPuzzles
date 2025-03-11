using System.Collections.Generic;
using UnityEngine;

namespace CookedOutPuzzle.PuzzlePieceSettings
{
    public class PuzzlePieceProperties : MonoBehaviour
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
        
        void Start()
        {
            // CHANGE COLOR OF FORM
            SelectStartColor();
        }

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
    }
}