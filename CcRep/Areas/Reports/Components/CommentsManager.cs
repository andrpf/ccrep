using CcRep.Areas.Reports.Models;
using CcRep.Models;
using CcRep.Models._dc;
using CcRep.Models.vk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CcRep.Areas.Reports.Components
{
    
    public class CommentsManager
    {
        CcRepContext db;
        public CommentsManager(CcRepContext dbCont)
        {
            db = dbCont;
        }

        public bool AddUserComment(AddCommentModel comment, string userId)
        {
            var UserModel = db.Users.Find(userId);

            var TranModel = db.AddInfReps.Find(comment.TranId);

            if(UserModel != null && TranModel != null)
            {
                var newComment = new CommentsRep() {
                    Date = DateTime.Now,
                    IdOper = TranModel.IdOper,
                    UserId = UserModel.Id,
                    Comments = comment.Comment
                };

                db.CommentsReps.Add(newComment);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<object> FindComments(long TranId)
        {
            var TranModel = db.AddInfReps.Include(i => i.CommentsReps).Where(d => d.IdOper == TranId).FirstOrDefault();

            if (TranModel != null)
            {
                var comments = TranModel
                    .CommentsReps
                    .Select(d => new { Comment = d.Comments, Date = d.Date.ToString(), User = d.User.UserName })
                    .OrderBy(t => t.Date);

                return comments;
            }

            return null;
        }




    }
}