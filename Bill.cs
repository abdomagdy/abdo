using PartnersDecisions.Core.Basis;
using PartnersDecisions.GraphDiff.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersDecisions.Core
{
    [Table("Bill", Schema = "pd")]
    // hello
    public class Bill : AuditableEntity
    {
        #region  Contractors

        public Bill()
        {

        }

        public Bill(string sadadNumber, int daysToExpire, string billerName, Guid referenceNumber, List<BillDetail> details)
        {
            this.SadadNumber = sadadNumber;
            double sumVATAmount = details.Sum(d => d.VATAmount);
            double sumAmount = details.Sum(d => d.Amount);
            this.TotalAmount = sumVATAmount + sumAmount;
            this.BillerName = billerName;
            this.Description = billerName;
            this.ReferenceNumber = referenceNumber;
            this.ExpirationDate = DateTime.Now.AddDays(daysToExpire);

            this.BillDetails = details;
            this.SetCreated();
        }

        public Bill(double amount, string description)
        {
            this.TotalAmount = amount;
            this.Description = description;
        }
        #endregion

        #region Properties
        [Key]
        public int BillID { get; private set; }

        [StringLength(50)]
        public string SadadNumber { get; private set; }

        public double TotalAmount { get; private set; }

        public DateTime? SadadDate { get; private set; }

        public DateTime? ExpirationDate { get; private set; }

        [StringLength(1000)]
        public string Description { get; private set; }

        public string BillerName { get; private set; }

        public Guid ReferenceNumber { get; private set; }

        [Owned]
        public List<BillDetail> BillDetails { get; private set; }

        #endregion

        #region Public Methods

        public Bill UpdateBill(string sadadNumber, DateTime expirationDate)
        {
            this.SadadNumber = sadadNumber;
            this.ExpirationDate = expirationDate;
            this.SetUpdated();
            return this;
        }

        public Bill SetBillPaid(DateTime SadadDate)
        {
            this.SadadDate = SadadDate;
            this.SetUpdated();
            return this;
        }

        public Bill SetDetialsBill(int invoiceID)
        {
            this.BillID = invoiceID;
            this.BillDetails.ForEach(d => { d.SetBillID(invoiceID); });
            this.SetUpdated();
            return this;
        }

        public Bill UpdateBillDetails(List<BillDetail> billDetails)
        {
            this.BillDetails = billDetails;
            this.SetUpdated();
            return this;
        }

        //public Bill SetBillCancelled()
        //{
        //    this.IsValid = false;
        //    this.SetUpdated();
        //    return this;
        //}
        #endregion
    }
}
