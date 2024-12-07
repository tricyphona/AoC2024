using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Report
    {
        public List<int> levels;
        public bool? ascending;
        public bool? safe;
        public bool damped;
        public Report(List<int> levels, bool damped = false)
        {
            this.levels = levels;
            this.damped = damped;
        }
        public void SetDirection()
        {
            this.ascending = (levels[0] < levels[1]);
        }
        public bool? GetDirection()
        {
            return this.ascending;
        }

        public bool? IsSafe()
        {
            if (this.damped)
            {
                if (GetDirection() is null)
                {
                    SetDirection();
                }
                if (this.safe != null)
                {
                    return this.safe;
                }
                bool direction = (bool)GetDirection();
                for (int i = 0; i < levels.Count - 1; i++)
                {
                    if ((!direction && (levels[i] <= levels[i + 1])) || (direction && (levels[i] >= levels[i + 1])))
                    {
                        this.safe = false;
                        return false;
                    }
                    if (Math.Abs(levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    {
                        return false;
                    }
                }
                this.safe = true;
                return true;
            }
            else
            {
                if (GetDirection() is null)
                {
                    SetDirection();
                }
                if (this.safe != null)
                {
                    return this.safe;
                }
                bool ascending = (bool)GetDirection();
                for (int i = 0; i < levels.Count - 1; i++)
                {
                    if ((!ascending && (levels[i] <= levels[i + 1])) || (ascending && (levels[i] >= levels[i + 1])) || Math.Abs(levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    {
                        bool reportImin_safe = false;
                        if (i == 1)
                        {
                            reportImin_safe = this.checkDampedOutcome(this.levels, i - 1);
                        }
                        bool reportI_safe = this.checkDampedOutcome(this.levels, i);
                        bool reportI1_safe = this.checkDampedOutcome(this.levels, i + 1);

                        if (reportImin_safe || reportI1_safe || reportI_safe)
                        {
                            this.safe = true;
                            return true;
                        }
                        this.safe = false;
                        //Console.Write(String.Join(", ", this.levels));
                        //Console.Write("\t");
                        //Console.WriteLine(i);
                        return false;
                    }
                }
                this.safe = true;
                return true;
            }
        }

        bool checkDampedOutcome(List<int> levels, int levelIndex)
        {
            List<int> damped_list = new List<int>(levels);
            damped_list.RemoveAt(levelIndex);
            Report report = new Report(damped_list, true);
            return report.IsSafe() ?? false;
        }
    }
}

