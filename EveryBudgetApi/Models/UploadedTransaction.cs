using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace EveryBudgetApi.Models
{
    public class UploadedTransaction
    {
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string CheckNumber { get; set; }
        public string Description { get; set; }
        public string EffectiveDate { get; set; }
        public string ExtendedDescription { get; set; }
        public string Memo {  get; set; }
        public string PostingDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionCategory { get; set; }
        public string TransactionID { get; set; }
        public string TransactionType { get; set; }
        public string Type { get; set; }
    }
}
