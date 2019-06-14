using System;
using System.Linq;
using judge_c_sharp.Util;

namespace judge_c_sharp.Feedback
{
    public class FeedbackTable : Group<Tab>
    {

        private Status Status = Status.INTERNAL_ERROR;
        private bool Accepted;
        private String Description;

        public bool IsStatus(Status status)
        {
            return Status == status;
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }

        public void SetAccepted(bool accepted)
        {
            Accepted = accepted;
        }

        public void DeriveDescription()
        {
            long failed = Children().Select(x => x.GetBadgeCount()).Sum();
            long executed = Children().Select(x => x.Children().Count).Sum();

            if (Status == Status.TIME_LIMIT_EXCEEDED)
            {
                Description = executed + " " + TextUtil.Pluralize(executed, "test", "testen") + " uitgevoerd";
            }
            else if (Status == Status.CORRECT)
            {
                Description = (executed - failed) + " " + TextUtil.Pluralize(executed - failed, "test", "testen") + " geslaagd";
            }
            else
            {
                Description = failed + " " + TextUtil.Pluralize(failed, "test", "testen") + " gefaald";
            }
        }
    }
}
