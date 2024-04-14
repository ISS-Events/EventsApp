﻿using EventsApp.Logic.Adapters;
using EventsApp.Logic.Entities;
using EventsApp.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.Logic.Managers
{
    public static class ReviewsManager
    {
        private static DataAdapter<ReviewInfo> _adapter;

        public static void Initialize(DataAdapter<ReviewInfo> adapter)
        {
            _adapter = adapter;
        }

        public static ReviewInfo GetReview(Guid reviewerId, Guid eventId)
        {
            ReviewInfo review = new ReviewInfo(reviewerId, eventId);
            return _adapter.Get(review.GetIdentifier());
        }

        public static List<ReviewInfo> GetAllReviews()
        {
            return _adapter.GetAll();
        }

        public static void AddReview(Guid reviewerId, Guid eventId, float score, string description)
        {
            ReviewInfo review = new ReviewInfo(reviewerId, eventId, score, description);

            _adapter.Add(review);
        }

        public static List<ReviewInfo> GetAllReviewsOfReviewer(Guid reviewer)
        {
            List<ReviewInfo> reviews = new List<ReviewInfo>();

            foreach (ReviewInfo review in _adapter.GetAll())
            {
                if (review.userGUID == reviewer)
                {
                    reviews.Add(review);
                }
            }

            return reviews;
        }

        public static List<ReviewInfo> GetAllReviewsOfEvent(Guid eventId)
        {
            List<ReviewInfo> reviews = new List<ReviewInfo>();

            foreach (ReviewInfo review in _adapter.GetAll())
            {
                if (review.eventGUID == eventId)
                {
                    reviews.Add(review);
                }
            }

            return reviews;
        }

        public static List<ReviewInfo> GetAllReviewsOfUser(Guid userId)
        {
            List<EventInfo> eventsOfUser = EventsManager.GetEventsOfUser(userId);
            List<ReviewInfo> reviews = new List<ReviewInfo>();

            foreach (EventInfo eventInfo in eventsOfUser)
            {
                reviews.AddRange(GetAllReviewsOfEvent(eventInfo.GUID));
            }

            return reviews;
        }

    
        public static float GetReviewsAverageScoreOfUser(Guid userId)
        {
            List<ReviewInfo> userReviews = GetAllReviewsOfUser(userId);
            float averageScore = 0;
            foreach (ReviewInfo review in userReviews)
            {
                averageScore += review.score;
            }
            averageScore /= userReviews.Count;
            return averageScore;
        }
    }
}
