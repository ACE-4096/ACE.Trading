using Newtonsoft.Json;
using OpenAI_API.FineTune;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Trading.Optimization
{
    /// <summary>
    /// Runs FineTunes in the background looking for the optimal method of finetuning to get the best set of results
    /// </summary>
    /*
     * Compares Metrics
     * 
     * Priorities
     * 0 - careless
     * 10 - crucial
     * 
     * 
     */

    public class OptimizerLimits
    {
        public decimal max_cost;
        public HyperParams maxHyper, minHyper;
    }

    public class Optimizer
    {
        public enum OptimizerStatus
        {
            Working,
            Idle,
            Invalid
        }
        public static string[] metricsToPrioritize = new[] { "fluency", "accuracy", "largest_price_deviance",
            "largest_gradient_deviance", "total_price_deviance", "total_gradient_deviance" };

        public OptimizerStatus status;

        public OptimizerLimits limits;

        public HyperParams hyperParams = new HyperParams { BatchSize = 1, NumberOfEpochs = 1, LearningRateMultiplier = 0.01, PromptLossWeight = 0.01 };

        public Optimizer(Dictionary<string, int> priorities, OptimizerLimits limits) 
        {
            // Priorities

            // checks priorities are set
            if (priorities == null || priorities.Count == 0)
            {
                status = OptimizerStatus.Invalid;
                Debug.WriteLine("Optimizer Faulted, Cause: priorities == null || priorities.Count == 0");
                return;
            }

            // Check that each metric property has a corrorsponding int value, if not default to 0
            foreach (string str in metricsToPrioritize)
            {
                int val;
                if(!priorities.TryGetValue(str, out val))
                {
                    priorities.Add(str, 0);
                    Debug.WriteLine("Optimizer Faulted, Cause: priority not set: " + str);
                }
            }

            // Limits

            // Checks limits are set
            if (limits == null || limits.maxHyper == null || limits.minHyper == null)
            {
                status = OptimizerStatus.Invalid;
                Debug.WriteLine("Optimizer Faulted, Cause: Limits not set");
                return;
            }

            // Checks cost limit is set
            if (limits.max_cost == 0)
            {
                status = OptimizerStatus.Invalid;
                Debug.WriteLine("Optimizer Faulted, Cause: Cost Limit not set");
                return;
            }

            // Check that each limit is set
            if (limits.maxHyper.NumberOfEpochs == 0 || limits.maxHyper.PromptLossWeight == 0 ||
                limits.maxHyper.LearningRateMultiplier == 0 || limits.maxHyper.BatchSize == 0)
            {
                status = OptimizerStatus.Invalid;
                Debug.WriteLine("Optimizer Faulted, Cause: limits.maxHyper values not set");
                Debug.WriteLine(limits.maxHyper);
                return;
            }
            if (limits.minHyper.NumberOfEpochs == 0 || limits.minHyper.PromptLossWeight == 0 ||
                limits.minHyper.LearningRateMultiplier == 0 || limits.minHyper.BatchSize == 0)
            {
                status = OptimizerStatus.Invalid;
                Debug.WriteLine("Optimizer Faulted, Cause: limits.minHyper values not set");
                Debug.WriteLine(limits.minHyper);
                return;
            }

            status = OptimizerStatus.Idle;
        }
    }
}
