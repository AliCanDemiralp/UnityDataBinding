using System;
using System.Collections.Generic;
using Assets.UDB.Scripts.Core.Extensions;
using Mono.CSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UCL.Scripts
{
    public class CommandLine : MonoBehaviour
    {
        public KeyCode      ToggleKey = KeyCode.BackQuote;
        public GameObject   Panel;
        public InputField   InputField;
        public Text         FeedText;
        public Scrollbar    FeedScrollbar;

        private readonly List<Command>  _commandHistory     = new List<Command>();
        private int                     _historyRefIndex    = 0;

        private void Start()
        {
            EvaluatorExtensions.Prepare();
        }
        private void Update()
        {
            if (Input.GetKeyDown(ToggleKey))
            {
                Panel.SetActive(!Panel.activeSelf);

                if (Panel.activeSelf)
                {
                    InputField.Select();
                    InputField.ActivateInputField();
                }
                else
                {
                    return;
                }
            }
        
            if (Input.GetKeyUp(KeyCode.Return))
            {
                InputField.text = "";
                InputField.Select();
                InputField.ActivateInputField();
            }

            if (InputField.isFocused)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _historyRefIndex--;
                    if (_historyRefIndex < 0)
                    {
                        _historyRefIndex = 0;
                        return;
                    }

                    InputField.text = _commandHistory[_historyRefIndex].Entry;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _historyRefIndex = Math.Min(_commandHistory.Count, _historyRefIndex + 1);
                    if (_historyRefIndex == _commandHistory.Count)
                        InputField.text = "";
                    else
                        InputField.text = _commandHistory[_historyRefIndex].Entry;
                }
            }
        }

        public void OnCommandSubmit()
        {
            if (string.IsNullOrEmpty(InputField.text) || !Input.GetKeyDown(KeyCode.Return))
                return;
            
            EvaluatorExtensions.Prepare();
            var result  = Evaluator.Run(InputField.text);
            var command = new Command(InputField.text, result);
            _commandHistory.Add(command);
            
            _historyRefIndex = _commandHistory.Count;

            FeedText.text += "\n" + command;
            FeedScrollbar.value = 0.0F;
        }
    }
}
