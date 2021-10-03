using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class FeedBackManager:IFeedbackManager
    {
        private readonly IFeedbackRepository repository;

        public FeedBackManager(IFeedbackRepository repository)
        {
            this.repository = repository;
        }
        public bool AddFeedBack(FeedBackModel feedBackData)
        {
            try
            {
                return this.repository.AddFeedBack(feedBackData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
