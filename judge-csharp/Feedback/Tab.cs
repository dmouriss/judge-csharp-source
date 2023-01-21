using System;
namespace judge_csharp.Feedback
{
    public class Tab : Group<Context>
    {

        private int BadgeCount;
        private string Description;
        private bool Hidden;

        public Tab(string description) : this(description, false)
        {
        }

        public Tab(string description, bool hidden)
        {
            Description = description;
            Hidden = hidden;
            BadgeCount = 0;
        }

        public void SetBadgeCount(int badgeCount) => this.BadgeCount = badgeCount;

        public void IncrementBadgeCount() => this.BadgeCount += 1;

        public void DecrementBadgeCount() => this.BadgeCount -= 1;

        public int GetBadgeCount() => BadgeCount;
    }
}
