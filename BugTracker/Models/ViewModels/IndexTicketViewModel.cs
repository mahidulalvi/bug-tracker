﻿using System;
using System.Collections.Generic;

namespace BugTracker.Models.ViewModels
{
    public class IndexTicketViewModel
    {
        public string Id { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string TicketType { get; set; }
        public string TicketPriority { get; set; }
        public string TicketStatus { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string MediaUrl { get; set; }
        public List<string> MediaUrls { get; set; }
        public ProjectMemberViewModel TicketOwner { get; set; }
        public List<ProjectMemberViewModel> TicketAssignees { get; set; }
        public List<ProjectMemberViewModel> TicketNonAssignees { get; set; }
        public string CommentData { get; set; }
        public List<IndexCommentViewModel> Comments { get; set; }
        public bool AvailableForUser { get; set; }
        public List<FullTicketHistory> TicketHistories { get; set; }

        public IndexTicketViewModel()
        {
            TicketAssignees = new List<ProjectMemberViewModel>();
            TicketNonAssignees = new List<ProjectMemberViewModel>();
            MediaUrls = new List<string>();
            Comments = new List<IndexCommentViewModel>();
            TicketHistories = new List<FullTicketHistory>();
        }
    }
}