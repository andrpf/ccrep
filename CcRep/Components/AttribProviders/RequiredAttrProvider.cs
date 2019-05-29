using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CcRep.Components.AttribProviders
{
    public class FixedRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public FixedRequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            // set the error message here or use a resource file
            // access the original message with "ErrorMessage"
            var errorMessage = "Required field!";

            return new[] { new ModelClientValidationRequiredRule(errorMessage) };
        }
    }
}