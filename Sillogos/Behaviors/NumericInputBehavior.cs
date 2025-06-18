using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Sillogos.Behaviors
{
    public class NumericInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            // Allow only digits, with an optional leading minus sign for integers
            e.Handled = !Regex.IsMatch(newText, @"^-?[0-9]*$") || (newText.Contains("-") && textBox.CaretIndex != 0);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                TextBox textBox = (TextBox)sender;
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                string newText = textBox.Text.Insert(textBox.SelectionStart, pastedText);

                // Allow only digits, with an optional leading minus sign for integers
                if (!Regex.IsMatch(newText, @"^-?[0-9]*$") || (newText.Contains("-") && textBox.SelectionStart != 0))
                {
                    e.CancelCommand();
                    e.Handled = true;
                }
            }
            else
            {
                e.CancelCommand();
                e.Handled = true;
            }
        }
    }
}