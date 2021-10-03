using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IFeedbackRepository
    {
        bool AddFeedBack(FeedBackModel feedBackData);
        List<FeedBackModel> GetFeedBack(int bookId);
    }
}
