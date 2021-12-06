namespace MyLeague.Football.Services.Responses
{
    public class TradeResult
    {
        public TradeResult(bool isAccepted, string reason)
        {
            this.IsAccepted = isAccepted;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets or sets whether or not the trade was accepted or not
        /// </summary>
        public bool IsAccepted { get; set; }

        /// <summary>
        /// Gets or sets the reason a trade was either accepted or rejected
        /// </summary>
        public string Reason { get; set; }

        public override string ToString()
        {
            if (this.IsAccepted)
            {
                return $"This trade was accepted because, {this.Reason}";
            }
            else
            {
                return $"This trade was rejected because, {this.Reason}";
            }
        }
    }
}
