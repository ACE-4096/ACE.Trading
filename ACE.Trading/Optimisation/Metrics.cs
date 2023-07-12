using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Optimisation
{
    public class Metrics
    {
        // ID
        [JsonProperty("Id")]
        private int _predictionId;
        [JsonIgnore]
        public int predictionId { get { return _predictionId; } }

        // Lock metrics - metrics cannot be changed after
        [JsonProperty("locked")]
        private bool _locked = false;
        [JsonIgnore]
        public bool isLocked { get { return _locked; } }
        public void Lock()
        {
            _locked = true;
        }


        // Metrics

        // Evaluation of the fluency of the predictions
        // i.g are the results in consecutive order
        // Expressed as a %
        [JsonProperty("fluency")]
        private double _fluency;
        [JsonIgnore]
        public double fluency { get { return _fluency; } set { if (!_locked) _fluency = value; } }

        // Overall accuracy of the prediction
        // Expressed as a %
        [JsonProperty("accuracy")]
        private double _accuracy;
        [JsonIgnore]
        public double accuracy { get { return _accuracy; } set { if (!_locked) _accuracy = value; } }

        // Largest price deviance
        // Expressed as a %
        [JsonProperty("largest_price_deviance")]
        private double _priceDeviance;
        [JsonIgnore]
        public double priceDeviance { get { return _priceDeviance; } set { if (!_locked) _priceDeviance = value; } }

        // Largest gradient deviance
        // Expressed as a %
        [JsonProperty("largest_gradient_deviance")]
        private double _gradientDeviance;
        [JsonIgnore]
        public double gradientDeviance { get { return _gradientDeviance; } set { if (!_locked) _gradientDeviance = value; } }

        // Total price deviance
        // Expressed as a %
        [JsonProperty("total_price_deviance")]
        private double _totalPriceDeviance;
        [JsonIgnore]
        public double totalPriceDeviance { get { return _totalPriceDeviance; } set { if (!_locked) _totalPriceDeviance = value; } }

        // Total gradient deviance
        // Expressed as a %
        [JsonProperty("total_gradient_deviance")]
        private double _totalGradientDeviance;
        [JsonIgnore]
        public double totalGradientDeviance { get { return _totalGradientDeviance; } set { if (!_locked) _totalGradientDeviance = value; } }
    }
}
