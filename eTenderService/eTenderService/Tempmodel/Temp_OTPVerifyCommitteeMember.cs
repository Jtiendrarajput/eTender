using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;

namespace eTenderService.Tempmodel
{
    public class Temp_OTPVerifyCommitteeMember 
    {
        public int Status { get; set; }
        public List<Temp_OTPReturnCommitteeList> CommitteeMember { get; set; }
    }
}
