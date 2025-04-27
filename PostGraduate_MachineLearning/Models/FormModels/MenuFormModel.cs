using EmotionClassifier.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.Models.FormModels
{
    [ServiceRegistration(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient)]
    public class MenuFormModel
    {
        public string ChoosenParty { get; set; } = string.Empty;
        public DateTime ChoosenStartDate { get; set; } = DateTime.Now;

        public bool IsRequestValid()
        {
            if (string.IsNullOrEmpty(ChoosenParty))
                return false;

            if (ChoosenStartDate > DateTime.Now)
                return false;

            return true;
        }
    }
}
