using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
   public interface IFeedbackManager
    {
        bool AddFeedBack(FeedBackModel feedBackData);
        List<FeedBackModel> GetFeedBack(int bookId);
    }
}
