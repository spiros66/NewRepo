using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Sillogos.Converters
{
    public class DecimalInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.TextChanged += OnTextChanged;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.TextChanged -= OnTextChanged;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Allow only digits, decimal point, and negative sign
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != "." && e.Text != "-")
            {
                e.Handled = true;
            }

            // Ensure only one decimal point and negative sign
            if ((e.Text == "." && AssociatedObject.Text.Contains(".")) ||
                (e.Text == "-" && AssociatedObject.Text.Contains("-")))
            {
                e.Handled = true;
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Update the binding source
            if (AssociatedObject.GetBindingExpression(TextBox.TextProperty) != null)
            {
                AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            // Validate pasted text
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}