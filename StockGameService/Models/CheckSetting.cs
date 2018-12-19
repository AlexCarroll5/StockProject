using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockGameService.Models
{
    public class CheckSetting
    {
        public string SettingValue { get; set; }
        public bool SettingTF {
            get
            {
                if(SettingValue == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
