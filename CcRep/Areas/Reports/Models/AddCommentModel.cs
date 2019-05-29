namespace CcRep.Areas.Reports.Models
{
    public class AddCommentModel
    {
        private long tranId;
        public long TranId {
            get {
                return tranId;
            }
            set {
                tranId = value;
            }
        }

        public string Comment {
            get {
                return comment;
            }
            set {
                comment = TextHelper.RemoveHtmlTags(value).Trim();
            }
        }
        private string comment;
    }
}