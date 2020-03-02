using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database.Models;

namespace Core.Users.Implementation.Helpers
{
    public interface INotificationHelper
    {

    }

    public class NotificationHelper : INotificationHelper
    {
        public string GetText(NotificationPayloadModel payload)
        {
            switch (payload.Type.ToLower())
            {
                case "document_risk_status_changed": // IntValue1 = Documents count
                    if(payload.IntValue1 > 1) return $"<a href=''>{payload.IntValue1} documents changed their risk status. Click here to visualize these documents.</a>"; // StringValue1
                    return $"<a href=''>The risk status of document {payload.StringValue2} changed from {payload.StringValue3} to {payload.StringValue4}.</a>"; // GuidValue1, StringValue2, StringValue3, StringValue4
                //case "document_new":
                //    return "<a href=''>New document <doc_id> has been created for <owner_name>.</a>";
                //case "document_projected_deadline_changed":
                //    return "<a href=''>Deadline changed for document <doc_id> from <old_deadline> to <new_deadline>.</a>";
                //case "client_review_deadline_violation":
                //    return "<a href=''>Client violated maximum review time (<num_days> days) for document <doc_id>.</a>";
                //case "subcontractor_is_late":
                //    return "<a href=''>Subcontractor <subcontractor_name> has not submitted document <doc_id> yet. Document risk status changed to <new_status>.</a>";
            }
            return "";
        }

        //public string GetText(NotificationPayloadModel[] payload)
        //{
        //    foreach (var notification in payload.GroupBy(x => x.Type))
        //        switch (notification.Key.ToLower())
        //        {
        //            case "document_risk_status_changed":
        //                return $"<a href=''>{notification.Count()} documents changed their risk status. Click here to visualize these documents.</a>";
        //            //case "document_new":
        //            //    return "<a href=''><num_docs> documents were created. Click here to visualize these documents.</a>";
        //            //case "document_projected_deadline_changed":
        //            //    return "<a href=''>Deadline changed for <num_docs> documents. Click here to visualize affected documents.</a>";
        //            //case "client_review_deadline_violation":
        //            //    return "<a href=''>Client violated maximum review time for <num_docs> documents. Click here to visualize these documents.</a>";
        //            //case "subcontractor_is_late":
        //            //    return "<a href=''><num_docs> documents changed risk status because subcontractor <subcontractor_name> has not submitted them yet. Click here to visualize these documents.</a>";
        //        }
        //    return "";
        //}
    }
}
