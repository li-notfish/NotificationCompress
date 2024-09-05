using Android.Content;
using Android.Runtime;
using AndroidX.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCompress.Services
{
    public class NextSchedulerMessageWorker : Worker
    {
        public const string TAG = "NextSchedulerWorker";

        private readonly RuleAggregator ruleAggregator = RuleAggregator.Instance;

        public NextSchedulerMessageWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
        }

        public override Result DoWork()
        {
            ruleAggregator.FilterMessageAggregator.Clear();

            return Result.InvokeSuccess();
        }
    }
}
