using System;
using Codice.Client.Common.GameUI;
using TMPro;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.UI
{
    public class DebugText : MonoBehaviour
    {
        private DebugText()
        {
        }

        private static DebugText _instance;
        public static DebugText Instance => _instance;
        private TextMeshProUGUI _textMeshPrefab;


        public static void InitializeInstance()
        {
            _instance = GameObject.FindObjectOfType<DebugText>();
            
        }

        private void Start()
        {
            

        }
        
        public void Add(ref Action<string> updateTextAction)
        {
            if (_textMeshPrefab == null)
                _textMeshPrefab = Resources.Load<TextMeshProUGUI>(@"Prefabs\UI\DebugText");

            var textMesh = Instantiate(_textMeshPrefab);
            textMesh.rectTransform.SetParent(this.transform);

            updateTextAction += (value) =>
            {
                textMesh.text = value;
            };
        }
    }
}