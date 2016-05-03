﻿using ClashOfTheCharacters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClashOfTheCharacters.Services
{
    public class TravelService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void CheckArrivalTime(string userId)
        {
            var travel = db.Travels.First(t => t.UserId == userId);

            if ((travel.ArrivalTime - DateTimeOffset.Now).TotalMilliseconds < 0)
            {
                var user = db.Users.Find(userId);

                db.CurrentLands.Add(new CurrentLand
                {
                    CurrentStageIndex = 1,
                    LandId = travel.LandId,
                    UserId = userId,
                    WildCreatureStartLevel = user.TeamMembers.OrderByDescending(tm => tm.Level).First().Level
                });

                db.Travels.Remove(travel);
                db.SaveChanges();
            }
        }
    }
}