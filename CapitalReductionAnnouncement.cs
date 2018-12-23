using PartnersDecisions.GraphDiff.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnersDecisions.Core
{
    [Serializable]
    [Table("CapitalReductionAnnouncement", Schema = "pd")]
    public class CapitalReductionAnnouncement : PartnerDecision
    {
        #region Constractors

        public CapitalReductionAnnouncement() : base()
        {

        }

        public CapitalReductionAnnouncement(Contract contract, float newCapital, DateTime meetingDate, string MeetingDateHijri, string reductionReason, List<PartnerDecisionApproval> Approvals = null, string DecisionText = "")
            : base(contract, Enums.PartnerDecisionStatusEnum.WaitingPartnersApprovals)
        {
            this.NewCapital = newCapital;
            this.MeetingDate = meetingDate;
            this.MeetingDateHijri = MeetingDateHijri;
            this.ContractID = contract.ContractID;
            this.ReductionReason = reductionReason;
            this.CreatedAt = DateTime.Now;
            this.PartnerDecisionApprovals = Approvals;
            this.DecisionText = DecisionText;
            this.PartnerDecisionStatus = Enums.PartnerDecisionStatusEnum.WaitingPartnersApprovals;
            this.SetCreated();
        }

        #endregion

        #region Properties

        public DateTime MeetingDate { get; protected set; }

        [StringLength(10)]
        public string MeetingDateHijri { get; protected set; }

        public float NewCapital { get; protected set; } 

        [StringLength(3000)]
        public string ReductionReason { get; protected set; }


        #endregion
    }
}
