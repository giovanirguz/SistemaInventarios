using System;
using System.Collections.Generic;
using SharpArch.Core.CommonValidator;

namespace SistemaInventarios.UI.ValidationHelper
{
    public static class ModelStateValidation
    {
        public static string ValidationResults(ICollection<IValidationResult> errors)
        {
            var errorString = String.Empty;
            errorString += "Se registraron los siguientes errores al grabar".ToUpper() + "\n\n";
            foreach (IValidationResult error in errors)
            {
                errorString += error.PropertyName + ": " + StripMessage(error.Message);
            }

            return errorString;

        }

        static string StripMessage(string message)
        {
            var values = message.Split('|');
            return values[0];
        }
    }
}