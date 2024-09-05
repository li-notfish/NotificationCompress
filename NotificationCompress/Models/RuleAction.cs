using NotificationCompress.Helps;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotificationCompress.Models
{
    public class RuleAction
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public RuleEnum RuleType { get; set; }
        [Ignore]
        public List<string> RulePkgList { get; set; }
        public string RulePkg { get; set; }

        public RuleAction()
        {
            
        }

        public RuleAction(string name,RuleEnum ruleEnum)
        {
            Name = name;
            RuleType = ruleEnum;
            RulePkgList = new List<string>();
        }
    }
}
